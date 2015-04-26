using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorRemote;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace CalculatorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpChannel channel = new HttpChannel(8080);
            ChannelServices.RegisterChannel(channel, false);
            Calculator calc = new Calculator();
            ObjRef ref1 = RemotingServices.Marshal(calc, "calcURI");
            Console.WriteLine("CalcRef.URI: " + ref1.URI);
            Console.WriteLine("Server is running, press enter to stop server");
            Console.ReadLine();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
