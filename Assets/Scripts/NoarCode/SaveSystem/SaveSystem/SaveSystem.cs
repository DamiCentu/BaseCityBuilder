using System.IO;
using System.Threading.Tasks;
using System;
using System.Threading;

public class SaveSystem<T> where T : class
{
    private AbstractMemoryCard memoryCard;
    
    public string FileDirectory { get; private set; }
    public string FileName { get; private set; }

    private Task<bool> saveStateTask;
    private CancellationTokenSource saveStateCancellationToken;

    private string FilePath => Path.Combine(FileDirectory, FileName);

    public bool IsSaving => memoryCard?.IsSaving ?? false;

    public event Action OnSaveRequested;
    public event Action<bool> OnSaveDone;
    public event Action OnLoadRequested;
    public  event Action<bool> OnLoadDone;

    public SaveSystem(string directory, string filename)
    {
        FileDirectory = directory;
        FileName = filename;
    }

    public void CreateMemoryCard<TCard>() where TCard : AbstractMemoryCard, new()
    {
        memoryCard = (TCard)Activator.CreateInstance(typeof(TCard), FileDirectory, FileName);
    }

    public bool HasSavedData() => memoryCard.HasSavedData();

    public async void SaveStateAsync(T stateToSave)
    {
        CheckIfCardExists();
        
        if (IsSaving)
        {
            saveStateCancellationToken.Cancel();
        }
        
        saveStateCancellationToken = new CancellationTokenSource();
        saveStateTask = Task.Run(() => SaveStateSync(stateToSave), saveStateCancellationToken.Token);
        
        OnSaveRequested?.Invoke();
    }

    private void CheckIfCardExists()
    {
        if (memoryCard == null)
        {
            throw new Exception($"{GetType()} :: Memory Card wasn't settled. Call CreateMemoryCard().");
        }
    }

    public bool SaveStateSync(T stateToSave)
    {
        CheckIfCardExists();

        try
        {
            memoryCard.Save(stateToSave);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<T> LoadStateAsync()
    {
        var task = Task.Run(LoadStateSync);
        OnLoadRequested?.Invoke();
        T state = await task;
        return state;
    }

    public T LoadStateSync()
    {
        CheckIfCardExists();

        try
        {
            return memoryCard.Load<T>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default(T);
        }
    }
}
