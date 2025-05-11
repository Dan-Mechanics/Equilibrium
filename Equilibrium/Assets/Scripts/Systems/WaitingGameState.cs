using UnityEngine;

namespace Equilibrium
{
    public class WaitingGameState : GameState
    {
        [SerializeField] private GameObject playButton = default;

        public override void EnterState()
        {
            base.EnterState();

            playButton.SetActive(false);
        }

        public override void ExitState()
        {
            base.ExitState();

            playButton.SetActive(true);
        }
    }
}