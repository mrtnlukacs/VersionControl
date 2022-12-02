using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroszimulacio.Entities
{
    class Person
    {
        public int Szulido { get; set; }
        public Gender Neme { get; set; }
        public int Gyerekekszama { get; set; }
        public bool IsAlive { get; set; }

        public Person()
        {
            IsAlive = true;
        }


    }
}
