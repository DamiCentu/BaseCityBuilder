namespace Factory
{
    public interface IFactory
    {
        TObject CreateInstance<TObject>() where TObject : new();
    }
}