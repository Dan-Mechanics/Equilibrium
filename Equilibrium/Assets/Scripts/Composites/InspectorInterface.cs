using System;
using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// I call this the "bullshit fuck your mother pattern".
    /// 
    /// It would be better if we just had a list of Objects
    /// and then we make our own thing out of it
    /// so like pip the objects into this so it works better.
    /// </summary>
    [Serializable]
    public class InspectorInterface<T>
    {
        /// <summary>
        /// Could i add other things like ScriptableOjbect i nthe futuer?
        /// </summary>
        public MonoBehaviour monoBehaviour;
        public T attached;

        public void Setup()
        {
            if (monoBehaviour == null)
            {
                Debug.LogWarning($"please assign monoBehaviour");
                return;
            }

            attached = monoBehaviour.GetComponent<T>();

            if (attached == null)
                Debug.LogWarning($"if (attached == null), on {GetType()}");
        }
    }
}