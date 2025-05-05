using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class TimeHandler : MonoBehaviour, IWritable<float>
    {
        public const float INTERVAL = 1f / 50f;

        [SerializeField] private List<InspectorInterface<IWritable<float>>> listeners = default;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
        }

        private void Start() => BackToNormal();

        public void SetTimeScale(float speed) 
        {
            Time.timeScale = speed;
            Time.fixedDeltaTime = Time.timeScale * INTERVAL;

            listeners.ForEach(x => x.attached.Write(speed));
        }

        public void BackToNormal() => SetTimeScale(1f);

        public void Write(float t) => SetTimeScale(t);
    }
}