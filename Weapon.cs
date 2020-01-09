using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Weapon
    {
        string type;
        double attackPower;

        public Weapon(string type, int attackPower)
        {
            this.type = type;
            this.attackPower = attackPower;
        }

        public double Damage()
        {
            return attackPower;
        }

        public string Type()
        {
            return type;
        }



    }
}
