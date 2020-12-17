using CarRaceLibrary;
using System;
using System.Threading.Tasks;

namespace CarRaceApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RaceTrack raceTrack = new RaceTrack(1000, 3, new TimeSpan(TimeSpan.TicksPerSecond * 20));
            long seconds = TimeSpan.TicksPerSecond * 60;
            TimeSpan timePerLap = new TimeSpan(seconds); // 60 seconds per lap
            CarConfiguration carConiguration = new CarConfiguration(raceTrack, 20, timePerLap, 10);
            RaceCar car1 = new RaceCar(carConiguration);
            await car1.Run();
        }
    }
}
