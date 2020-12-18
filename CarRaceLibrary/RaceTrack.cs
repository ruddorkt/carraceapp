// **********************************************
// Copyright (c) RUDDOR_KT All rights reserved.   
// **********************************************
using System;

namespace CarRaceLibrary
{
    public class RaceTrack : IRaceTrack
    {
        private readonly Guid _trackId;
        private double _lapDistance;
        private int _numberOfLaps;
        private static TimeSpan _timeToPistop = new TimeSpan(TimeSpan.TicksPerSecond * 30); // 30 seconds

        public RaceTrack(double lapDistance, int numberOfLaps, TimeSpan timeToPitstop)
        {
            _lapDistance = lapDistance;
            _numberOfLaps = numberOfLaps;
            _timeToPistop = timeToPitstop;
            _trackId = Guid.NewGuid();
        }

        public RaceTrack(double lapDistance, int numberOfLaps)
        {
            _lapDistance = lapDistance;
            _numberOfLaps = numberOfLaps;
            _trackId = Guid.NewGuid();
        }

        public Guid TrackId { get => _trackId; }
        public double LapDistrance { get => _lapDistance; set => _lapDistance = value; }
        public int NumberOfLaps { get => _numberOfLaps; set => _numberOfLaps = value; }
        public static TimeSpan TimeToPitstop { get => _timeToPistop; set => _timeToPistop = value; }
    }
}
