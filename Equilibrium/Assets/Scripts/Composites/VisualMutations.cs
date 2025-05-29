using UnityEngine;

namespace Equilibrium
{
    public class VisualMutations : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs = default;

        private void Start()
        {
            GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
        }
    }
}