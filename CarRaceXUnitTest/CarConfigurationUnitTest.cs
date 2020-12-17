using CarRaceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarRaceXUnitTest
{
    public class CarConfigurationUnitTest : TestBase
    {
        [Fact]
        public void CarConfigurationGetPropertyTest()
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
            // assert
            Assert.Equal(trackId, carConfig.RaceTrack.TrackId);
            Assert.Equal(fuelCapacity, carConfig.FuelCapacity);
            Assert.Equal(timePerLap, carConfig.TimePerLap);
            Assert.Equal(fuelConsumptionPerLap, carConfig.FuelConsumptionPerLap);
            Assert.IsType<Guid>(carConfig.ConfigurationId);
        }

        [Fact]
        public void CarConfigurationSetPropertyTest()
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
            carConfig.FuelCapacity += 10;
            carConfig.TimePerLap.Add(timePerLap);
            carConfig.FuelConsumptionPerLap += 20;
            // assert
            Assert.Equal(trackId, carConfig.RaceTrack.TrackId);
            Assert.Equal(fuelCapacity + 10, carConfig.FuelCapacity);
            Assert.Equal(timePerLap.Add(timePerLap), carConfig.TimePerLap);
            Assert.Equal(fuelConsumptionPerLap + 20, carConfig.FuelConsumptionPerLap);
            Assert.IsType<Guid>(carConfig.ConfigurationId);
        }

    }
}
