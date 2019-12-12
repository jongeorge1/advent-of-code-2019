namespace AoC2019.Solutions.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using AoC2019.Solutions.IntCodeVm;

    public class Part01 : ISolution
    {
        private static readonly Point[] Directions = new Point[]
        {
            new Point(0, 1),
            new Point(1, 0),
            new Point(0, -1),
            new Point(-1, 0),
        };

        public string Solve(string data)
        {
            var vm = new AsyncIntCodeVm(data);
            vm.InputBuffer.Post(0);

            int direction = 0; // Facing up
            var location = new Point(0, 0);
            var paintedLocations = new HashSet<Point>(); // So we won't get dupes
            var hullLocationColours = new Dictionary<Point, long>();

            Task vmTask = vm.ExecuteAsync();

            // Assuming we'll always get outputs in pairs. If not, we'd need to check between the two Receive calls.
            while (!vmTask.IsCompleted)
            {
                // Get the colour
                long colour = vm.OutputBuffer.Receive(TimeSpan.FromSeconds(1));

                // Paint
                hullLocationColours[location] = colour;
                paintedLocations.Add(location);

                // Get the location change
                long turn = vm.OutputBuffer.Receive(TimeSpan.FromSeconds(1));

                // Turn and move
                direction = (direction + (turn == 0 ? -1 : 1) + 4) % 4;
                location = new Point(location.X + Directions[direction].X, location.Y + Directions[direction].Y);

                // Post colour of current location to VM
                hullLocationColours.TryGetValue(location, out colour); // If there is no value, colour gets set to default(long), i.e. 0
                vm.InputBuffer.Post(colour);
            }

            return paintedLocations.Count.ToString();
        }
    }
}
