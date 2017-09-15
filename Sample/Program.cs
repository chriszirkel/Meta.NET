using Meta.NET;
using System;
using System.Threading.Tasks;

namespace CoreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter URL:");
            var url = Console.ReadLine();

            var parser = new Parser();
            var metaData = parser.ParseUrlAsync(url).GetAwaiter().GetResult();

            Console.WriteLine();

            foreach (var item in metaData)
            {
                Console.WriteLine(item.Key + ":\t" + item.Value);
            }

            Console.ReadLine();
        }
    }
}