using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public abstract class GameState : MonoBehaviour, IUpdatable, IFixedUpdatable
    {
        [SerializeField] private List<InspectorInterface<IUpdatable>> updatables = default;
        [SerializeField] private List<InspectorInterface<IFixedUpdatable>> fixedUpdatables = default;
        [SerializeField] private GameObject statePanel = default;

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
        }

        public virtual void ExitState() 
        {
            statePanel.SetActive(false);
        }
    }
}