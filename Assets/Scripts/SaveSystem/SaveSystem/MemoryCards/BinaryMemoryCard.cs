using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryMemoryCard : AbstractMemoryCard
{
    private BinaryFormatter formatter = new BinaryFormatter();
    private FileStream currentStream;

    public BinaryMemoryCard() {}
    
    public BinaryMemoryCard(string directory, string filename) : base(directory, filename) { }
    
    protected override bool OnSave<T>(T state)
    {
        try
        {
            currentStream = new FileStream(FilePath, FileMode.OpenOrCreate);
            currentStream.Position = 0;

            formatter.Serialize(currentStream, state);
            currentStream.Close();
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }

    protected override T OnLoad<T>()
    {
        try
        {
            currentStream = new FileStream(FilePath, FileMode.Open);
            currentStream.Position = 0;

            T loadedState = (T)formatter.Deserialize(currentStream);

            currentStream.Close();
            return loadedState;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return default(T);
        }
    }
}