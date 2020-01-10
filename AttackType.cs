
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class AttackType
    {

        public string name { get; private set; }
        public double attackMultiplier { get; private set; }
        public double energyMultiplier { get; private set; }

        public AttackType(string name, double attackMultiplier, double energyMultiplier)
        {
            this.name = name;
            this.attackMultiplier = attackMultiplier;
            this.energyMultiplier = energyMultiplier;
        }

        override
        public string ToString()
        {            
            return $"(Name: {name}, AttackMultiplier: {attackMultiplier}, EnergyMultiplier: {energyMultiplier}) Energy Cost = {energyMultiplier * 20}";
        }


    }
}
