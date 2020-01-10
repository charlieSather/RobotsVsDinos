using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Weapon
    {
        public string type { get; private set; }
        public double attackPower { get; private set; }
        public double drainMultiplier { get; private set; }

        public Weapon(string type, int attackPower, double drainMultiplier)
        {
            this.type = type;
            this.attackPower = attackPower;
            this.drainMultiplier = drainMultiplier;
        }
        public Weapon()
        {
            type = "fists";
            attackPower = 5;
            drainMultiplier = 25;
        }

        override    
        public string ToString()
        {
            return $"(Name: {type}, AttackPower: {attackPower}, PowerDrainMultiplier: {drainMultiplier}) Power Cost = {50 * drainMultiplier}";
        }



    }
}
