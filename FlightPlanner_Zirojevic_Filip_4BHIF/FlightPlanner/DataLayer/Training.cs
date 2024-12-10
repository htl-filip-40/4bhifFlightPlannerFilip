using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    internal class Training
    {
        public int id { set; get; }

        public Training(int id) 
        {
            this.id = id;
        }
    }
}