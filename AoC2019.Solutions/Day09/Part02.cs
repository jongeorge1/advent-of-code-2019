namespace AoC2019.Solutions.Day09
{
    using System.Linq;
    using AoC2019.Solutions.IntCodeVm;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            var vm = new AsyncIntCodeVm(data);
            long[] output = vm.Execute(2);

            return output.Last().ToString();
        }
    }
}
