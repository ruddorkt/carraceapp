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
        //void Process(ref bool isRuning, ref long i, ref double lapSeconds, ref double lapDistance);
        void UpdateFuel(ref double lapDistance, ref double prevLapDistance, ref double _carFuel);
        //void LapCounts(ref bool isRuning, ref double lapSeconds, ref double preLapSeconds, ref double lapDistance, ref double prevLapDistance, ref Stopwatch lapStopWatch);
        //bool EndProcess(bool isRuning, double lapDistance);
        //void Reset(ref double lapSeconds, ref double prevLapSeconds, ref double lapDistance, ref double prevLapDistance, ref Stopwatch lapStopWatch);

    }
}
