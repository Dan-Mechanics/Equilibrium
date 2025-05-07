using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Equilibrium
{
    /// <summary>
    /// Main question: do we want the title handler to respond to 
    /// "END GAME" "START GAME" etc and then we define here what that gets
    /// or do we want there to be a "SHOW TITEL" event whereby we give it the color 
    /// and such/
    /// </summary>
    public class TitleHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text title = default;
        [SerializeField] private TitleMessage[] titleMessages = default;

        private float clearTime;
        private TitleMessage current;
        private readonly Dictionary<EventManager.EventType, TitleMessage> conversions = new();

        private void OnEnable()
        {
            for (int i = 0; i < titleMessages.Length; i++)
            {
                conversions.Add(titleMessages[i].eventType, titleMessages[i]);
                EventManager.AddListener(titleMessages[i].eventType, ReceiveEventType);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < titleMessages.Length; i++)
            {
                EventManager.RemoveListener(titleMessages[i].eventType, ReceiveEventType);
            }
        }

        /// <summary>
        /// Fade effect would be nice.
        /// </summary>
        private void FixedUpdate()
        {
            //title.gameObject.SetActive(!Utils.IsTime(clearTime));

            // Not the best code this but ok.
            Color temp = title.color;
            temp.a -= Time.fixedDeltaTime / current.duration;
            title.color = temp;
        }

        private void ReceiveEventType(EventManager.EventType eventType) 
        {
            if (!conversions.ContainsKey(eventType))
                return;

            SetTitle(conversions[eventType]);
        }

        private void SetTitle(TitleMessage message) 
        {
            clearTime = Time.time + message.duration;
            title.gameObject.SetActive(true);
            title.text = message.message.ToUpper();
            title.color = message.color;

            current = message;
        }

        [System.Serializable]
        public struct TitleMessage 
        {
            public EventManager.EventType eventType;
            public string message;
            public Color color;
            public float duration;
        }
    }
}