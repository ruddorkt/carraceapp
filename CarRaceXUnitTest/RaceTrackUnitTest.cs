using CarRaceLibrary;
using System;
using Xunit;

namespace CarRaceXUnitTest
{
    public class RaceTestUnitTest : TestBase
    {
        [Fact]
        public void RaceTrackGetPropertyTest()
        {
            // arrange            
            double lapDistrance;
            int numberOfLaps;
            TimeSpan timeToPitstop;
            RaceTrack rt;
            // act
            TestBase.GetRaceTrackEntity(out lapDistrance, out numberOfLaps, out timeToPitstop, out rt);
            // assert
            Assert.Equal(lapDistrance, rt.LapDistrance);
            Assert.Equal(numberOfLaps, rt.NumberOfLaps);
            Assert.Equal(timeToPitstop, RaceTrack.TimeToPitstop);
            Assert.IsType<Guid>(rt.TrackId);
        }

        [Fact]
        public void RaceTrackSetPropertyTest()
        {
            // arrange            
            double lapDistrance;
            int numberOfLaps;
            TimeSpan timeToPitstop;
            RaceTrack rt;
            // act
            TestBase.GetRaceTrackEntity(out lapDistrance, out numberOfLaps, out timeToPitstop, out rt);
            rt.LapDistrance += 60;
            rt.NumberOfLaps += 20;
            RaceTrack.TimeToPitstop.Add(timeToPitstop);
            // assert
            Assert.Equal(lapDistrance + 60, rt.LapDistrance);
            Assert.Equal(numberOfLaps + 20, rt.NumberOfLaps);
            Assert.Equal(timeToPitstop.Add(timeToPitstop), RaceTrack.TimeToPitstop);
            Assert.IsType<Guid>(rt.TrackId);
        }
    }
}
