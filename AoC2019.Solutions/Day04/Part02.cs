namespace AoC2019.Solutions.Day04
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
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
            int repeatCount = 0;
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

                // Keep track of how many matching digits we have encountered. If the digit is
                // different, see how many of the preceeding digits matched. If it's the last
                // two, we've found a pair and met that part of the requirements. If it's more
                // than that, we don't count it as a match.
                if (thisDigit == lastDigit)
                {
                    repeatCount++;
                }
                else
                {
                    if (repeatCount == 1)
                    {
                        pairFound = true;
                    }

                    repeatCount = 0;
                }

                // Get ready for the next iteration.
                input /= 10;
                lastDigit = thisDigit;
            }

            // The loop doesn't check to see if the last two digits are a pair, so
            // we need to do that here.
            return pairFound || (repeatCount == 1);
        }
    }
}
