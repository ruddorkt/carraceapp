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
    public class CarConfiguration : ICarConfiguration
    {
        private RaceTrack _raceTrack;
        private readonly Guid _configurationId;
        private double _fuelCapacity;
        private TimeSpan _timePerLap;
        private double _fuelConsumptionPerLap;

        public CarConfiguration(IRaceTrack raceTrack, double fuelCapacity, TimeSpan timePerLap, double fuelConsumptionPerLap)
        {
            _raceTrack = (RaceTrack)raceTrack;
            _fuelCapacity = fuelCapacity;
            _timePerLap = timePerLap;
            _fuelConsumptionPerLap = fuelConsumptionPerLap;
            _configurationId = Guid.NewGuid();
        }
        public RaceTrack RaceTrack { get => _raceTrack; }
        public Guid ConfigurationId { get => _configurationId; }
        public double FuelCapacity { get => _fuelCapacity; set => _fuelCapacity = value; }
        public TimeSpan TimePerLap { get => _timePerLap; set => _timePerLap = value; }
        public double FuelConsumptionPerLap { get => _fuelConsumptionPerLap; set => _fuelConsumptionPerLap = value; }

    }
}
