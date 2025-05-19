using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Equilibrium
{
    public enum State { Mountain = 0, Water = 1, Dragging = 2, Simulating = 3}

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
        [SerializeField] private bool isSimulating = default;

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

        public void SetSimulating(bool value)
        {
            isSimulating = value;
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

            if (isSimulating)
                return State.Simulating;

            return isMountain ? State.Mountain : State.Water;
        }

    }
}