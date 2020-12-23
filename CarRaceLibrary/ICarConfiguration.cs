// *********************************************
// Copyright (c) KANG TAN All rights reserved.   
// *********************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceLibrary
{
    public interface ICarConfiguration
    {
        public double FuelCapacity { get; set; }
        public TimeSpan TimePerLap { get; set; }
        public double FuelConsumptionPerLap { get; set; }
    }
}
