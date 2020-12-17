using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceLibrary
{
    public interface IRaceTrack
    {
        public double LapDistrance { get; set; }
        public int NumberOfLaps { get; set; }
        public static TimeSpan TimeToPitstop { get; set; }
    }
}
