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

        //private Stopwatch stopWatch = new Stopwatch();
        private Stopwatch lapStopWatch = new Stopwatch();
        private int _numberOfLapsPassed;

        IRaceCar.Status _status;
        private double _carFuel;
        private int _lapCount = 0;

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
        public int NumberOfLapsPassed 
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
        
        public async Task Run()
        {
            bool isRuning = true;
            long i = 0;
            double lapSeconds = 0d;
            double lapDistance = 0d;
            await Task.Run(() =>
            {
                Process(ref isRuning, ref i, ref lapSeconds, ref lapDistance);
            });
        }

        private void Process(ref bool isRuning, ref long i, ref double lapSeconds, ref double lapDistance)
        {
            // Car if fuelled to its configuration at the start of the race
            _carFuel = _fuelLevel;
            _status = IRaceCar.Status.Idle;
            lapStopWatch.Start();
            _status = IRaceCar.Status.Start;
            while (isRuning)
            {
                i++;
                lapSeconds = ((double)lapStopWatch.ElapsedMilliseconds) / 1000d;
                lapDistance = lapSeconds * _speed;
                if (i % 100000000 == 0)
                {
                    Console.WriteLine($"car fuel {_carFuel}");
                    Console.WriteLine($"Lap Distance {lapDistance}");
                    Console.WriteLine($"Lap Seconds {lapSeconds}");
                    Console.WriteLine($"Lap Count {_lapCount}");
                }
                // update car fuel
                if (lapDistance != 0 && lapSeconds % 10 == 0)
                {
                    var consumed = (lapDistance * _carConfiguration.FuelConsumptionPerLap) / _raceTrack.LapDistrance;
                    _carFuel = _carFuel - consumed;
                    Console.WriteLine($"consume: {consumed}, capacity: {FuelLevel}, carFuel {_carFuel}");
                    if ((int)_carFuel > _carConfiguration.FuelCapacity)
                    {
                        throw new InvalidOperationException("car fuel exception");
                    }
                }
                _status = IRaceCar.Status.Running;
                // Car is fuelled up to the same level each pitstop
                if (lapSeconds > 1 && ((int)lapSeconds) % (int)RaceTrack.TimeToPitstop.TotalSeconds == 0)
                {
                    _carFuel = FuelLevel;
                }
                if (lapSeconds > 1)
                {
                    if (lapSeconds % _carConfiguration.TimePerLap.TotalSeconds == 0)
                    {
                        if (LapCount == _raceTrack.NumberOfLaps)
                        {
                            _status = IRaceCar.Status.Complete;
                            isRuning = false;
                        }
                        LapCount++;
                        lapStopWatch.Reset();
                        lapSeconds = 0;
                        lapDistance = 0;
                        lapStopWatch.Start();
                    }
                }

                if (lapDistance >= (_raceTrack.LapDistrance * _raceTrack.NumberOfLaps) || LapCount > _raceTrack.NumberOfLaps)
                {
                    _status = IRaceCar.Status.Complete;
                    isRuning = false;
                }
            }
        }

        public IRaceCar.Status GetRaceCarStatus()
        {
            return _status;
        }
    }
}
