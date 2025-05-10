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
    /// 
    /// I could make an event bus for this? or like a queeu type beat.
    /// </summary>
    public class TitleHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text title = default;
        [SerializeField] private float introductionTime = default;
        [SerializeField] private TitleMessage standard = default;
        [SerializeField] private TitleMessage[] titleMessages = default;

        private TitleMessage current;
        private Color fadingColor;
        private readonly Dictionary<EventManager.EventType, TitleMessage> conversions = new();
        private readonly Queue<TitleMessage> pendingTitleMessages = new();

        private void FixedUpdate()
        {
            if (current == null)
                Pop();

            if (current == null)
                return;
            
            fadingColor.a -= Time.fixedDeltaTime / current.duration / Time.timeScale;
            title.color = fadingColor;

            // Or just call pop.
            if (fadingColor.a <= 0f)
                current = null;
        }

        private void ReceiveEventType(EventManager.EventType eventType) 
        {
            if (!conversions.ContainsKey(eventType))
                return;

            EnqueueTitle(conversions[eventType]);
        }

        private void EnqueueTitle(TitleMessage message) 
        {
            bool ble = current != null && !current.priority && !message.priority;
            if (ble)
                message.priority = true;
                //return;
            // or make current massage have prioity.

            if (message.duration <= 0f)
                message.duration = introductionTime;

            message.color.a = 1f;
            message.message = message.message.ToUpper();

            if (message.priority)
            {
                if (current != null && !current.priority && !ble)
                    pendingTitleMessages.Enqueue(current);

                SetCurrentMessage(message);
            }
            else 
            {
                pendingTitleMessages.Enqueue(message);
            }
        }

        private void Pop()
        {
            if (pendingTitleMessages.Count <= 0)
                return;

            SetCurrentMessage(pendingTitleMessages.Dequeue());
        }

        private void SetCurrentMessage(TitleMessage message)
        {
            current = message;

            title.text = current.message;
            fadingColor = current.color;
            title.color = fadingColor;

            print(current.message);
        }

        public void SetTitleWithMessage(string message) 
        {
            print(message);
            standard.message = message;
            EnqueueTitle(standard);
        }

        private void OnEnable()
        {
            for (int i = 0; i < titleMessages.Length; i++)
            {
                conversions.Add(titleMessages[i].eventType, titleMessages[i]);
                EventManager.AddListener(titleMessages[i].eventType, ReceiveEventType);
            }

            EventManager<TitleMessage>.AddListener(EventManager.EventType.TITLE, EnqueueTitle);
        }

        private void OnDisable()
        {
            for (int i = 0; i < titleMessages.Length; i++)
            {
                EventManager.RemoveListener(titleMessages[i].eventType, ReceiveEventType);
            }

            EventManager<TitleMessage>.RemoveListener(EventManager.EventType.TITLE, EnqueueTitle);
        }

        [System.Serializable]
        public class TitleMessage
        {
            public EventManager.EventType eventType;
            public string message;
            public Color color;
            public bool priority;
            [Min(0f)] public float duration;

            public TitleMessage(string message, Color color, bool priority, float duration = 0f)
            {
                this.message = message;
                this.color = color;
                this.priority = priority;
                this.duration = duration;
            }
        }
    }
}