using System.ComponentModel.Composition;

using mef.infrastructure.mef.interfaces;

namespace mef.domain.plugin
{
    [Export(typeof(IPlugin))]
    public class Plugin2 : IPlugin
    {
        public int Execute(int left, int right)
        {
            return left - right;
        }
    }
}
