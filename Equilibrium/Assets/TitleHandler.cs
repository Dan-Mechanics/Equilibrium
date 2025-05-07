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

        private TitleMessage current;
        private readonly Dictionary<EventManager.EventType, TitleMessage> conversions = new();

        private void OnEnable()
        {
            for (int i = 0; i < titleMessages.Length; i++)
            {
                conversions.Add(titleMessages[i].eventType, titleMessages[i]);
                EventManager.AddListener(titleMessages[i].eventType, ReceiveEventType);
            }

            EventManager<TitleMessage>.AddListener(EventManager.EventType.TITLE, SetTitle);
        }

        private void OnDisable()
        {
            for (int i = 0; i < titleMessages.Length; i++)
            {
                EventManager.RemoveListener(titleMessages[i].eventType, ReceiveEventType);
            }

            EventManager<TitleMessage>.RemoveListener(EventManager.EventType.TITLE, SetTitle);
        }

        private void FixedUpdate()
        {
            if (current == null)
                return;
            
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
            //clearTime = Time.time + message.duration;
            title.gameObject.SetActive(true);
            title.text = message.message.ToUpper();
            title.color = message.color;

            current = message;
        }

        [System.Serializable]
        public class TitleMessage
        {
            public EventManager.EventType eventType;
            public string message;
            public Color color;
            public float duration;

            public TitleMessage(string message, Color color, float duration)
            {
                this.message = message;
                this.color = color;
                this.duration = duration;
            }
        }
    }
}