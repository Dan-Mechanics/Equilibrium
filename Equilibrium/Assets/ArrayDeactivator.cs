using UnityEngine;

namespace Equilibrium
{
    public class ArrayDeactivator : MonoBehaviour
    {
        [SerializeField] private int onIndex = default;
        [SerializeField] private GameObject[] array = default;

        private void Start()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].SetActive(i == onIndex);
            }
        }
    }
}