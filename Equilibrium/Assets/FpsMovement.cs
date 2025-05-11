using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    public class FpsMovement : MonoBehaviour, IWritable<Vector3>, IDieCallback
    {

        [SerializeField] private float speed = 6f;
        [SerializeField] private CreatureHandler creatureHandler = default;
        [SerializeField] private Creature creature = null;
        [SerializeField] private InspectorInterface<IWritable<Vector3>> idealVelocityWriter = default;

        private void Awake()
        {
            idealVelocityWriter.Setup();
            //creature = GetComponent<Creature>();
        }

        private void Start()
        {
            creature.Setup(this, creatureHandler);
            creature.ResetCreature();
        }

        /// <summary>
        /// Lol
        /// </summary>
        /// <param name="obj"></param>
        public void Write(Vector3 obj) 
        { 
            /*speed = obj.magnitude;
            if (speed <= 0f)
                speed = 6f;*/
        }

       // public void Write(float obj) => this.speed = obj;

        private void FixedUpdate()
        {
            creature.ProcessFixedFrame();

            Vector3 movement = (transform.right * Input.GetAxisRaw("Horizontal")) + (transform.forward * Input.GetAxisRaw("Vertical"));
            movement.Normalize();

            idealVelocityWriter.attached.Write(movement * speed);
            
        }

        public void DieCallback(int faction) { }
    }
}