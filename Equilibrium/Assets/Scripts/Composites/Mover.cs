using UnityEngine;

namespace Equilibrium
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb = default;

        private Vector3 velocity;
        private Vector3 idealVelocity;
        private float fallingSpeed;

        private void Start()
        {
            rb.sleepThreshold = 0f;
        }

        public void SetIdealVelocity(Vector3 idealVelocity) 
        {
            idealVelocity.y = 0f;
            if (idealVelocity != Vector3.zero)
                transform.forward = idealVelocity;

            this.idealVelocity = idealVelocity;
        }

        public void SetFallingSpeed(float fallingSpeed) => this.fallingSpeed = fallingSpeed;

        private void FixedUpdate() => Move();

        private void Move()
        {
            velocity = rb.velocity;

            /*if (fallingSpeed < 1f)
                fallingSpeed = 1f;*/

            velocity.y /= fallingSpeed;

            //rb.AddForce(-velocity);
            rb.AddForce(idealVelocity - velocity, ForceMode.VelocityChange);
        }

        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            idealVelocity = Vector3.zero;
        }
    }
}