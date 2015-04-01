using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXCommons;

namespace UpdateHouseData
{
    public  class Program
    {
        static void Main(string[] args)
        {
            int min = PubConstant.ToInt32(PubConstant.GetConfigString("min"));

            int max = PubConstant.ToInt32(PubConstant.GetConfigString("max"));
            for (int i=min; i <= max; i++)
            {
                TXCommons.MsgQueue.SendMessage.SendLongHouseIndexMessage("update", i, 253);
                Console.WriteLine(i);
            }
            Console.WriteLine("ok");
            Console.ReadLine();
            
        }
    }
}
