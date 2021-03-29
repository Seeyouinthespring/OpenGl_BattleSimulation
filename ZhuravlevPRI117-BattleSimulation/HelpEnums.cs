
namespace ZhuravlevPRI117_BattleSimulation
{
    public class HelpEnums
    {
        //Статус Юнита
        public enum Status 
        {
            //Стоит
            Wait = 0,
            //Атакует
            Attack= 1,
            //Двигается
            Move = 2
        }

        //Какой армии принадлежит объект
        public enum Team 
        {
            //Красная армия
            Red = 0,
            //Синяя армия
            Blue = 1
        }

        //Статус движения Юнита
        public enum MovingStatus 
        {
            //Стандартный (по прямой навстречу противнику)
            Default = 0,
            //Направленный на цель
            Target = 1,
            //Никакой
            None = 2
        }

        //Тип объекта класса Юнит
        public enum TypeOfWarrior
        {
            //Пехотинец
            Melee = 0,
            //Стрелок
            Range = 1
        }
    }
}
