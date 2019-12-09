namespace AoC2019.Solutions.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            bool testMode = data.Length < 100;
            int width = testMode ? 2 : 25;
            int height = testMode ? 2 : 6;

            int layerSize = width * height;

            char[][] layers = data
                .ToCharArray()
                .Select((layer, index) => (index, layer))
                .GroupBy(pixel => pixel.index / layerSize)
                .Select(group => group.Select(l => l.layer).ToArray())
                .ToArray();

            string[] result = Enumerable.Range(0, layerSize)
                .Select(pixelNumber => layers.Select(layer => layer[pixelNumber]).First(pixel => pixel != '2'))
                .Select((pixel, index) => (index, pixel))
                .GroupBy(pixel => pixel.index / width)
                .Select(group => string.Join(string.Empty, group.Select(pixels => pixels.pixel)))
                .ToArray();

            return string.Join(Environment.NewLine, result).Replace("1", "X").Replace("0", " ");
        }
    }
}
