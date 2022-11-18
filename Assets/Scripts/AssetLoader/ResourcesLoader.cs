using UnityEngine;

namespace AssetLoader
{
    public class ResourcesLoader : IAssetLoader
    {
        public Object LoadObject(string path) => Resources.Load(path);
        public Object[] LoadObjects(string path) => Resources.LoadAll(path);
    }
}