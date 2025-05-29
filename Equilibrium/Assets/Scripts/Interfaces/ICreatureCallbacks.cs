namespace Equilibrium
{
    public interface ICreatureCallbacks
    {
        void ClaimCallback(int faction);
        void DieCallback(int faction);
    }
}