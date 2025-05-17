using UnityEngine;
using UnityEngine.UI;

namespace Equilibrium
{
    [RequireComponent(typeof(Image))]
    public class BarHandler : MonoBehaviour
    {
        private Image bar;

        private void Awake()
        {
            bar = GetComponent<Image>();
           // SetBar(0f);
        }

        public void SetFill(float fill) 
        {
            bar.fillAmount = fill;
        }
    }
}