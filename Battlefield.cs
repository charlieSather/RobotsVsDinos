using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Battlefield
    {
        Herd herd;
        Fleet fleet;

        public static Random rand = new Random();

        public Battlefield(Herd herd, Fleet fleet)
        {
            this.herd = herd;
            this.fleet = fleet;
        }

        public void RobotAttack(Robot robot, Dinosaur dinosaur)
        {
            if(HitChance() == true)
            {
                robot.Attack(dinosaur);
            }
        }
                  
        public void DinosaurAttack(Dinosaur dinosaur, Robot robot)
        {
            if (HitChance() == true)
            {
                dinosaur.Attack(robot);
            }

        }

        public bool HitChance()
        {
            Random rand = new Random();
            return rand.Next(0, 10) > 5 ? true : false;
        }
        public bool BattleIsActive()
        {
            bool checkHerd = herd.HasSurvivors();
            bool checkFleet = fleet.HasSurvivors();

            if(checkHerd == false || checkFleet == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ConcludeBattle()
        {

        }

        public (Dinosaur, Robot) GetMatchup(Herd herd, Fleet fleet)
        {
            int dinoIndex = RandomNumber(0, herd.dinosaurs.Count);
            int roboIndex = RandomNumber(0, fleet.robots.Count);    

            Dinosaur dinosaur = null;
            Robot robot = null;
            while(dinosaur == null)
            {
                if (herd.dinosaurs[dinoIndex].IsAlive() == true)
                {
                    dinosaur = herd.dinosaurs[dinoIndex];
                }
                else
                {
                    dinoIndex  = RandomNumber(0, herd.dinosaurs.Count); ;
                }
            }

            while (robot == null)
            {
                if (fleet.robots[dinoIndex].IsAlive() == true)
                {
                    robot = fleet.robots[roboIndex];
                }
                else
                {
                    roboIndex = RandomNumber(0, fleet.robots.Count); ;
                }
            }
            return (dinosaur, robot);
        }

        public void DisplayStats(Herd herd, Fleet fleet)
        {
            foreach(Dinosaur dino in herd.dinosaurs)
            {
                Console.WriteLine(dino.DisplayStatus());
            }

            foreach(Robot robo in fleet.robots)
            {
                Console.WriteLine(robo.DisplayStatus());
            }
        }
        public int RandomNumber(int start, int end)
        {
            return rand.Next(start, end);
        }
        public void DisplayWinner()
        {

        }
        public void DetermineWinner()
        {

        }
      

    }
}
