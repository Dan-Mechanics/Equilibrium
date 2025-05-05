using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public class LittleCreature : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb = default;
        
        private int movementCounter;
        private int globalCounter;
        private int food;
        [SerializeField] private LittleCreatureBrain brain = default;

        private void Start()
        {
            Setup(brain);
        }

        public void Setup(LittleCreatureBrain parent) 
        {
            movementCounter = 0;
            globalCounter = 0;
            food = 0;

            brain = parent;
            brain.Mutate();

            CancelInvoke(nameof(Move));
            InvokeRepeating(nameof(Move), 0f, 1f);
        }

        private void Move() 
        {
            rb.AddForce(brain.movements[movementCounter], ForceMode.VelocityChange);
            movementCounter++;

            if (movementCounter >= brain.movements.Length)
            {
                movementCounter = 0;
                //Terminate();

                globalCounter++;
            }

            if (globalCounter > brain.movements.Length)
            {
                Terminate();
            }
        }

        private void Terminate()
        {
            CancelInvoke(nameof(Move));

            /*for (int i = 0; i < (food > 0 ? 2 : 1); i++)
            {
                Instantiate(gameObject, transform.position, transform.rotation).GetComponent<LittleCreature>().Setup(brain);
            }*/

            if (food > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(gameObject, transform.position, transform.rotation).GetComponent<LittleCreature>().Setup(brain);
                }

            }

            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Food")) 
            {
                food++;
                collision.gameObject.tag = "Untagged";
                Destroy(collision.gameObject);
                Terminate();
            }
        }

        [System.Serializable]
        public struct LittleCreatureBrain
        {
            public Vector3[] movements;

            public void Mutate()
            {
                for (int i = 0; i < movements.Length; i++)
                {
                    movements[i] += Random.insideUnitSphere * 5f;
                }
            }
        }
    }
}