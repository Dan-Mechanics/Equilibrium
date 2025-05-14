using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class WaitingGameState : GameState
    {
        [SerializeField] private float waitTime = default;
        [SerializeField] private GameStateMachine machine = default;
        [SerializeField] private bool wdwd = false;
        [SerializeField] private TerrainBuilder builder = default;
        private readonly Timer timer = new Timer();

        public override void DoFixedUpdate()
        {
            base.DoFixedUpdate();

            if (timer.Tick(Time.fixedDeltaTime)) 
            {
                if (!wdwd)
                    machine.TransitionTo(typeof(TerraformingGameState));
                else { machine.TransitionTo(typeof(SimulationGameState)); }
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