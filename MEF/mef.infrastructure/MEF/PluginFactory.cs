using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using mef.infrastructure.mef.interfaces;
using mef.infrastructure.mef.invoke;
using mef.infrastructure.cache;

namespace mef.infrastructure.mef
{
    public class PluginFactory
    {
        const string cacheKey = "Plugins";

        public void CreatePlugin(string path)
        {
            try
            {                      
                var plugins = RecuperaPlugins(path);
                foreach (var plugin in plugins)
                {
                    Console.WriteLine(plugin.Execute(10, 5));

                    if (plugin.ToString().ToLower().Contains("plugin2"))
                        Console.WriteLine("plugin2");
                }

                var plugins1 = RecuperaPlugins(path);
                foreach (var plugin in plugins1)
                {
                    Console.WriteLine(plugin.Execute(10, 5));

                    if (plugin.ToString().ToLower().Contains("plugin2"))
                        Console.WriteLine("plugin2");
                }

            }
            catch (CompositionException compositionException)
            {
                throw compositionException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IList<IPlugin> RecuperaPlugins(string path)
        {            
            var plugins = Cache.Get<List<IPlugin>>(cacheKey);
            if (plugins == null)
            {
                var pluginInvoke = new PluginInvoke(path);

                plugins = pluginInvoke.Plugins;

                Cache.AddItem(cacheKey, plugins, 1);
            }

            return plugins;
        }
    }
}