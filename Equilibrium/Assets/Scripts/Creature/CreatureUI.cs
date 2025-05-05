using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Equilibrium
{
    /// <summary>
    /// Have 3 bars,
    /// the bars display the count of the factions and display a bar which is x / max_count
    /// if the count is zero or the faction doesnt exist, display nothing.
    /// </summary>
    public class CreatureUI : MonoBehaviour, IPassable<int[]>
    {
        [SerializeField] private Transform barsHolder = default;
        [SerializeField] private CreatureData creatureData = default;

        // agian, could use interfaces for this...
        //[SerializeField] private CreatureHandler handler = default;

        private FactionUI[] displays;

        public void Pass(ref int[] t) => Display(ref t);

        private void Awake()
        {
            displays = new FactionUI[barsHolder.childCount];

            for (int i = 0; i < displays.Length; i++)
            {
                displays[i] = new FactionUI(barsHolder.GetChild(i));
            }

            //handler.OnNewFactionsTally += Display;
        }

        private void Display(ref int[] factionsTally)
        {
            for (int i = 0; i < factionsTally.Length; i++)
            {
                displays[i].image.fillAmount = (float)factionsTally[i] / creatureData.creatureSpawnCount;
                displays[i].text.text = $"{factionsTally[i]} / {creatureData.creatureSpawnCount}";

                displays[i].boss.SetActive(displays[i].image.fillAmount > 0f);
            }
        }

        [Serializable]
        public struct FactionUI 
        {
            public Image image;
            public TMP_Text text;
            [HideInInspector] public GameObject boss;

            public FactionUI(Transform t)
            {
                image = t.Find("bar").GetComponent<Image>();
                text = t.Find("text").GetComponent<TMP_Text>();
                boss = t.gameObject;
            }
        }
    }
}