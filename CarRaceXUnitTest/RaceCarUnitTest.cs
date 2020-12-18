using CarRaceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRaceXUnitTest
{
    public class RaceCarUnitTest : TestBase
    {
        [Fact]
        public async Task RaceCarFuelArgumentOutOfRangeExceptionTest()
        {
            // arrange           
            double lapDistrance;
            int numberOfLaps;
            TimeSpan timeToPitstop;
            RaceTrack raceTrack;
            TestBase.GetRaceTrackEntity(out lapDistrance, out numberOfLaps, out timeToPitstop, out raceTrack);
            Guid trackId;
            double fuelCapacity, fuelConsumptionPerLap;
            TimeSpan timePerLap;
            CarConfiguration carConfig;
            // act
            TestBase.GetCarConfigurationEntity(raceTrack, out trackId, out fuelCapacity, out timePerLap, out fuelConsumptionPerLap, out carConfig);
            var raceCar = new RaceCar(carConfig);
            double lapDistance = 2d;
            double prevLapDistance = 0d;
            double carFuel = -10d;

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => raceCar.UpdateFuel(ref lapDistance, ref prevLapDistance, ref carFuel));
        }

        [Fact]
        public async Task RaceCarFuelInvalidOperationExceptionTest()
        {
            // arrange           
            double lapDistrance;
            int numberOfLaps;
            TimeSpan timeToPitstop;
            RaceTrack raceTrack;
            TestBase.GetRaceTrackEntity(out lapDistrance, out numberOfLaps, out timeToPitstop, out raceTrack);
            Guid trackId;
            double fuelCapacity, fuelConsumptionPerLap;
            TimeSpan timePerLap;
            CarConfiguration carConfig;

            // act
            TestBase.GetCarConfigurationEntity(raceTrack, out trackId, out fuelCapacity, out timePerLap, out fuelConsumptionPerLap, out carConfig);
            var raceCar = new RaceCar(carConfig);
            double lapDistance = 2d;
            double prevLapDistance = 0d;
            double carFuel = 30d;

            // assert
            Assert.Throws<InvalidOperationException>(() => raceCar.UpdateFuel(ref lapDistance, ref prevLapDistance, ref carFuel));
        }
    }
}
