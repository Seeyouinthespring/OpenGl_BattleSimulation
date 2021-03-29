using System;
using System.Collections.Generic;
using System.Linq;
using Tao.FreeGlut;
using Tao.OpenGl;
using static ZhuravlevPRI117_BattleSimulation.HelpEnums;

namespace ZhuravlevPRI117_BattleSimulation
{
    public class Battle
    {
        //переменная для проверки создания списка графических объектов 
        private bool isDisplayList = false;

        //переменная для проверки начала сражения
        private bool IsStart = false;
        
        //интервал между Юнитами при их расположении
        private const float PositionInterval = 2f;
        //интервал между выстрелами стрел
        private const float ArrowInterval = 3.3f;
        //время последнего выстрела
        public float LastShot;

        //грница поля сражения по X слева
        const float RedTeamStartX = -23;
        //грница поля сражения по X справа
        const float BlueTeamStartX = 22;
        //грница поля сражения по Y снизу
        const float StartY = -15;
        //грница поля сражения по Y сверху
        const float EndY = 15;

        //Сисок всех Юнитов
        public List<Unit> AllUnits;
        //список всех Стрел
        public List<Arrow> AllArrows;
        //массив id живых Юнитов красной армии
        public int[] AliveBlues;
        //массив id живых Юнитов синей армии
        public int[] AliveReds;

        Random rnd = new Random();

        public Battle(int RedArchersNumber, int BlueArchersNumber, string BlueFormation, string RedFormation, float globalTime) 
        {
            LastShot = globalTime;
            AllUnits = new List<Unit>();
            AllArrows = new List<Arrow>();

            ArrangeArchers(Team.Red,RedArchersNumber, 0);
            ArrangeMelee(Team.Red, 20 - RedArchersNumber, RedFormation, RedArchersNumber);
            ArrangeArchers(Team.Blue, BlueArchersNumber, 20);
            ArrangeMelee(Team.Blue, 20 - BlueArchersNumber, BlueFormation, 20 + BlueArchersNumber);

            MixList();
        }

        //метод расстановки (определения позиции) Пехотинцев
        private void ArrangeMelee(Team team, int number, string formation, int plusId)
        {
            // коэффициент направления смещения в зависимости от армии
            int k = (team == Team.Blue) ? 1 : -1;
            switch (formation) 
            {
                // алгоритм построения фалангой
                case "Phalanx":
                    float startX = (team == Team.Blue) ? BlueTeamStartX : RedTeamStartX;
                    float interval = (EndY - StartY) / (number - 1);
                    for (int i = 1; i <= number; i++) 
                    {
                        Unit unit = new Unit(i + plusId, startX, StartY + (interval * (i-1)), 0, team, TypeOfWarrior.Melee);
                        AllUnits.Add(unit);
                    }
                    break;
                // алгоритм построения Черепахой
                case "Tortoise":
                    int ky = -1;
                    int kx = (team == Team.Blue) ? 1 : -1;
                    float startTortoiseX = (team == Team.Blue) ? BlueTeamStartX - (4 * PositionInterval) : RedTeamStartX + (4 * PositionInterval);
                    float help = 
                        ((number + 1)/3 == 2 ) ? PositionInterval :
                        ((number + 1) / 3 == 3) ? PositionInterval * 2 :
                        ((number + 1) / 3 == 4) ? PositionInterval * 3 : PositionInterval * 4;
                    float startTortoiseY = (EndY + StartY)/2 + help;
                    for (int i = 1; i <= number; i++) 
                    {
                        float tx = 0;
                        float ty = 0;
                        if (i % 3 == 0)
                        {
                            tx = startTortoiseX + (PositionInterval * 2 * kx);
                            ty = startTortoiseY + (PositionInterval * (i / 3 * 2 - 2) * ky);
                        }
                        else if (i % 3 == 1)
                        {
                            if (number == i)
                            {
                                tx = startTortoiseX + (PositionInterval * kx);
                                ty = startTortoiseY + (PositionInterval * (i / 3 * 2 - 1) * ky);
                            }
                            else
                            {
                                tx = startTortoiseX;
                                ty = startTortoiseY + (PositionInterval * (i / 3 * 2) * ky);
                            }
                        }
                        else if (i % 3 == 2)
                        {
                            if (number == i)
                            {
                                tx = startTortoiseX + (PositionInterval * 2 * kx);
                                ty = startTortoiseY + (PositionInterval * (i / 3 * 2) * ky);
                            }
                            else
                            {
                                tx = startTortoiseX + (PositionInterval * kx);
                                if (number % 3 == 1)
                                    ty = startTortoiseY + (PositionInterval * (i / 3 * 2 - 1) * ky);
                                else
                                    ty = startTortoiseY + (PositionInterval * (i / 3 * 2 + 1) * ky);
                            }
                        }
                        Unit unit = new Unit(i + plusId, tx, ty, 0, team, TypeOfWarrior.Melee);
                        AllUnits.Add(unit);
                    }
                    break;
                // алгоритм построения Клином
                case "Wedge":
                    float startWedgeY = 0;
                    float startWedgeX = (team == Team.Blue)? BlueTeamStartX - (number / 2) : RedTeamStartX + (number / 2);
                    for (int i = 1; i <= number; i++) 
                    {
                        float x = startWedgeX + ((i / 2) * k);
                        float y = (i % 2 == 0) ? startWedgeY + (PositionInterval * (i / 2)) : startWedgeY - (PositionInterval * (i / 2));
                        Unit unit = new Unit(i + plusId, x, y, 0, team, TypeOfWarrior.Melee);
                        AllUnits.Add(unit);
                    }
                    break;
            }
        }

        //метод расстановки Стрелков
        private void ArrangeArchers(Team team, int number, int plusId) 
        {
            float startX = (team == Team.Blue)? BlueTeamStartX + 2 : RedTeamStartX -2;
            float interval = (EndY - StartY)/(number-1);
            for (int i = 1; i <= number; i++) 
            {
                var unit = new Unit(i + plusId, startX, StartY+(interval*(i-1)), 0, team, TypeOfWarrior.Range);
                var arrow = new Arrow(unit.PositionX, unit.PositionY, 1, unit.Id, team);
                AllUnits.Add(unit);
                AllArrows.Add(arrow);
            }
        }

        //начало сражения
        public void StartBattle() 
        {
            // создание моделей объектов
            if (!isDisplayList)
            {
                CreateDisplayList();
            }
            // установка стартовых статусов для всех Пехотинцев 
            foreach (var unit in AllUnits.Where(x => x.Type == TypeOfWarrior.Melee)) 
            {
                unit.Status = Status.Move;
                unit.MovingStatus = MovingStatus.Default;
            }
            //установка начала сражения
            IsStart = true;
        }

        //метод перемешивания списков
        private void MixList() 
        {
            //перемешивание списка Юнитов
            for (int i = 0; i < AllUnits.Count(); i++) 
            {
                int j = rnd.Next(i + 1);
                var temp = AllUnits[j];
                AllUnits[j] = AllUnits[i];
                AllUnits[i] = temp;
            }
            //перемешивание списка Стрел
            for (int i = 0; i < AllArrows.Count(); i++)
            {
                int j = rnd.Next(i + 1);
                var temp = AllArrows[j];
                AllArrows[j] = AllArrows[i];
                AllArrows[i] = temp;
            }
        }

        // метод создания графических моделей объектов
        private void CreateDisplayList()
        {
            DrawingLogic.DrawElements();
            isDisplayList = true;
        }

        //метод рассчета логики для Стрел
        public void CalculateArrows(float globalTime)
        {
            // если сражение не началось происходит выход из метода
            if (!IsStart) return;

            //если пришло время нового выстрела
            if (LastShot + ArrowInterval < globalTime) 
            { 
                // переписывание в список стрел только тех стрел, чьи стрелки еще живы
                AllArrows = AllArrows.Where(x => AllUnits.Any(u => u.Id == x.Id)).ToList();
                foreach (var arrow in AllArrows) 
                {
                    //определение случайной цели
                    arrow.TargetId = (arrow.Team == Team.Blue) ? 
                       AliveReds[rnd.Next(0, AliveReds.Length)] :
                       AliveBlues[rnd.Next(0, AliveBlues.Length)];
                    // обновление свойств до стандартных
                    arrow.IsAlive = true;
                    arrow.PositionX = arrow.DefaultStartPositionX;
                    arrow.PositionY = arrow.DefaultStartPositionY;
                    arrow.PositionZ = arrow.DefaultStartPositionZ;
                    // обновление времени последнего выстрела
                    LastShot = globalTime;
                }
            }
            
            foreach (Arrow arrow in AllArrows) 
            {
                // если стрела еще в полете
                if (arrow.IsAlive) 
                {
                    // показатель того существует ли еще цель стрелы или нет
                    bool endPointFlag;
                    
                    //определение объекта, который является целью стрелы
                    var target = AllUnits.FirstOrDefault(x => x.Id == arrow.TargetId);
                    if (target != null)
                    {
                        endPointFlag = true;
                        // обновление положения объекта
                        arrow.UpdatePosition(endPointFlag, target.PositionX, target.PositionY);
                    }
                    else {
                        endPointFlag = false;
                        // обновление положжения объекта
                        arrow.UpdatePosition(endPointFlag); 
                    }

                    //проверка достигла ли стрела цели (находится ли она в радиусе поражения)
                    if (arrow.IsInRange(endPointFlag, target?.PositionX ?? null, target?.PositionY ?? null)) 
                    {
                        // окончание полета стрелы
                        arrow.IsAlive = false;
                        // нанесение урона
                        target.Health -= rnd.Next(arrow.MinDamage, arrow.MaxDamage);
                        if (target.Health < 1)
                            //убийство Юнита при потере всех единиц здоровья
                            target.IsAlive = false;
                    }

                    // проверка вылета стрелы за пределы поля сражения
                    // это возможно когда Юнит был убит в процессе полета стрелы
                    // в таком случае стрела перестает отрисовываться
                    if (arrow.PositionX > BlueTeamStartX + 5 || arrow.PositionX < RedTeamStartX - 5 || arrow.PositionY > EndY + 5 || arrow.PositionY < StartY - 5)
                        continue;

                    //определение угла поворота стрелы
                    float angle = (arrow.Team == Team.Red) ? 90 - (arrow.Angle * (180f / (float)Math.PI)) : 270 - (arrow.Angle * (180f / (float)Math.PI));

                    Gl.glPushMatrix();
                    //перемещение стрелы
                    Gl.glTranslated(arrow.PositionX, arrow.PositionY, arrow.PositionZ);
                    //поворот стрелы на зажанный угол
                    Gl.glRotatef(angle, 0, 0, 1);
                    //отрисовка стрелы в зависимости от армии, которой она принадлежит
                    if (arrow.Team == Team.Blue)
                        Gl.glCallList(6);
                    else
                        Gl.glCallList(5);
                    Gl.glPopMatrix();
                }
            }
        }
        
        //расчет логики для Юнитов
        public int? Calculate(float globalTime)
        {
            // если сражение не началось происходит выход из метода
            if (!IsStart) return null;

            // обновление списка Юнитов только живыми Юнитами
            AllUnits = AllUnits.Where(x => x.IsAlive).ToList();
            //обновления списков с Id юнитов только теми Id тех Юнитов, которые живы
            AliveBlues = AllUnits.Where(x => x.Team == Team.Blue).Select(x => x.Id).ToArray();
            AliveReds = AllUnits.Where(x => x.Team == Team.Red).Select(x => x.Id).ToArray();

            // прверка закончилось ли сражение (остались ли живые Юниты в армии)
            if (!AliveBlues.Any())
                // победа красных
                return 0;
            if (!AliveReds.Any())
                // победа синих
                return 1; 

            foreach (var unit in AllUnits) 
            {
                switch (unit.Status)
                {
                    // обработчик атаки юнита
                    case Status.Attack:
                        // обновление стастуса движения если он еще не обновлен
                        if (unit.MovingStatus != MovingStatus.Target) unit.MovingStatus = MovingStatus.Target;

                        // определение цели Юнита
                        Unit target = AllUnits.FirstOrDefault(x => x.Id == unit.TargetId);
                        // если цель находится вне радиуса атаки Юниту присваивается статус движения, выход из обработчика
                        if (target == null || !unit.IsInRange(target.PositionX, target.PositionY))
                        {
                            unit.Status = Status.Move;
                            break;
                        }

                        //вычисление времени оставшегося до следующего удара
                        float delta = (unit.NextAvailableAttakTime < globalTime) ? 
                            unit.AttackSpeed :
                            unit.NextAvailableAttakTime - globalTime;

                        //если время следующего удара наступило (стало меньше глобального времени) призводится атака
                        if (unit.NextAvailableAttakTime < globalTime && unit.IsAlive)
                        {
                            // обновление времени следующей атаки
                            unit.NextAvailableAttakTime = globalTime + unit.AttackSpeed;
                            // нанесение урона цели
                            target.Health -= rnd.Next(unit.MinDamage, unit.MaxDamage);
                            if (target.Health < 1)
                            {
                                // если у цели закончилось здоровье меняется статус и цель Юнита, пометка цели как не живой
                                target.IsAlive = false;
                                unit.TargetId = null;
                                unit.Status = Status.Move;
                            }
                        }

                        // рисование атакующего Юнита и его здоровья в зависимости от армии
                        Gl.glPushMatrix();
                        if (unit.Team == Team.Blue)
                        {
                            // перенос системы координат
                            Gl.glTranslatef(unit.PositionX, unit.PositionY, unit.PositionZ);
                            // поворот системы координат
                            Gl.glRotatef(-delta * 5, 0, 1, 0);
                            // рисование соответствующей содели
                            Gl.glCallList(2);
                            // рисование значения здоровья
                            PrintText2D(unit.PositionX - unit.PositionX + 2, unit.PositionY - unit.PositionY, unit.Health.ToString());
                        }
                        else
                        {
                            // перенос системы координат
                            Gl.glTranslatef(unit.PositionX, unit.PositionY, unit.PositionZ);
                            // поворот системы координат
                            Gl.glRotatef(delta * 5, 0, 1, 0);
                            // рисование соответствующей содели
                            Gl.glCallList(1);
                            // рисование значения здоровья
                            PrintText2D(unit.PositionX - unit.PositionX -3, unit.PositionY - unit.PositionY, unit.Health.ToString());
                        }
                        Gl.glPopMatrix();
                        break;

                        //обработка движения Юнита
                    case Status.Move:
                        // выход из обработчика если Юнит стрелок (проверка)
                        if (unit.Type != TypeOfWarrior.Melee)
                            break;
                        //нахождение ближайшего противника
                        Unit nearestEnemy = FindNearestEnemyUnit(unit, AllUnits);
                        //проверка находится ли ближайший противник в радиусе атаки
                        if (unit.IsInRange(nearestEnemy.PositionX, nearestEnemy.PositionY))
                        {
                            // если в радиусе, то смена статуса на Атаку и установка Id цели
                            unit.Status = Status.Attack;
                            unit.TargetId = nearestEnemy.Id;
                        }
                        else
                        {
                            //если противник не в радиусе то обновляется положение объекта в зависимости от статуса движения и пройденного расстояния 
                            //(после прохождения большей части поля сражения статус движения становится направленным на цель)
                            if (unit.MovingStatus == MovingStatus.Default)
                                if (unit.Team == Team.Blue && unit.PositionX < RedTeamStartX + 5 || unit.Team == Team.Red && unit.PositionX > BlueTeamStartX - 5)
                                    unit.MovingStatus = MovingStatus.Target;
                                else
                                    unit.UpdatePosition();
                            else
                                unit.UpdatePosition(nearestEnemy.PositionX, nearestEnemy.PositionY);
                        }

                        //рисование модели
                        Gl.glPushMatrix();
                        //перенос системы координат
                        Gl.glTranslated(unit.PositionX, unit.PositionY, unit.PositionZ);
                        if (unit.Team == Team.Blue)
                        {
                            // рисование соответствующей модели и значения здоровья
                            Gl.glCallList(2);
                            PrintText2D(unit.PositionX - unit.PositionX + 2, unit.PositionY - unit.PositionY, unit.Health.ToString());
                        }
                        else
                        {
                            // рисование соответствующей модели и значения здоровья
                            Gl.glCallList(1);
                            PrintText2D(unit.PositionX - unit.PositionX - 3 , unit.PositionY - unit.PositionY, unit.Health.ToString());
                        }
                        Gl.glPopMatrix();
                        break;
                    
                    
                    
                    // обработка стоящего Юнита-стрелка
                    case Status.Wait:
                        Gl.glPushMatrix();
                        Gl.glTranslated(unit.PositionX, unit.PositionY, unit.PositionZ);    
                        if (unit.Team == Team.Blue)
                        {
                            if (unit.Type == TypeOfWarrior.Melee)
                                Gl.glCallList(2);
                            else
                            {
                                Gl.glCallList(4);
                                PrintText2D(unit.PositionX - unit.PositionX + 1, unit.PositionY - unit.PositionY, unit.Health.ToString());
                            }
                        }
                        else
                        {
                            if (unit.Type == TypeOfWarrior.Melee)
                                Gl.glCallList(1);
                            else
                            {
                                Gl.glCallList(3);
                                PrintText2D(unit.PositionX - unit.PositionX - 2, unit.PositionY - unit.PositionY, unit.Health.ToString());
                            }
                        }
                        Gl.glPopMatrix();
                        break;
                }
            }
            return null;
        }

        //метод нахождения ближайшего противника
        private Unit FindNearestEnemyUnit(Unit currentUnit, List<Unit> enemyUnits)
        {
            if (enemyUnits == null || !enemyUnits.Any())
                return null;

            // ближайший противник находится по теореме Пифагора
            // расстояния между объектами по координатам X (x1-x0) и Y (y1 - y0) приняты за катеты
            // тогда гипотенузой является расстояние
            // Список сортируется по возрастанию рассчитанного значения гипотенузы
            // возвращается первый элемент отсортированного списка
            enemyUnits = enemyUnits.Where(x => x.Team != currentUnit.Team)
                .OrderBy(x =>
                    Math.Sqrt((
                        x.PositionX - currentUnit.PositionX) * (x.PositionX - currentUnit.PositionX) +
                        (x.PositionY - currentUnit.PositionY) * (x.PositionY - currentUnit.PositionY)))
                .ToList();
            return enemyUnits.First();
        }

        // метод вывода текста на экран сцены
        private void PrintText2D(float x, float y, string text)
        {
            Gl.glRasterPos3f(x,y,2);
            foreach (char char_for_draw in text)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_8_BY_13, char_for_draw);
            }
        }
    }
}
