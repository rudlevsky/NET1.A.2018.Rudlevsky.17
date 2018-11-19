using System;
using static StreamsDemo.StreamsExtension;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var source = ConfigurationManager.AppSettings["OutputText.txt"];
            //var destination = ConfigurationManager.AppSettings["test.txt"];

            string source = "OutputText.txt";
            string destination = "test.txt";

            Console.WriteLine($"ByteCopy() done. Total bytes: {ByByteCopy(source, destination)}");

            Console.WriteLine($"InMemoryByteCopy() done. Total bytes: {InMemoryByByteCopy(source, destination)}");

            Console.WriteLine($"ByBlockCopy() done. Total bytes: {ByBlockCopy(source, destination)}");

            Console.WriteLine($"InMemoryBlockCopy() done. Total bytes: {InMemoryByBlockCopy(source, destination)}");

            Console.WriteLine($"BufferedCopy() done. Total bytes: {BufferedCopy(source, destination)}");

            Console.WriteLine($"ByLineCopy() done. Total lines: {ByLineCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            //etc
            Console.ReadLine();
        }
    }
}
