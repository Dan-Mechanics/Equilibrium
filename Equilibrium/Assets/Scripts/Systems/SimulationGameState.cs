using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public class SimulationGameState : GameState
    {
        [SerializeField] private TimeHandler timeHandler = default;
        [SerializeField] private float hyperSpeedScale = default;
        [SerializeField] private TimerText timerText = default;
        [SerializeField] private CreatureHandler creatureHandler = default;
        [SerializeField] private GameObject playButton = default;
        [SerializeField] private GameObject stopButton = default;

        public override void EnterState()
        {
            base.EnterState();

            playButton.SetActive(false);
            stopButton.SetActive(!playButton.activeSelf);

            timerText.ResetTimer();
            creatureHandler.Respawn();
            timeHandler.SetTimeScale(hyperSpeedScale);
        }

        public override void ExitState()
        {
            base.ExitState();

            playButton.SetActive(true);
            stopButton.SetActive(!playButton.activeSelf);

            timeHandler.BackToNormal();
            creatureHandler.Stop();
            timerText.gameObject.SetActive(false);
        }
    }
}