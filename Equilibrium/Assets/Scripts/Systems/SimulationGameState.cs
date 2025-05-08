using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Equilibrium
{
    public class SimulationGameState : GameState
    {
        //[SerializeField] private GameStateMachine gameStateMachine = default;
        [SerializeField] private TimeHandler timeHandler = default;
        [SerializeField] private float hyperSpeedScale = default;
        //[SerializeField] private TerraformingGameState terraforming = default;
        [SerializeField] private CreatureHandler creatureHandler = default;
        [SerializeField] private GameObject playButton = default;
        [SerializeField] private GameObject stopButton = default;
        [SerializeField] private float simulationTime = default;
        [SerializeField] private UnityEvent<string> onNewTimerText = default;

        private readonly Timer timer = new();

        public override void EnterState()
        {
            base.EnterState();

            playButton.SetActive(false);
            stopButton.SetActive(!playButton.activeSelf);

            creatureHandler.Respawn();
            timeHandler.SetTimeScale(hyperSpeedScale);
            timer.SetValue(simulationTime);

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

            onNewTimerText?.Invoke(string.Empty);
            playButton.SetActive(true);
            stopButton.SetActive(!playButton.activeSelf);

            timeHandler.BackToNormal();
            creatureHandler.Stop();
        }
    }
}