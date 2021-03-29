using System;
using static ZhuravlevPRI117_BattleSimulation.HelpEnums;

namespace ZhuravlevPRI117_BattleSimulation
{
    public class Arrow
    {
        public float DefaultStartPositionX { get; set; }
        public float DefaultStartPositionY { get; set; }
        public float DefaultStartPositionZ { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        public float Angle { get; set; }

        public int Id { get; set; }

        public bool IsAlive { get; set; }

        public int? TargetId { get; set; }

        public const float Step = 1f;

        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }

        public Team Team { get; set; }

        public Arrow(float x, float y, float z, int id, Team team) 
        {
            PositionX = x;
            PositionY = y;
            PositionZ = z;
            Id = id;
            Team = team;
            TargetId = null;
            DefaultStartPositionX = x;
            DefaultStartPositionY = y;
            DefaultStartPositionZ = z;

            MinDamage = 10;
            MaxDamage = 20;
        }

        //Метод обновления положения объекта
        public void UpdatePosition(bool liveCheck,float targetX = 0, float targetY = 0)
        {

            if (!liveCheck)
            {
                float oldX = DefaultStartPositionX;
                float oldY = DefaultStartPositionY;

                Angle = (float)Math.Atan2(targetX - oldX, targetY - oldY);
                PositionX = PositionX + Step * (float)Math.Sin(Angle);
                PositionY = PositionY + Step * (float)Math.Cos(Angle);
                PositionZ = 1;
            }
            else 
            {
                float oldX = PositionX;
                float oldY = PositionY;
                
                Angle = (float)Math.Atan2(targetX - oldX, targetY - oldY);
                PositionX = PositionX + Step * (float)Math.Sin(Angle);
                PositionY = PositionY + Step * (float)Math.Cos(Angle);
            }

        }

        //Метод проверки нахождения объекта в радиусе атаки цели
        public bool IsInRange(bool liveCheck, float? enemyX = 0, float? enemyY = 0)
        {
            if(!liveCheck)
                return
                    Math.Sqrt((
                    DefaultStartPositionX - PositionX) * (DefaultStartPositionX - PositionX) +
                    (DefaultStartPositionY - PositionY) * (DefaultStartPositionY - PositionY)) < 0.5;
            return Math.Sqrt((
                    enemyX.Value - PositionX) * (enemyX.Value - PositionX) +
                    (enemyY.Value - PositionY) * (enemyY.Value - PositionY)) < 0.5;
        }
    }
}
