using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonMemoryCard : AbstractMemoryCard
{
    private StreamWriter streamWriter;
    private StreamReader streamReader;

    public JsonMemoryCard() {}
    
    public JsonMemoryCard(string directory, string filename) : base(directory, filename) { }

    public JsonSerializerSettings SerializerSettings { get; set; } = new JsonMemoryCardSerializerSettings();
    
    protected override bool OnSave<T>(T state)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(state, SerializerSettings);
            
            streamWriter = new StreamWriter(FilePath);
            streamWriter.Write(jsonData);
            streamWriter.Close();
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            streamWriter.Close();
            return false;
        }
    }

    protected override T OnLoad<T>()
    {
        try
        {
            streamReader = new StreamReader(FilePath);
            string jsonData = streamReader.ReadToEnd();

            T loadedState = JsonConvert.DeserializeObject<T>(jsonData, SerializerSettings);
            streamReader.Close();
            return loadedState;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            streamReader.Close();
            return default(T);
        }
    }
}