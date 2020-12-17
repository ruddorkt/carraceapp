using CarRaceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceXUnitTest
{
    public class TestBase : IDisposable
    {
        public TestBase()
        {

        }

        public static void GetRaceTrackEntity(out double lapDistrance, out int numberOfLaps, out TimeSpan timeToPitstop, out RaceTrack rt)
        {
            lapDistrance = 40;
            numberOfLaps = 10;
            timeToPitstop = new TimeSpan(600);
            rt = new RaceTrack(lapDistrance, numberOfLaps, timeToPitstop);
        }

        public static void GetCarConfigurationEntity(RaceTrack raceTrack, out Guid trackId, out double fuelCapacity, out TimeSpan timePerLap, out double fuelConsumptionPerLap, out CarConfiguration carConfig)
        {
            trackId = raceTrack.TrackId;
            fuelCapacity = 20;
            timePerLap = new TimeSpan(1800);
            fuelConsumptionPerLap = 5;
            carConfig = new CarConfiguration(raceTrack, fuelCapacity, timePerLap, fuelConsumptionPerLap);
        }

        public void Dispose()
        {
            
        }
    }
}
