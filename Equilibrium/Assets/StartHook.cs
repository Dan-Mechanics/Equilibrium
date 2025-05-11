using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class StartHook : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStart = default;

        private void Start()
        {
            onStart?.Invoke();   
        }
    }
}