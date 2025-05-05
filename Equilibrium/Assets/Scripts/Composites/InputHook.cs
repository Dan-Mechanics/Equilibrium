using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class InputHook : MonoBehaviour
    {
        [SerializeField] private KeyCode key = default;
        [SerializeField] private UnityEvent onHook = default;

        private void Update()
        {
            if (Input.GetKeyDown(key))
                onHook?.Invoke();
        }
    }
}