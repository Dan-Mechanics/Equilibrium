using System;
using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private GameState startingState = default;
        private GameState current;
        [SerializeField] private List<GameState> states = default;

        private void Start()
        {
           //states.ForEach(x => x.ExitState());

            TransitionTo(startingState);
        }

        private void Update()
        {
            current?.DoUpdate();
        }

        private void FixedUpdate()
        {
            current?.DoFixedUpdate();
        }

        public void TransitionTo(GameState newState) 
        {
            if (newState == null)
                return;
            
            if (current == newState)
                return;

            current?.ExitState();
            current = newState;
            current.EnterState();
        }

        public void TransitionTo(Type type) 
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].GetType() != type)
                    continue;

                TransitionTo(states[i]);
                return;
            }
        }
    }
}