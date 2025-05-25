using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Equilibrium
{
    public class TooltipHandler : MonoBehaviour
    {
        [SerializeField] private Tooltip tooltip = default;
        [SerializeField] private List<string> tipsToGive = default;

        private void Update()
        {
            if (Input.GetKeyDown("t"))
                GiveRandomTooltip();
        }

        public void GiveRandomTooltip() 
        {
            if (tipsToGive.Count <= 0)
                return;

            string mess = tipsToGive[Random.Range(0, tipsToGive.Count)];
            tooltip.Show(mess);
            tipsToGive.Remove(mess);
        }
    }
}