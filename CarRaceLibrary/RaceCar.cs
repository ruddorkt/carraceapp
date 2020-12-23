// **********************************************
// Copyright (c) KANG TAN All rights reserved.   
// **********************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceLibrary
{
    public class RaceCar : IRaceCar
    {
        private readonly Guid _raceCarId;
        private double _fuelLevel;
        private readonly ICarConfiguration _carConfiguration;
        private readonly RaceTrack _raceTrack;
        private readonly double _speed;
        private int _numberOfLapsPassed;

        IRaceCar.Status _status;
        private double _carFuel;
        private int _lapCount = 0;
        private double _lapSeconds;
        private double _prevLapSeconds;
        private double _lapDistance;
        private double _prevLapDistance;
        private Stopwatch _lapStopwatch;

        public RaceCar(ICarConfiguration carConfiguration, double fuelLevel)
        {
            _carConfiguration = carConfiguration;
            _raceTrack = ((CarConfiguration)carConfiguration).RaceTrack;
            _raceCarId = Guid.NewGuid();
            _fuelLevel = fuelLevel;
            _speed = _raceTrack.LapDistrance / _carConfiguration.TimePerLap.TotalSeconds;
        }

        public RaceCar(ICarConfiguration carConfiguration)
        {
            _carConfiguration = carConfiguration;
            _raceTrack = ((CarConfiguration)carConfiguration).RaceTrack;
            _raceCarId = Guid.NewGuid();
            _fuelLevel = _carConfiguration.FuelCapacity;
            _speed = _raceTrack.LapDistrance / _carConfiguration.TimePerLap.TotalSeconds;
        }

        public Guid RaceCarId { get => _raceCarId; }
        int IRaceCar.NumberOfLapsPassed
        { 
            get => _numberOfLapsPassed; 
            set => _numberOfLapsPassed = value > _raceTrack.NumberOfLaps ? _raceTrack.NumberOfLaps : value; 
        }

        public double FuelLevel
        {
            get => _fuelLevel;
            set => _fuelLevel = (value > _carConfiguration.FuelCapacity ? _carConfiguration.FuelCapacity : value);
        }

        public int LapCount { get => _lapCount; set => _lapCount = value; }

        public double LapSeconds { get => _lapSeconds; set => _lapSeconds = value; }

        public double PrevLapSeconds { get => _prevLapSeconds; set => _prevLapSeconds = value; }

        public double LapDistance { get => _lapDistance; set => _lapDistance = value; }

        public double PrevLapDistance { get => _prevLapDistance; set => _prevLapDistance = value; }

        public Stopwatch LapStopwatch { get => _lapStopwatch; set => _lapStopwatch = value; }

        public double CarFuel { get => _carFuel; set => _carFuel = value; }

        public async Task Run()
        {
            bool isRuning = true;
            long i = 0;
            _lapSeconds = 0d;
            _lapDistance = 0d;
            _prevLapSeconds = 0d;
            _prevLapDistance = 0d;
            _lapStopwatch = new Stopwatch();

            await Task.Run(() =>
            {
                Process(ref isRuning, ref i);
            });
        }

        void Process(ref bool isRuning, ref long i)
        {
            // Car if fuelled to its configuration at the start of the race
            _carFuel = _fuelLevel;
            _status = IRaceCar.Status.Idle;
            _lapStopwatch.Start();
            _status = IRaceCar.Status.Start;

            while (isRuning)
            {
                i++;                
                _status = IRaceCar.Status.Running;  
                _lapSeconds = ((double)_lapStopwatch.ElapsedMilliseconds) / 1000d;
                _lapDistance = _lapSeconds * _speed;                
                if (_lapSeconds > 0 && _lapSeconds > _prevLapSeconds + 1)
                { 

                    _prevLapSeconds = _lapSeconds;
                }
                if (i % 100000000 == 0)
                {
                    ShowMessage();
                }          
                if (_lapSeconds > 1)
                {
                    // update car fuel
                    if (_lapSeconds % 10 == 0)
                    {
                        UpdateFuel();
                    }
                    // Car is fuelled up to the same level each pitstop
                    if (((int)_lapSeconds) % (int)RaceTrack.TimeToPitstop.TotalSeconds == 0)
                    {
                        _carFuel = FuelLevel;
                    }
                    LapCounts(ref isRuning);
                }

                isRuning = EndProcess(isRuning);
            }
        }

        public void UpdateFuel()
        {
            if (_lapDistance > _prevLapDistance)
            { 
                var consumed = ((_lapDistance-_prevLapDistance) * _carConfiguration.FuelConsumptionPerLap) / _raceTrack.LapDistrance;
                _prevLapDistance = _lapDistance;
                _carFuel = _carFuel - consumed;
                Console.WriteLine($"consume: {consumed}, capacity: {FuelLevel}, carFuel {_carFuel}");
            }
            
            if (_carFuel > _carConfiguration.FuelCapacity)
            {
                throw new InvalidOperationException("car fuel exception");
            }
            if (_carFuel < 0)
            {
                throw new ArgumentOutOfRangeException("Fuel consumption cannot be negative.");
            }
        }

        void LapCounts(ref bool isRuning)
        {
            if (_lapSeconds % _carConfiguration.TimePerLap.TotalSeconds == 0)
            {
                if (LapCount == _raceTrack.NumberOfLaps)
                {
                    _status = IRaceCar.Status.Complete;
                    isRuning = false;
                }
                LapCount++;
                Reset();
            }
        }

        bool EndProcess(bool isRuning)
        {
            if (_lapDistance >= (_raceTrack.LapDistrance * _raceTrack.NumberOfLaps) || LapCount > _raceTrack.NumberOfLaps)
            {
                _status = IRaceCar.Status.Complete;
                isRuning = false;
            }

            return isRuning;
        }

        void Reset()
        {
            _lapStopwatch.Reset();
            _lapSeconds = 0;
            _prevLapSeconds = 0;
            _lapDistance = 0;
            _prevLapDistance = 0;
            _lapStopwatch.Start();
        }

        private void ShowMessage()
        {
            Console.WriteLine("");
            Console.WriteLine($"Lap Seconds {_lapSeconds} Prev Lap Seconds {_prevLapSeconds}");            
            Console.WriteLine($"Lap Distance {_lapDistance} Prev Lap Distance {_prevLapDistance}");            
            Console.WriteLine($"car fuel {_carFuel} Lap Count {_lapCount}");
            Console.WriteLine($"id {_raceCarId}");
            Console.WriteLine("");
        }

        IRaceCar.Status IRaceCar.GetRaceCarStatus()
        {
            return _status;
        }

    }
}
