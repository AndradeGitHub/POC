using System;
using System.ComponentModel.Composition;

using mef.infrastructure.mef;

namespace mef.console
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Sistemas\\POC\\MEF\\Pluginstt";

            try
            {
                var pluginFactory = new PluginFactory();
                pluginFactory.Fabricate(path);                         
            }
            catch(Exception ex)
            {
                Console.Write(ex);                
            }

            Console.ReadLine();
        }
    }
}
