namespace AoC2019.Solutions.IntCodeVm
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks.Dataflow;

    public static class AsyncIntCodeVmExtensions
    {
        public static long[] Execute(this AsyncIntCodeVm vm, long inputValue)
        {
            vm.InputBuffer.Post(inputValue);

            vm.ExecuteAsync().Wait();

            if (vm.OutputBuffer.TryReceiveAll(out IList<long> items))
            {
                return items.ToArray();
            }

            return new long[0];
        }
    }
}
