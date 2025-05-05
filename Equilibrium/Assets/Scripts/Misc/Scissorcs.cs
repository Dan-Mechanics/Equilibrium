using System;
using UnityEngine;
using UnityEngine.Events;

namespace OuterWilds
{
    public class Scissors : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private Camera cam = null;
        [SerializeField] private LayerMask placeMask = default;
        [SerializeField] private LayerMask destroyMask = default;
        [SerializeField] private GameObject selectedPrefab = default;

        [Header("Settings")]

        [SerializeField] private float maxCutRange = 0f;
        //[SerializeField] private KeyCode cutKey = KeyCode.None;

        //private readonly RandomRotation randomRotator = new RandomRotation();

        

        private void Update()
        {
            if (selectedPrefab == null)
                return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxCutRange, placeMask, QueryTriggerInteraction.Ignore))
                {
                    GameObject go = Instantiate(selectedPrefab, hit.point, selectedPrefab.transform.rotation);
                    //randomRotator.Rotate(go.transform);

                    if (go.TryGetComponent(out ISetupable setupable))
                        setupable.Setup();
                    //selectedPrefab = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxCutRange, destroyMask, QueryTriggerInteraction.Ignore))
                {
                    Destroy(hit.transform.root.gameObject);
                }
            }
        }

        //public void SetPrefab(GameObject prefab) { this.selectedPrefab = prefab; }
    }
}