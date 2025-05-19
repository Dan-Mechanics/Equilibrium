using UnityEngine;

namespace Equilibrium
{
    public class PanelHandler : MonoBehaviour
    {
        [SerializeField] private int startingPanelIndex = default;
        [SerializeField] private Transform panelHolder = default;

        private GameObject[] panels;

        private void Start()
        {
            if (panelHolder == null)
            {
                Debug.LogError("if (panelHolder == null)");
                Destroy(this);
                return;
            }
            
            panels = new GameObject[panelHolder.childCount];
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i] = panelHolder.GetChild(i).gameObject;
            }

            ShowPanel(startingPanelIndex);
        }

        public void ShowPanel(int index) 
        {
            if(panels == null)
                return;

            if (panels.Length <= 0)
                return;

            HideAll();

            if (index < 0 || index >= panels.Length)
                return;

            panels[index].SetActive(true);
        }

        public void HideAll() 
        {
            if (panels == null )
                return;

            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }
        }

        private void OnValidate()
        {
            if (panelHolder == null)
                return;

            for (int i = 0; i < panelHolder.childCount; i++)
            {
                panelHolder.GetChild(i).name = $"panel_{i}";
                panelHolder.GetChild(i).gameObject.SetActive(i == startingPanelIndex);
            }
        }
    }
}