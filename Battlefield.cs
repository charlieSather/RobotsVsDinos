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

        public bool HitChance()
        {
            return RandomNumber(11) > 0 ? true : false;
        }
        public bool BattleIsActive()
        {
            bool checkHerd = herd.HasSurvivors();
            bool checkFleet = fleet.HasSurvivors();

            if (checkHerd == false || checkFleet == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public (Dinosaur, Robot) GetMatchup(Herd herd, Fleet fleet)
        {
            int dinoIndex = RandomNumber(herd.dinosaurs.Count);
            int roboIndex = RandomNumber(fleet.robots.Count);    

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
                    dinoIndex  = RandomNumber(herd.dinosaurs.Count); ;
                }
            }

            while (robot == null)
            {
                if (fleet.robots[roboIndex].IsAlive() == true)
                {
                    robot = fleet.robots[roboIndex];
                }
                else
                {
                    roboIndex = RandomNumber(fleet.robots.Count); ;
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
        public int RandomNumber(int range)
        {
            return rand.Next(range);
        }
        public void DisplayWinner(Herd herd, Fleet fleet)
        {
            if (herd.HasSurvivors() == true)
            {
                Console.WriteLine("Winner winner chicken dinner!!!\nTeam Dinosaur is victorious!!!");
            }
            else
            {
                Console.WriteLine("Winner winner chicken dinner!!!\nTeam Robot is victorious!!!");
            }
        }
    }
}
