using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Equilibrium
{
    public class TerraformingGameState : GameState
    {
        [SerializeField] private UnityEvent onEnterState = default;
        
        
        public override void EnterState()
        {
            base.EnterState();

            onEnterState?.Invoke();
            /*EventManager<TitleHandler.TitleMessage>.RaiseEvent(EventManager.EventType.TITLE,
                new TitleHandler.TitleMessage("...", Color.white, 0.5f));*/
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}