using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Equilibrium
{
    public class TimerEvent : MonoBehaviour
    {
        private readonly Timer timer = new Timer();
        [SerializeField] private UnityEvent onDone = default;

        private void FixedUpdate()
        {
            if (timer.Tick(Time.fixedDeltaTime))
            {
                Hook();
                timer.DisableUntilSet();
            }
        }

        public void SetTimer(float value) 
        {
            if (value <= 0f)
            {
                Hook();
                return;
            }

            timer.SetValue(value);
        }

        private void Hook() => onDone?.Invoke();
    }
}