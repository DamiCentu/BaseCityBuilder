
namespace Factory
{
    public class ObjectFactory : IFactory
    {
        public TObject CreateInstance<TObject>() where TObject : new() => new TObject();
    }
}