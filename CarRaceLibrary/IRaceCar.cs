// *********************************************
// Copyright (c) RUDDOR_KT All rights reserved.   
// *********************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        void UpdateFuel();
        int LapCount { get; set; }
        double LapSeconds { get; set; }
        double PrevLapSeconds { get; set; }
        double LapDistance { get; set; }
        double PrevLapDistance { get; set; }
        Stopwatch LapStopwatch { get; set; }
        double CarFuel { get; set; }
    }
}
