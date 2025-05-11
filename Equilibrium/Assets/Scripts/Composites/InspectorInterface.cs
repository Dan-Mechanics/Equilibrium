using System;
using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// I call this the "bullshit fuck your mother pattern".
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