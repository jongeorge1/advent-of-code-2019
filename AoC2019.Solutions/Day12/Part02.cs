namespace AoC2019.Solutions.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AoC2019.Solutions.Helpers;

    public class Part02 : ISolution
    {
        public const int TargetSteps = 1000;

        public string Solve(string data)
        {
            IEnumerable<Moon> moons = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] { ',', '<', '>', '=', 'x', 'y', 'z', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                .Select(m => new Moon(m[0], m[1], m[2]));

            IEnumerable<Task<long>> periods = Enumerable.Range(0, 3)
                .Select(x => this.FindPeriodAsync(moons.Select(m => m.GetDimension(x)).ToArray()));

            Task.WhenAll(periods).Wait();

            return Numeric.LeastCommonMultiple(periods.Select(x => x.Result)).ToString();
        }

        private Task<long> FindPeriodAsync(MoonDimension[] dimension)
        {
            // To find the period we logically just need to look for the first time that everything is exactly where we
            // started. This means velocities as well - if the velocities are different, then the length of the next period
            // would be different, and that's not what we're looking for.
            return Task.Run(() =>
            {
                var initialState = (MoonDimension[])dimension.Clone();

                for (long steps = 1; ; steps++)
                {
                    dimension.UpdateVelocities();
                    dimension.UpdatePositions();

                    if (dimension.IsSameAs(initialState))
                    {
                        return steps;
                    }
                }
            });
        }
    }
}
