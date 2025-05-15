using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class SimulationGameState : GameState
    {
        [SerializeField] private TimeHandler timeHandler = default;
        [SerializeField] private CreatureHandler creatureHandler = default;
        [SerializeField] private float hyperSpeedScale = default;
        [SerializeField] private float simulationTime = default;

        [SerializeField] private UnityEvent<float> onNewTimerText = default;

        private readonly Timer timer = new();

        public override void EnterState()
        {
            base.EnterState();

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

            onNewTimerText?.Invoke(Mathf.Round(timer.Value));
        }

        public override void ExitState()
        {
            base.ExitState();

            print(timer.Value);

            // ??
            onNewTimerText?.Invoke(0f);

            timeHandler.BackToNormal();
            creatureHandler.Stop();
        }
    }
}