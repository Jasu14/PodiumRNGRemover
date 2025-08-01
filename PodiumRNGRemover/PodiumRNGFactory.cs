using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using PodiumRNGRemover.Utils;

[assembly: ComponentFactory(typeof(PodiumRNGRemover.PodiumRNGFactory))]

namespace PodiumRNGRemover
{
    public class PodiumRNGFactory : IComponentFactory
    {
        public string ComponentName => Constants.UI.COMPONENT_NAME;

        public string Description => Constants.UI.COMPONENT_DESCRIPTION;

        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state)
        {
            return new PodiumRNGComponent(state);
        }

        public string UpdateName => ComponentName;

        public string UpdateURL => "";

        public string XMLURL => "";

        public Version Version => Version.Parse(Constants.UI.COMPONENT_VERSION);
    }
}