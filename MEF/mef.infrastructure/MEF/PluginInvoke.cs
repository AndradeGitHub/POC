using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using mef.infrastructure.mef.interfaces;

namespace mef.infrastructure.mef.invoke
{
    public class PluginInvoke
    {
        private CompositionContainer _container;

        [ImportMany]
        public List<IPlugin> Plugins;

        public PluginInvoke(string path)
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(PluginInvoke).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(path));

            _container = new CompositionContainer(catalog);

            this._container.ComposeParts(this);
        }
    }
}