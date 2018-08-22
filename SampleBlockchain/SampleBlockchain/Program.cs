using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Hosting.Self;

namespace SampleBlockchain
{
    public class Program
    {
        /// <summary>
        /// The instance of the blockchain
        /// </summary>
        public static Blockchain blockchain = new Blockchain();

        /// <summary>
        /// The URI
        /// </summary>
        public static String Uri;

        static void Main(string[] args)
        {
            if (args.Length > 0)
                Uri = String.Format("http://localhost:{0}", args[0]);
            else
                Uri = "http://localhost:8877";

            // initialize an instance of NancyHost (found in the Nancy.Hosting.Self package)
            var host = new NancyHost(new Uri(Uri));
            host.Start(); 

            Console.WriteLine("Server started, now listening on " + Uri);
            Console.WriteLine("Press any key to stop the server...");

            Console.ReadKey();
            host.Stop();
        }
    }
}
