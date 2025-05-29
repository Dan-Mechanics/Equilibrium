using UnityEngine;

namespace Equilibrium
{
    public class MaterialMatcher : MonoBehaviour
    {
        private MeshRenderer rend;
        private MeshRenderer parentRend;

        private void Start()
        {
            rend = GetComponent<MeshRenderer>();
            parentRend = transform.root.GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            rend.material = parentRend.material;
        }
    }
}