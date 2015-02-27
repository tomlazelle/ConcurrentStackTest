using System;
using System.Collections.Generic;
using FizzWare.NBuilder;

namespace ConcurrentStackTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<WrapperTest> wrappers = Builder<WrapperTest>.CreateListOfSize(10).Build();

            var isFirst = true;
            var Id = Guid.Empty;
            foreach (var test in wrappers)
            {
                if (isFirst)
                {
                    Id = DataStream.StartStream(test);
                    isFirst = false;
                }
                else
                {
                    DataStream.Append(Id, test);
                }
            }

            DataStream.Persist(Id);

            Console.Read();
        }
    }
}