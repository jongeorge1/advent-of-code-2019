namespace AoC2019.Solutions.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using AoC2019.Solutions.IntCodeVm;

    public class Part02 : ISolution
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
            vm.InputBuffer.Post(1);

            int direction = 0; // Facing up
            var location = new Point(0, 0);
            var paintedLocations = new HashSet<Point>(); // So we won't get dupes

            // Initialise with current location as white
            var hullLocationColours = new Dictionary<Point, long>()
            {
                { location, 1 },
            };

            Task vmTask = vm.ExecuteAsync();

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
                if (hullLocationColours.TryGetValue(location, out colour))
                {
                    vm.InputBuffer.Post(colour);
                }
                else
                {
                    vm.InputBuffer.Post(0);
                }
            }

            // Build the visualisation; remove black locations from the list to ensure we only visualise what's needed.
            StringBuilder display = this.BuildVisualisation(hullLocationColours);

            return display.ToString();
        }

        private StringBuilder BuildVisualisation(Dictionary<Point, long> hullLocationColours)
        {
            var display = new StringBuilder();
            Point[] whiteLocations = hullLocationColours.Where(x => x.Value == 1).Select(x => x.Key).ToArray();

            // Get the bounds for the registration
            int top = whiteLocations.Max(p => p.Y);
            int left = whiteLocations.Min(p => p.X);
            int bottom = whiteLocations.Min(p => p.Y);
            int right = whiteLocations.Max(p => p.X);

            for (int y = top; y >= bottom; y--)
            {
                for (int x = left; x <= right; x++)
                {
                    if (whiteLocations.Contains(new Point(x, y)))
                    {
                        display.Append("X");
                    }
                    else
                    {
                        display.Append(" ");
                    }
                }

                display.AppendLine();
            }

            return display;
        }
    }
}
