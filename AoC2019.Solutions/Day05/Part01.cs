namespace AoC2019.Solutions.Day05
{
    using System.Linq;
    using AoC2019.Solutions.IntCodeVm;

    public class Part01 : ISolution
    {
        public string Solve(string data)
        {
            var vm = new AsyncIntCodeVm(data);
            long[] outputs = vm.Execute(1);

            return outputs.LastOrDefault().ToString();
        }
    }
}
