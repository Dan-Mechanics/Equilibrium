using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class WaitingGameState : GameState
    {
        [SerializeField] private float waitTime = default;
        [SerializeField] private GameStateMachine machine = default;
        [SerializeField] private TerrainBuilder builder = default;
        private readonly Timer timer = new Timer();

        public override void DoFixedUpdate()
        {
            base.DoFixedUpdate();

            if (timer.Tick(Time.fixedDeltaTime)) 
            {
                machine.TransitionTo(typeof(TerraformingGameState));
            }
        }

        public override void EnterState()
        {
            base.EnterState();

            timer.SetValue(waitTime);
        }

        public override void ExitState()
        {
            base.ExitState();

            builder.GoNextMap();
        }
    }
}