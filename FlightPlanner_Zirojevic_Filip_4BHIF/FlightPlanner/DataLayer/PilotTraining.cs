using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.DataLayer
{
    class PilotTraining
    {
        public int PilotId { set; get; }
        public int trainingID { set; get; }
        public DateTime date { set; get; }

        public PilotTraining(int pilotId, int trainingID, DateTime date)
        {
            PilotId = pilotId;
            trainingID = trainingID;
            date = date;
        }
    }
}