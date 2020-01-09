using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Robot
    {
        public string name;
        double health;
        int powerLevel;
        public Weapon weapon;

        public Robot(string name, double health, int powerLevel, Weapon weapon)
        {
            this.name = name;
            this.health = health;
            this.powerLevel = powerLevel;
            this.weapon = weapon;
        }

        public bool Attack(Dinosaur dinosaur)
        {
            if(powerLevel > 50)
            {
                powerLevel -= 50;
                dinosaur.IncomingHit(weapon.Damage());
                return true;
            }
            else
            {
                Console.WriteLine($"Power Level is only { powerLevel } and not enough to attack");
                Console.WriteLine($"{name} restores his powerLevel by 25");
                powerLevel += 25;
                return false;
            }

        }
        
        public void IncomingHit(double damage)
        {
            if(health < damage)
            {
                health = 0;
                Console.WriteLine($"{name} has died!!!");
            }
            else
            {
                health -= damage;
                if(health == 0)
                {
                    Console.WriteLine($"{name} has died!!!");
                }
            }
        }
        public double Health()
        {
            return health;
        }
        public bool IsAlive()
        {
            return health > 0 ? true : false;
        }

        public string DisplayStatus()
        {
            return string.Format("Name: {0}, Health: {1}, Power Level: {2}, Weapon: {3}", name, health, powerLevel, weapon.Type());
        }

    }
}
