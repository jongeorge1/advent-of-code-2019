namespace AoC2019.Solutions.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class MoonDimensionExtensions
    {
        public static void UpdateVelocities(this MoonDimension[] moons)
        {
            for (int moon1Index = 0; moon1Index < moons.Length; moon1Index++)
            {
                for (int moon2Index = moon1Index + 1; moon2Index < moons.Length; moon2Index++)
                {
                    int change = moons[moon1Index].Position.CompareTo(moons[moon2Index].Position);

                    moons[moon1Index].Velocity -= change;
                    moons[moon2Index].Velocity += change;
                }
            }
        }

        public static int UpdatePositions(this MoonDimension[] moons)
        {
            for (int i = 0; i < moons.Length; i++)
            {
                moons[i].Move();
            }

            return 0;
        }

        public static bool IsSameAs(this MoonDimension[] actual, MoonDimension[] expected)
        {
            return Enumerable.Range(0, actual.Length).All(i => actual[i].Position == expected[i].Position
                && actual[i].Velocity == expected[i].Velocity);
        }

        public static Moon[] Combine(this (MoonDimension[] X, MoonDimension[] Y, MoonDimension[] Z) dimensions)
        {
            var result = new Moon[dimensions.X.Length];
            for (int i = 0; i < dimensions.X.Length; i++)
            {
                result[i] = new Moon
                {
                    Position = (dimensions.X[i].Position, dimensions.Y[i].Position, dimensions.Z[i].Position),
                    Velocity = (dimensions.X[i].Velocity, dimensions.Y[i].Velocity, dimensions.Z[i].Velocity),
                };
            }

            return result;
        }
    }
}
