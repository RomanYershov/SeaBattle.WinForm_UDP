using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Boat : Ship
    {
        public Boat()
        {
            Size = 1;
            Name = "Boat";
        }

        public override void SetPosition(int pos)
        {
            Body.Add(pos);
        }
    }
}
