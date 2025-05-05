using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
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