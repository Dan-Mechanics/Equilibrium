using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    public class TerraformingGameState : GameState
    {
        public override void EnterState()
        {
            base.EnterState();

            /*EventManager<TitleHandler.TitleMessage>.RaiseEvent(EventManager.EventType.TITLE,
                new TitleHandler.TitleMessage("...", Color.white, 0.5f));*/
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}