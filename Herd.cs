using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Herd
    {
        public List<Dinosaur> dinosaurs;

        public Herd(List<Dinosaur> dinosaurs)
        {
            this.dinosaurs = dinosaurs;
        }

        public bool HasSurvivors()
        {
            bool alive = false;
            foreach (Dinosaur dino in dinosaurs)
            {
                if (dino.Health() > 0)
                {
                    alive = true;
                    break;
                }
            }
            return alive;
        }



    }
}
