using System;
using System.ComponentModel.Composition;

namespace mef.infrastructure.mef
{
    public class PluginFactory
    {
        public void Fabricate(string path)
        {
            try
            {
                var pluginInvoke = new PluginInvoke(path);

                var plugins = pluginInvoke.Plugins;

                foreach (var plugin in plugins)
                    Console.WriteLine(plugin.Execute(10, 5));
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
    }
}