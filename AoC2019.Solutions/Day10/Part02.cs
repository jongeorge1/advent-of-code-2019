namespace AoC2019.Solutions.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            Point[] points = data
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany((row, rowNum) => row.ToCharArray().Select((col, colNum) => (new Point(colNum, rowNum), col)))
                .Where(x => x.col == '#')
                .Select(x => x.Item1)
                .ToArray();

            // Repeat of part 1 code to find the monitoring station, although slightly tweaked to obtain the Point rather
            // than just the asteroid count.
            Point monitoringStation = points.OrderByDescending(
                source => points.Where(t => t != source)
                    .Select(t => Math.Atan2(t.X - source.X, t.Y - source.Y))
                    .Distinct()
                    .Count())
                .First();

            // Now get all the other stations angles and distances from this location
            var targets = points.Where(p => p != monitoringStation)
                .Select(p =>
                (
                    Location: p,
                    Heading: Math.Atan2(p.X - monitoringStation.X, p.Y - monitoringStation.Y),
                    Distance: Math.Sqrt(Math.Pow(p.X - monitoringStation.X, 2) + Math.Pow(p.Y - monitoringStation.Y, 2))))
                .GroupBy(x => x.Heading)
                .Select(x => (Heading: x.Key, Asteroids: x.OrderBy(a => a.Distance).ToArray()))
                .OrderByDescending(g => g.Heading) // Because up is Math.Pi, and we're going clockwise which is the sweep from Math.Pi to -Math.Pi
                .ToList();

            // We're going to repeatedly loop over the array of target angles. For each target angle we need to track the
            // index of the next target (i.e. how many asteroids we've vaporised on that heading).
            var angleDepthPointers = Enumerable.Range(0, targets.Count).ToDictionary(x => x, _ => 0);
            int anglePointer = 0;
            int vaporisationCount = 0;

            // Vaporise 199 asteroids.
            while (vaporisationCount < 199)
            {
                int currentDepthPointer = angleDepthPointers[anglePointer];

                // Check we haven't already vaporised all the asteroids on this heading.
                if (currentDepthPointer < targets[anglePointer].Asteroids.Length)
                {
                    // We haven't, so count it and increase the depth pointer for this heading
                    vaporisationCount++;
                    angleDepthPointers[anglePointer] = currentDepthPointer + 1;
                }

                // Move on, or back to the start if necessary.
                anglePointer = (anglePointer + 1) % targets.Count;
            }

            // We've vaporised 199 asteroids; the one we're now pointing at is #200.
            Point target = targets[anglePointer].Asteroids[angleDepthPointers[anglePointer]].Location;

            return ((target.X * 100) + target.Y).ToString();
        }
    }
}
