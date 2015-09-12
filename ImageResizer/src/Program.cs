using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSizer
{
    class Program
    {
        static readonly int[] defaultSizes = { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };

        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("Usage: resize filename [size1 [size2 [size3 [...]]]]");
                    Console.WriteLine("Default sizes: " + string.Join(" ", defaultSizes));
                    break;
                case 1:
                    Resizer.Resize(args[0], defaultSizes);
                    break;
                default:
                    Resizer.Resize(args[0], args.Skip(1).Select(a => Convert.ToInt32(a)));
                    break;
            }
        }
    }
}
