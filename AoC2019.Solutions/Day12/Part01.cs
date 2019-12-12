namespace AoC2019.Solutions.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Part01 : ISolution
    {
        public const int TargetSteps = 1000;

        public string Solve(string data)
        {
            Moon[] moons = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] { ',', '<', '>', '=', 'x', 'y', 'z', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                .Select(m => new Moon(m[0], m[1], m[2]))
                .ToArray();

            MoonDimension[] dimensionX = moons.Select(x => x.GetDimension(0)).ToArray();
            MoonDimension[] dimensionY = moons.Select(x => x.GetDimension(1)).ToArray();
            MoonDimension[] dimensionZ = moons.Select(x => x.GetDimension(2)).ToArray();

            Task.WaitAll(
                this.SimulateMovementAsync(dimensionX),
                this.SimulateMovementAsync(dimensionY),
                this.SimulateMovementAsync(dimensionZ));

            Moon[] finalStates = (dimensionX, dimensionY, dimensionZ).Combine();

            return finalStates.TotalEnergy().ToString();
        }

        private Task SimulateMovementAsync(MoonDimension[] dimension)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < TargetSteps; i++)
                {
                    dimension.UpdateVelocities();
                    dimension.UpdatePositions();
                }
            });
        }
    }
}
