using UnityEngine;

namespace Equilibrium
{
    public class GameInitiator : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs = default;

        private void Start()
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                Instantiate(prefabs[i], prefabs[i].transform.position, prefabs[i].transform.rotation);
            }
        }
    }
}