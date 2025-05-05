namespace OuterWilds
{
    /// <summary>
    /// How do you add more params in the T that would be useufl?
    /// could you use param t or some shit like that ??
    /// I think generally this interface is too abstract LOL.
    /// </summary>>
    public interface IWritable<T> 
    {
        void Write(T obj);
    }

    public interface IWritable<T1, T2>
    {
        void Write(T1 args1, T2 args2);
    }
}