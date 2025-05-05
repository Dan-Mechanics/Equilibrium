using System;
using UnityEngine;

namespace OuterWilds
{
    /// <summary>
    /// I call this the "bullshit fuck your mother pattern".
    /// </summary>
    [Serializable]
    public class InspectorInterface<T>
    {
        public MonoBehaviour monoBehaviour;
        public T attached;

        public void Setup()
        {
            if (monoBehaviour == null)
            {
                Debug.LogError($"please assign monoBehaviour");
                return;
            }

            attached = monoBehaviour.GetComponent<T>();

            if (attached == null)
                Debug.LogError($"if (attached == null), on {GetType()}");
        }
    }
}