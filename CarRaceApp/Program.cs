// **********************************************
// Copyright (c) KANG TAN All rights reserved.   
// **********************************************
using CarRaceLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
            CarConfiguration carConiguration = new CarConfiguration(raceTrack, 20, timePerLap, 40);
            RaceCar car1 = new RaceCar(carConiguration);
            RaceCar car2 = new RaceCar(carConiguration);
            List<RaceCar> raceCars = new List<RaceCar>();
            Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            raceCars.Add(car1);
            raceCars.Add(car2);
            //await RunAll(raceCars);
            await RunAllParalle(raceCars);
        }


        public static async Task RunAllParalle(List<RaceCar> raceCars)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Task[] ary = new Task[raceCars.Count];
            for (int i = 0; i < raceCars.Count; i++)
            {
                ary[i] = raceCars[i].Run();
            }
            await Task.WhenAll(
                ary
            );
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime);
        }

        public static async Task RunAll(List<RaceCar> raceCars)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var c in raceCars)
            {
                await c.Run();
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime);
        }
    }
}
