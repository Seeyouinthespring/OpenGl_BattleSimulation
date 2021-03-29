using System;
using static ZhuravlevPRI117_BattleSimulation.HelpEnums;

namespace ZhuravlevPRI117_BattleSimulation
{
    public class Unit
    {
        public float Step;

        public float AttackSpeed 
        { 
            get 
            { 
                return 5.5f - (this.Health * 0.01f); 
            } 
        }

        public sbyte DefaultXMovingVector { get; set; }

        public int Id { get; set; }

        public Team Team { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        public bool IsAlive { get; set; }

        public TypeOfWarrior Type { get; set; }
        public Status Status { get; set; }
        public MovingStatus MovingStatus { get; set; }

        public int Health { get; set; }
        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }

        public int? TargetId { get; set; }

        public float NextAvailableAttakTime { get; set; }

        public Unit(int id, float x, float y, float z, Team team, TypeOfWarrior type) 
        {
            Id = id;
            PositionX = x;
            PositionY = y;
            PositionZ = z;

            MaxDamage = 30;
            MinDamage = 20;
            
            IsAlive = true;
            Status = Status.Wait;
            MovingStatus = MovingStatus.None;
            Type = type;
            Health = 100;
            TargetId = null;
            Team = team;
            NextAvailableAttakTime = 0;
            DefaultXMovingVector = (sbyte)((team == Team.Blue) ? -1 : 1);
        }

        //Метод обновления положения объекта
        public void UpdatePosition(float targetX = 0, float targetY = 0 )
        {
            Step = 0.08f + (0.0015f * Health);

            if(MovingStatus == MovingStatus.Default)
                PositionX += (Step * DefaultXMovingVector);
            if (MovingStatus == MovingStatus.Target) 
            {
                float oldX = PositionX;
                float oldY = PositionY;

                PositionX = PositionX + Step * (float)Math.Sin(Math.Atan2(targetX - oldX, targetY - oldY));
                PositionY = PositionY + Step * (float)Math.Cos(Math.Atan2(targetX - oldX, targetY - oldY));
            }

        }

        //Метод проверки нахождения объекта в радиусе атаки цели
        public bool IsInRange(float enemyX, float enemyY) 
        {
            return Math.Sqrt((
                    enemyX - this.PositionX) * (enemyX - this.PositionX) +
                    (enemyY - this.PositionY) * (enemyY - this.PositionY)) < 0.5;
        } 
    }
}
