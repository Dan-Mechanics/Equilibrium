using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Equilibrium
{
    public class SimulationGameState : GameState
    {
        [SerializeField] private TimeHandler timeHandler = default;
        [SerializeField] private CreatureHandler creatureHandler = default;
        [SerializeField] private float hyperSpeedScale = default;
        [SerializeField] private float simulationTime = default;

      //  [SerializeField] private ButtonSwapper playSwapper = default;
        /*[SerializeField] private BetterButton waterMountainButton = default; 
        [SerializeField] private BetterButton wipeCleanButton = default;*/

        [SerializeField] private UnityEvent<string> onNewTimerText = default;

        private readonly Timer timer = new();

        public override void EnterState()
        {
            base.EnterState();

            /*playButton.SetActive(false);
            stopButton.SetActive(!playButton.activeSelf);*/
           // playSwapper.SetAs(false);
            //playSwapper.BetterButton.GiveCooldown(0.5f);
            creatureHandler.Respawn();
            timeHandler.SetTimeScale(hyperSpeedScale);
            timer.SetValue(simulationTime);

          //  waterMountainButton.SetInteractable(false);
          //  wipeCleanButton.SetInteractable(false);

            EventManager.RaiseEvent(EventManager.EventType.ROUND_START);
        }
        
        public override void DoFixedUpdate()
        {
            base.DoFixedUpdate();

            if (timer.Tick(Time.fixedDeltaTime))
            {
                EventManager.RaiseEvent(EventManager.EventType.ROUND_WIN);
                return;
            }

            onNewTimerText?.Invoke(Mathf.Round(timer.Value).ToString());
        }

        public override void ExitState()
        {
            base.ExitState();

            print(timer.Value);
            onNewTimerText?.Invoke(string.Empty);

         //   playSwapper.SetAs(true);
          //  playSwapper.BetterButton.GiveCooldown(0.5f);

         //   waterMountainButton.SetInteractable(true);
         //   wipeCleanButton.SetInteractable(true);

            timeHandler.BackToNormal();
            creatureHandler.Stop();
        }
    }
}