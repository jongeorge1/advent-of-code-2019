namespace AoC2019.Solutions.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Part02 : ISolution
    {
        private static readonly Dictionary<char, Point> Vectors = new Dictionary<char, Point>
        {
            { 'U', new Point(0, 1) },
            { 'D', new Point(0, -1) },
            { 'L', new Point(-1, 0) },
            { 'R', new Point(1, 0) },
        };

        public string Solve(string input)
        {
            List<Point>[] lines = input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(path => path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                .Select(path => this.BuildPoints(path))
                .ToArray();

            // Find common points
            IEnumerable<Point> commonPoints = lines[0].Intersect(lines[1]);

            // Distances to common points. Add 2 to take account of the fact that each array is missing the origin point.
            int minimumCombinedSteps = commonPoints.Min(x => lines[0].IndexOf(x) + lines[1].IndexOf(x) + 2);

            return minimumCombinedSteps.ToString();
        }

        private List<Point> BuildPoints(string[] path)
        {
            var result = new List<Point>();
            var currentLocation = new Point(0, 0);

            foreach (string current in path)
            {
                int distance = int.Parse(current.Substring(1));

                Point vector = Vectors[current[0]];

                for (int i = 0; i < distance; i++)
                {
                    currentLocation = new Point(currentLocation.X + vector.X, currentLocation.Y + vector.Y);
                    result.Add(currentLocation);
                }
            }

            return result;
        }
    }
}
