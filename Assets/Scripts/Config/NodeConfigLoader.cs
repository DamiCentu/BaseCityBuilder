using AssetLoader;

namespace DefaultNamespace
{
    public class NodeConfigLoader : AbstractConfigLoader
    {
        public NodeConfigLoader(IAssetLoader loader) : base(loader) { }

        protected override string ConfigPath => AssetLoaderConstants.SCRIPTABLE_OBJECT_PATH +
                                                AssetLoaderConstants.NODES_PATH +
                                                AssetLoaderConstants.NODES_RESIDENTIAL_PATH +
                                                AssetLoaderConstants.NODES_RESIDENTIAL_LOW_PATH;
    }
}