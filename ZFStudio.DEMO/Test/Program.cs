using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static List<Func<RequestDelegate, RequestDelegate>> funcs = new List<Func<RequestDelegate, RequestDelegate>>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            
            Func<RequestDelegate, RequestDelegate> middleware1 = next =>
            {
                return async context =>
                {
                    Console.WriteLine("middleware 1 start");
                    await next.Invoke(context);
                    Console.WriteLine("middleware 1 end");
                };
            };

            Func<RequestDelegate, RequestDelegate> middleware2 = next =>
            {
                return async context =>
                {
                    Console.WriteLine("middleware 2 start");
                    await next.Invoke(context);
                    Console.WriteLine("middleware 2 end");
                };
            };

            Func<RequestDelegate, RequestDelegate> middleware3= next =>
            {
                return async context =>
                {
                    Console.WriteLine("middleware 3 start");
                    Console.WriteLine("middleware 3 end");
                };
            };

            funcs.Add(middleware1);
            funcs.Add(middleware2);
            funcs.Add(middleware3);

            RequestDelegate app = async contxt =>
            {
                Console.WriteLine("error 404");
            };

            for (int i = funcs.Count - 1; i >= 0; i--)
            {
                app = funcs[i].Invoke(app);
            }


            app.Invoke(null);

            Console.ReadLine();
        }

        
    }
}
