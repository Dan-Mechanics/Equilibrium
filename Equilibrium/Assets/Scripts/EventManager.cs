using System;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// https://github.com/vmuijrers/GitGud/blob/main/Assets/Scripts/EventsExample/EventScripts.cs
    /// NOTE: i could use this as decoupler for new terrains and such.
    /// </summary>
    public static class EventManager
    {
        public enum EventType
        {
            OPEN_GAME = 0,
            ROUND_LOSE = 1,
            ROUND_WIN = 2,
            ROUND_START = 3,
            CLOSE_GAME = 4
        }

        private static readonly Dictionary<EventType, Action<EventType>> events = new();

        public static void RaiseEvent(EventType eventType)
        {
            if (!events.ContainsKey(eventType))
                return;

            events[eventType]?.Invoke(eventType);
        }

        public static void AddListener(EventType eventType, Action<EventType> listener)
        {
            if (!events.ContainsKey(eventType))
                events.Add(eventType, null);

            events[eventType] += listener;
        }

        public static void RemoveListener(EventType eventType, Action<EventType> listener)
        {
            if (!events.ContainsKey(eventType))
                return;

            events[eventType] -= listener;
        }
    }
}