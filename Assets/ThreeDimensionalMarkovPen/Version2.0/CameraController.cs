using System;

namespace ThreeDimensionalMarkovPen
{
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public InputController inputController;

        private bool _allowCameraMovement = true;
        private bool _allowCameraRotation = true;

        void Update()
        {
            if (_allowCameraMovement)
            {
                MoveCameraWithInput();
            }

            if (_allowCameraRotation)
            {
                RotateCameraWithInput();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToggleCameraMovement();
                ToggleCameraRotation();
            }
        }

        private void ToggleCameraMovement()
        {
            _allowCameraMovement = !_allowCameraMovement;
        }

        private void ToggleCameraRotation()
        {
            _allowCameraRotation = !_allowCameraRotation;
        }

        public void MoveCameraWithInput()
        {
            Vector3 cameraMovement = inputController.GetMovementInput();
            transform.Translate(cameraMovement);
        }

        public void RotateCameraWithInput()
        {
            //transform.Rotate(inputController.GetRotationInput() * (inputController.rotationSpeed * Time.deltaTime));

            float xRotation = inputController.GetRotationInput().xRotation * inputController.rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, xRotation, Space.Self);
            
            float yRotation = inputController.GetRotationInput().yRotation * inputController.rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, yRotation, Space.Self);
        }
    }
}