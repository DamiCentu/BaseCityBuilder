using UnityEngine;

namespace AssetLoader
{
    public interface IAssetLoader
    {
        Object LoadObject(string path);
        Object[] LoadObjects(string path);
    }
}