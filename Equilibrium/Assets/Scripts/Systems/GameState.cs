using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public abstract class GameState : MonoBehaviour, IUpdatable, IFixedUpdatable
    {
        [SerializeField] private List<InspectorInterface<IUpdatable>> updatables = default;
        [SerializeField] private List<InspectorInterface<IFixedUpdatable>> fixedUpdatables = default;
        [SerializeField] private GameObject statePanel = default;
        [SerializeField] private UnityEvent onEnter = default;
        [SerializeField] private UnityEvent onExit = default;

        public virtual void Awake() 
        {
            updatables.ForEach(x => x.Setup());
            fixedUpdatables.ForEach(x => x.Setup());

            statePanel.SetActive(false);
        }

        public virtual void DoUpdate() 
        {
            updatables.ForEach(x => x.attached.DoUpdate());
        }

        public virtual void DoFixedUpdate()
        {
            fixedUpdatables.ForEach(x => x.attached.DoFixedUpdate());
        }

        public virtual void EnterState() 
        {
            statePanel.SetActive(true);
            onEnter?.Invoke();
        }

        public virtual void ExitState() 
        {
            statePanel.SetActive(false);
            onExit?.Invoke();
        }
    }
}