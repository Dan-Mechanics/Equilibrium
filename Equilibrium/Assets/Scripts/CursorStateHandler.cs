using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Equilibrium
{
    public enum State { Mountain = 0, Water = 1, Dragging = 2 }

    /// <summary>
    /// This code does not seem very scalable.
    /// </summary>
    public class CursorStateHandler : MonoBehaviour, IDataGettable<State>
    {
        [SerializeField] private InspectorInterface<IWritable<int>> spriteSetter = default;

        public State Data => state;
        private State state;

        [SerializeField] private bool isMountain = default;
        [SerializeField] private bool isDragging = default;

        private void Awake()
        {
            spriteSetter.Setup();
        }

        private void Start() => RefreshState();

        public void SetMountain(bool value)
        {
            isMountain = value;
            RefreshState();
        }

        public void SetDragging(bool value)
        {
            isDragging = value;
            RefreshState();
        }

        private void RefreshState()
        {
            state = CalculateState();
            spriteSetter.attached.Write((int)state);
        }

        private State CalculateState() 
        {
            if (isDragging)
                return State.Dragging;

            return isMountain ? State.Mountain : State.Water;
        }

    }
}