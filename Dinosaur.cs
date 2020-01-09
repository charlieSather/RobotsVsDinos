using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Dinosaur
    {
        public string type { get; private set; }
        double health;
        int energy;
        public double attackPower;
        List<AttackType> attacks; 

        public Dinosaur(string type, double health, int energy, double attackPower, List<AttackType> attacks)
        {
            this.type = type;
            this.health = health;
            this.energy = energy;
            this.attackPower = attackPower;
            this.attacks = attacks;
        }
        
        public bool Attack(Robot robot)
        {
            if(energy > 20)
            {
                robot.IncomingHit(attackPower);
                energy -= 20;
                return true;
            }
            else
            {
                Console.WriteLine($"{type} only has {energy} of energy which is not enough to attack!");
                Console.WriteLine($"{type} restores 10 points of energy");
                energy += 10;
                return false;

            }
        }

        public void IncomingHit(double damage)
        {
            if (health < damage)
            {
                health = 0;
                Console.WriteLine($"{type} has died!!!");
            }
            else
            {
                health -= damage;
                if (health == 0)
                {
                    Console.WriteLine($"{type} has died!!!");
                }
            }
        }
        public bool IsAlive()
        {
            return health > 0 ? true : false;
        }
        public void DisplayAttacks()
        {
            foreach(AttackType attack in attacks)
            {
                Console.WriteLine(attack);
            }
        }

        public double Health()
        {
            return health;
        }

        public string DisplayStatus()
        {
            return string.Format("Type: {0}, Health: {1}, Energy: {2}, Attack Power: {3}", type, health, energy, attackPower);
        }



    }
}
