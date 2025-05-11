using UnityEngine;

namespace Equilibrium
{
    public interface ISpawnable
    {
         Transform[] Spawn(SpawnData spawnData);
         Transform SpawnSingle(SpawnData spawnData);
    }
}