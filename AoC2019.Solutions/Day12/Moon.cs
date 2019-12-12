namespace AoC2019.Solutions.Day12
{
    using System;

    public struct Moon
    {
        public Moon(int x, int y, int z)
        {
            this.Position = (x, y, z);
            this.Velocity = (0, 0, 0);
        }

        public (int X, int Y, int Z) Position { get; set; }

        public (int X, int Y, int Z) Velocity { get; set; }

        public MoonDimension GetDimension(int dimension)
        {
            switch (dimension)
            {
                case 0:
                    return new MoonDimension(this.Position.X, this.Velocity.X);
                case 1:
                    return new MoonDimension(this.Position.Y, this.Velocity.Y);
                case 2:
                    return new MoonDimension(this.Position.Z, this.Velocity.Z);
            }

            throw new ArgumentException();
        }

        public void Move()
        {
            this.Position = (
                this.Position.X + this.Velocity.X,
                this.Position.Y + this.Velocity.Y,
                this.Position.Z + this.Velocity.Z);
        }
    }
}
