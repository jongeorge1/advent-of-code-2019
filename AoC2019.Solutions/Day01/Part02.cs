namespace AoC2019.Solutions.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .Select(this.CalculateTotalFuelRequirementForMass)
                .Sum()
                .ToString();
        }

        public int CalculateTotalFuelRequirementForMass(int mass)
        {
            int totalFuelRequirement = 0;

            int fuelRequirement = this.CalculateFuelRequirementForMass(mass);

            while (fuelRequirement > 0)
            {
                totalFuelRequirement += fuelRequirement;

                fuelRequirement = this.CalculateFuelRequirementForMass(fuelRequirement);
            }

            return totalFuelRequirement;
        }

        public int CalculateFuelRequirementForMass(int mass)
        {
            return (mass / 3) - 2;
        }
    }
}
