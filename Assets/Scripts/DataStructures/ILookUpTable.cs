namespace DataStructures
{
    public interface ILookUpTable <Tkey,TValue>
    {
        TValue GetValue(Tkey key);
    }
}