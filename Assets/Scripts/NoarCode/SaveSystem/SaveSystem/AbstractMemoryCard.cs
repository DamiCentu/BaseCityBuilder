
using System.IO;
using UnityEngine;

public abstract class AbstractMemoryCard
{
    public string FileDirectory { get; private set; }
    public string FileName { get; private set; }

    public string FilePath => Path.Combine(FileDirectory, FileName);

    public bool IsSaving { get; private set; }

    public AbstractMemoryCard() {}
    public AbstractMemoryCard(string directory, string filename)
    {
        FileDirectory = directory;
        FileName = "json_"+filename;
    }

    protected void CheckOrCreateDirectory()
    {
        if (!Directory.Exists(FileDirectory))
        {
            Directory.CreateDirectory(FileDirectory);
        }
    }

    public bool Save<T>(T state)
    {
        CheckOrCreateDirectory();

        IsSaving = true;
        bool result =  OnSave(state);
        IsSaving = false;
        return result;
    }


    public T Load<T>()
    {
        if (!Directory.Exists(FileDirectory) || !HasSavedData())
        {
            Debug.LogError("Attempting to Load but there is no save file.");
            return default(T);
        }

        return OnLoad<T>();
    }

    protected abstract bool OnSave<T>(T state);
    protected abstract T OnLoad<T>();

    public bool HasSavedData() => File.Exists(FilePath);

}