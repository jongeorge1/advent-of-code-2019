namespace AoC2019.Solutions.IntCodeVm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public static void Connect(this AsyncIntCodeVm[] vms, bool circular)
        {
            for (int i = 1; i < vms.Length; i++)
            {
                vms[i].InputBuffer = vms[i - 1].OutputBuffer;
            }

            if (circular)
            {
                vms[0].InputBuffer = vms.Last().OutputBuffer;
            }
        }

        public static void InitialiseInputs(this AsyncIntCodeVm[] vms, int[] inputs)
        {
            if (vms.Length != inputs.Length)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < vms.Length; i++)
            {
                vms[i].InputBuffer.Post(inputs[i]);
            }
        }

        public static Task ExecuteAllAsync(this AsyncIntCodeVm[] vms)
        {
            IEnumerable<Task> tasks = vms.Select(vm => vm.ExecuteAsync());

            return Task.WhenAll(tasks);
        }
    }
}
