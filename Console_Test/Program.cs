using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Ship aeroship = new Aerocarrier();
            

            Ship boat = new Boat();
            boat.SetPosition(7);

            Console.WriteLine(aeroship.IsAlive);

            aeroship.Remove(3);
            aeroship.Remove(1);

            for (int i = 0; i < aeroship.Size; i++)
                Console.WriteLine(aeroship.Body[i]);


            boat.Remove(7);
            Console.WriteLine(boat.IsAlive);


            Console.Read();
        }
    }
}
