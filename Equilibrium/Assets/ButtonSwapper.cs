using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class ButtonSwapper : MonoBehaviour
    {
        public BetterButton BetterButton => betterButton;
        
        [SerializeField] private BetterButton betterButton = default;
        
        [SerializeField] private SwapButton a = default;
        [SerializeField] private SwapButton b = default;

        private bool isPrimary;

        private void Start()
        {
            a.Go(betterButton, false);
            isPrimary = true;
        }

        public void Swap() 
        {
            isPrimary = !isPrimary;

            if (isPrimary)
            {
                a.Go(betterButton);
            }
            else 
            {
                b.Go(betterButton);
            }
        }

        public void SetAs(bool value) 
        {
            isPrimary = value;

            if (isPrimary)
            {
                a.Go(betterButton, false);
            }
            else
            {
                b.Go(betterButton, false);
            }
        }

        [System.Serializable]
        public class SwapButton 
        {
            public string message;
            public Sprite sprite;
            public UnityEvent onEnter;

            public void Go(BetterButton betterButton, bool invoke = true) 
            {
                betterButton.SetText(message);
                if (sprite != null)
                    betterButton.SetSprite(sprite);

                if (invoke)
                    onEnter?.Invoke();
            }
        }
    }
}