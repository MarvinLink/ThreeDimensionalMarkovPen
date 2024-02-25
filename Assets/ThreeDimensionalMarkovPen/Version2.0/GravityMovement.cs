using UnityEngine;

namespace ThreeDimensionalMarkovPen
{
    public class GravityMovement : MonoBehaviour
    {
        [SerializeField] private bool isGrounded;
        [SerializeField] private float groundcheckDistance = (float)0.2;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float gravity = (float)-9.81;
        [SerializeField] private bool isGravityEnabled = true;

        private Vector3 movement;
        private Vector3 rotation;
        private Vector3 velocity;
        


        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                ToggleGravity();
            }

            Move();
        }

        private void Move()
        {
            if (isGravityEnabled)
            {
               
                isGrounded = Physics.CheckSphere(transform.position, groundcheckDistance, groundMask);
                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }


                if (isGrounded)
                {
                    
                    return;
                }

                velocity.y += gravity * Time.deltaTime;
                transform.Translate(velocity * Time.deltaTime);
            }
        }

        private void ToggleGravity()
        {
            isGravityEnabled = !isGravityEnabled;
            gravity = isGravityEnabled ? -9.81f : 0f;
        }
    }
}