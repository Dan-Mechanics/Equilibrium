using UnityEngine;

namespace Equilibrium
{
    public class DestroyableGameObject : MonoBehaviour, IDestroyable
    {
        public void Destroy() => Destroy(gameObject);
    }
}