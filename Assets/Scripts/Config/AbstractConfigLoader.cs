using AssetLoader;
using DataStructures;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AbstractConfigLoader
    {
        protected AbstractConfigLoader(IAssetLoader loader)
        {
            AssetLoader = loader;
            ConfigLookUpTable = new LookUpTable<string, ScriptableObject>(LoadObject);
        }
        
        public TScriptableConfig GetConfig<TScriptableConfig>(string configName) where TScriptableConfig : ScriptableObject
        {
            return ConfigLookUpTable.GetValue(configName) as TScriptableConfig;
        }
        protected abstract string ConfigPath { get; }

        private ScriptableObject LoadObject(string configName) => AssetLoader.LoadObject(ConfigPath + configName) as ScriptableObject;
        private IAssetLoader AssetLoader { get; }
        private ILookUpTable<string, ScriptableObject> ConfigLookUpTable { get; }
    }
}