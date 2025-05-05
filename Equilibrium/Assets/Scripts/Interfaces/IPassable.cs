namespace OuterWilds
{
    /// <summary>
    /// Or call it IWritable or something,
    /// i could also make one without reference
    /// and then this would be IReferencable
    /// </summary>
    public interface IPassable<T> 
    {
        void Pass(ref T t);
    }
}