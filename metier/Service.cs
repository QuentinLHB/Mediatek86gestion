using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    class Service
    {
        
        public bool Lecture { get; set; }
        public bool Modification { get; set; }

        public Service(int id)
        {
            switch (id)
            {
                case 1: Lecture = true; Modification = true; break;
                case 2: Lecture = false; Modification = false; break;
                case 3: Lecture = true; Modification = false; break;
                default: Lecture = false; Modification = false; break;
            }
        }
    }
}
