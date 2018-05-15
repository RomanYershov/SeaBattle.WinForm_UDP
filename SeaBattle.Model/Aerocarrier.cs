using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
     public  class Aerocarrier : Ship
    {
        public Aerocarrier() : base()
        {
            Size = 5;
            Name = "Aerocarrier";
        }

        public override void SetPosition(int pos)
        {
            if (Body.Count >= Size)
                return;

            Body.Add(pos);
        }
    }
}
