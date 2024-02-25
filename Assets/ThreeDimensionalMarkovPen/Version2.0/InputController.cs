using UnityEngine;
using UnityEngine.Animations;

namespace ThreeDimensionalMarkovPen
{
    public class InputController : MonoBehaviour
    {
        public float moveSpeed = 5.0f;
        public float rotationSpeed = 5.0f;

        private bool _isRotating = false;
        private bool _isMovingInX = false;
        private bool _isMovingInY = false;
        private bool _isMovingInZ = false;
        private Vector3 _lastMousePosition;

        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                AllowMovementInX();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                AllowMovementInY();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                AllowMovementInZ();
            }

            transform.Translate(GetMovementInput());
            //transform.Translate(GetRotationInput());
            transform.Rotate(Vector3.right, GetRotationInput().xRotation, Space.Self);
            transform.Rotate(Vector3.right, GetRotationInput().yRotation, Space.Self);
        }

        public Vector3 GetMovementInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = 0;
            float depth = Input.GetAxis("Vertical");


            if (_isMovingInX)
            {
                horizontal = 0;
            }

            if (_isMovingInY)
            {
                vertical = Input.GetAxis("Mouse ScrollWheel");
            }

            if (_isMovingInZ)
            {
                depth = 0;
            }


            Vector3 movement = new Vector3(horizontal, vertical, depth) * moveSpeed * Time.deltaTime;
            return movement;
        }


        public (float xRotation, float yRotation) GetRotationInput()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float yRotation= 0;
            float xRotation= 0;
            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            Vector3 rotation = new Vector3(0, 0, 0);


            if (Input.GetMouseButtonDown(1))
            {
                _isRotating = true;
                _lastMousePosition = Input.mousePosition;
            }


            if (Input.GetMouseButtonUp(1))
            {
                _isRotating = false;
            }

            if (_isRotating)
            {
                Vector3 currentMousePosition = Input.mousePosition;
                Vector3 mouseDelta = currentMousePosition - _lastMousePosition;
                _lastMousePosition = currentMousePosition;
                /*if (Mathf.Abs(mouseDelta.x) > Mathf.Abs(mouseDelta.y))
                {
                    if (mouseDelta.x > 0)
                    {
                        rotation.y = 90f;
                    }
                    else
                    {
                        rotation.y = -90f;
                    }
                }
                else
                {
                    if (mouseDelta.y > 0)
                    {
                        rotation.x = -90f;
                    }
                    else
                    {
                        rotation.x = 90f;
                    }
                }*/

                yRotation = mouseX * rotationSpeed * Time.deltaTime;
                xRotation = -mouseY * rotationSpeed * Time.deltaTime;
                //rotation = new Vector3( yRotation , xRotation, 0 ) ;

            }

            return (xRotation,yRotation);
            //return rotation;
        }

        private void AllowMovementInX()
        {
            _isMovingInX = !_isMovingInX;
        }

        private void AllowMovementInY()
        {
            _isMovingInY = !_isMovingInY;
        }

        private void AllowMovementInZ()
        {
            _isMovingInZ = !_isMovingInZ;
        }

        public void SetMovementInZ(bool gravity)
        {
            _isMovingInZ = gravity;
        }
        
    }
}