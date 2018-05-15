using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public abstract class Ship
    {
        private bool _isAlive;
        public int Size { get; set; }
        public string Name { get; set; }
        public bool IsAlive
        {
            get
            {
                if (Body.Count == 0) _isAlive = false;
                return _isAlive;
            }
            set
            {
                _isAlive = value;
            }
        }
        public List<int> Body { get; set; }

        public Ship()
        {
            Body = new List<int>();
            IsAlive = true;
        }

        //public virtual void SetPosition(int [] pos)
        //{
        //    if (pos.Length > Size)
        //    {
        //        for (int i = 0; i < Size; i++)
        //            Body.Add(pos[i]);
        //        return;
        //    }
        //    Body.AddRange(pos);
        //    if (pos.Length < Size)
        //    {               
        //        for (int i = pos.Length; i < Size; i++)
        //            Body.Add(Body[i - 1] + 1);
        //    }

        //}

        public virtual void SetPosition(int pos)
        {
            Body.Add(pos);
        }
        

        public virtual void Remove(int cell)
        {
            int index = Body.IndexOf(cell);
            if (index == -1) return;
            Body.RemoveAt(index);
            --Size;
        }

    } 
}

