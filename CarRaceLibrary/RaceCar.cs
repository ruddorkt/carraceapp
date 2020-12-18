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

            double prevLapSeconds = 0d;
            double prevLapDistance = 0d;

            while (isRuning)
            {
                i++;                
                _status = IRaceCar.Status.Running;  
                lapSeconds = ((double)lapStopWatch.ElapsedMilliseconds) / 1000d;
                lapDistance = lapSeconds * _speed;                
                if (lapSeconds > 0 && lapSeconds > prevLapSeconds + 1)
                { 

                    prevLapSeconds = lapSeconds;
                }
                if (i % 100000000 == 0)
                {
                    ShowMessage(lapSeconds, prevLapSeconds, lapDistance, prevLapDistance, _carFuel, _lapCount);
                }          
                if (lapSeconds > 1)
                {
                    // update car fuel
                    if (lapSeconds % 10 == 0)
                    {
                        UpdateFuel(ref lapDistance, ref prevLapDistance, ref _carFuel);
                    }
                    // Car is fuelled up to the same level each pitstop
                    if (((int)lapSeconds) % (int)RaceTrack.TimeToPitstop.TotalSeconds == 0)
                    {
                        _carFuel = FuelLevel;
                    }
                    LapCounts(ref isRuning, ref lapSeconds, ref prevLapSeconds, ref lapDistance, ref prevLapDistance, ref lapStopWatch);
                }

                isRuning = EndProcess(isRuning, lapDistance);
            }
        }

        private void UpdateFuel(ref double lapDistance,ref double prevLapDistance, ref double _carFuel)
        {
            if (lapDistance > prevLapDistance)
            { 
                var consumed = ((lapDistance-prevLapDistance) * _carConfiguration.FuelConsumptionPerLap) / _raceTrack.LapDistrance;
                prevLapDistance = lapDistance;
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

        private void LapCounts(ref bool isRuning, ref double lapSeconds, ref double preLapSeconds, ref double lapDistance, ref double prevLapDistance, ref Stopwatch lapStopWatch)
        {
            if (lapSeconds % _carConfiguration.TimePerLap.TotalSeconds == 0)
            {
                if (LapCount == _raceTrack.NumberOfLaps)
                {
                    _status = IRaceCar.Status.Complete;
                    isRuning = false;
                }
                LapCount++;
                Reset(ref lapSeconds, ref preLapSeconds, ref lapDistance, ref prevLapDistance, ref lapStopWatch);
            }
        }

        private bool EndProcess(bool isRuning, double lapDistance)
        {
            if (lapDistance >= (_raceTrack.LapDistrance * _raceTrack.NumberOfLaps) || LapCount > _raceTrack.NumberOfLaps)
            {
                _status = IRaceCar.Status.Complete;
                isRuning = false;
            }

            return isRuning;
        }

        private void Reset(ref double lapSeconds, ref double prevLapSeconds, ref double lapDistance, ref double prevLapDistance, ref Stopwatch lapStopWatch)
        {
            lapStopWatch.Reset();
            lapSeconds = 0;
            prevLapSeconds = 0;
            lapDistance = 0;
            prevLapDistance = 0;
            lapStopWatch.Start();
        }

        private void ShowMessage(double lapSeconds, double prevLapSeconds, double lapDistance, double prevLapDistance, double _carFuel, double _lapCount)
        {
            Console.WriteLine("");
            Console.WriteLine($"Lap Seconds {lapSeconds} Prev Lap Seconds {prevLapSeconds}");            
            Console.WriteLine($"Lap Distance {lapDistance} Prev Lap Distance {prevLapDistance}");            
            Console.WriteLine($"car fuel {_carFuel} Lap Count {_lapCount}");
            Console.WriteLine("");
        }

        public IRaceCar.Status GetRaceCarStatus()
        {
            return _status;
        }
    }
}
