using System.Collections.Generic;
using UnityEngine;

namespace OuterWilds
{
    public abstract class GameState : MonoBehaviour, IUpdatable, IFixedUpdatable
    {
        //public string Nickname => nickname;

        //[SerializeField] private string nickname = default;
        [SerializeField] private List<InspectorInterface<IUpdatable>> updatables = default;
        [SerializeField] private List<InspectorInterface<IFixedUpdatable>> fixedUpdatables = default;

        public virtual void Awake() 
        {
            updatables.ForEach(x => x.Setup());
            fixedUpdatables.ForEach(x => x.Setup());
        }

        public virtual void DoUpdate() 
        {
            updatables.ForEach(x => x.attached.DoUpdate());
        }

        public virtual void DoFixedUpdate()
        {
            fixedUpdatables.ForEach(x => x.attached.DoFixedUpdate());
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }
    }
}