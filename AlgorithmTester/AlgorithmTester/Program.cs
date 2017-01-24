using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace AlgorithmTester
{
    class Program
    {
        const float XaccelOffset = 10166;
        const float YaccelOffset = -9868;
        const float ZaccelOffset = -5096;

        static void Main(string[] args)
        {
            var textLines = File.ReadAllLines(@"C:/Test/TestData.csv").ToList();
            var dataPoints = textLines.ConvertAll(i => i.Split(',').ToList().ConvertAll(a => float.Parse(a)));

            foreach (var dataPoint in dataPoints)
            {
                Thread.Sleep(100);

            }
        }
    }
}
