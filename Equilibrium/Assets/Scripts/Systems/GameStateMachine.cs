using System;
using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private GameState current = default;

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
            /*Type lookingForType = newState.GetType();
            
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].GetType() != lookingForType)
                    continue;

                Switch(i);
                return;
            }*/

            current?.ExitState();
            current = newState;
            current.EnterState();
        }
    }
}