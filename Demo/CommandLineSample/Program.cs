using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CommandLineSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings=new Dictionary<string, string> {
                {"name","peter"},
                { "age","28"}
            };
            
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .AddCommandLine(args);

            var configuration = builder.Build();
            Console.WriteLine($"name:{configuration["name"]}");
            Console.WriteLine($"age:{configuration["age"]}");
            Console.ReadLine();
        }
    }
}
