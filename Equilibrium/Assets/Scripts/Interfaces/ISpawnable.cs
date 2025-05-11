using UnityEngine;

namespace Equilibrium
{
    public interface ISpawnable
    {
         Transform[] Spawn(SpawnData spawnData);
    }
}