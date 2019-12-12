namespace AoC2019.Solutions.Day12
{
    using System.Diagnostics;

    [DebuggerDisplay("{Position}, {Velocity}")]
    public struct MoonDimension
    {
        public MoonDimension(int position, int velocity)
        {
            this.Position = position;
            this.Velocity = velocity;
        }

        public int Position { get; set; }

        public int Velocity { get; set; }

        public void Move()
        {
            this.Position += this.Velocity;
        }
    }
}
