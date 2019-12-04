namespace AoC2019.Solutions.Day04
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int[] range = input
                .Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int validPasswordCount = Enumerable.Range(range[0], range[1] - range[0] + 1)
                .Count(this.IsValidPassword);

            return validPasswordCount.ToString();
        }

        private bool IsValidPassword(int input)
        {
            int lastDigit = 10;
            bool pairFound = false;

            // This loop processes the digits one at a time. We keep track of the last digit as well
            // as the current one as we need it for both parts of the validity rule.
            // Note that we process the digits in reverse order...
            while (input > 0)
            {
                // Extract the current digit
                int thisDigit = input % 10;

                // The check is > rather than <= because we're working backwards. If it fails, we can exit the loop
                // immediately.
                if (thisDigit > lastDigit)
                {
                    return false;
                }

                // If this digit matches the last, we've satisified the "pair" part of the requirement.
                pairFound = pairFound || (thisDigit == lastDigit);

                // Get ready for the next iteration.
                input /= 10;
                lastDigit = thisDigit;
            }

            return pairFound;
        }
    }
}
