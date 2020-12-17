using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceLibrary
{
    public interface IRaceCar
    {
        int NumberOfLapsPassed { get; set; }
        Task Run();
        enum Status { Idle, Start, Running, Complete };
        Status GetRaceCarStatus();
    }
}
