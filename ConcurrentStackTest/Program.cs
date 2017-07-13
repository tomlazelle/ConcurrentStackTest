using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;

namespace ConcurrentStackTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var wrappers = Builder<WrapperTest>.CreateListOfSize(10).Build().ToArray();

            var evens = Guid.NewGuid();
            var odds = Guid.NewGuid();



            for (int i = 1; i < 11; i++)
            {

                if (i % 2 == 0)
                {
                    DataStream.Append(evens, wrappers[i-1]);
                }
                else
                {
                    DataStream.Append(odds, wrappers[i-1]);
                }
            }

            Console.WriteLine("now do even");
            DataStream.Persist(evens);
            Console.WriteLine("now do odd");
            DataStream.Persist(odds);

            Console.Read();
        }
    }
}