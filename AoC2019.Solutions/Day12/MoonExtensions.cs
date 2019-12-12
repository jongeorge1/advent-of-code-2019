namespace AoC2019.Solutions.Day12
{
    using System;
    using System.Linq;
    using AoC2019.Solutions.Helpers;

    public static class MoonExtensions
    {
        public static int TotalEnergy(this Moon[] moons)
        {
            return moons.Sum(x => x.PotentialEnergy() * x.KineticEnergy());
        }

        public static int PotentialEnergy(this Moon moon)
        {
            return Distance.Manhattan(moon.Position);
        }

        public static int KineticEnergy(this Moon moon)
        {
            return Distance.Manhattan(moon.Velocity);
        }
    }
}
