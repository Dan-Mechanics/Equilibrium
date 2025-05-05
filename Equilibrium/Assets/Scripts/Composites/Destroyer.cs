using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    public class Destroyer : MonoBehaviour, IDestroyable
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyOther(GameObject other) 
        {
            Destroy(other);
        }
    }
}