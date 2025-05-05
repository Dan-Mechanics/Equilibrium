using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    /// <summary>
    /// User timer composite !!
    /// </summary>
    public class TimerText : MonoBehaviour
    {
        private TMP_Text text;
        private float startTime;
        [SerializeField] private float timerDoneValue;
        [SerializeField] private UnityEvent onDone = default;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            ResetTimer();
        }

        public void ResetTimer()
        {
            startTime = Time.time;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            float f = Mathf.Round(Time.time - startTime);
            text.text = f.ToString();
            if (f >= timerDoneValue)
            {
                print("W !!");
                onDone?.Invoke();
            }

        }
    }
}