using UnityEngine;

namespace ThreeDimensionalMarkovPen.Version2._0
{
    public class CursorMovementHandler : MonoBehaviour
    {
        public Transform cameraTransform;
        public bool allowCursorFreeMovement = false;
        public float cursorDistance = 5.0f;
        public float minCursorDistance = 1.0f;
        public float scrollSpeed = 1.0f;
        

        private Transform cursorTransform;
        private InputController inputController;

        private void Start()
        {
            cursorTransform = transform;
            inputController = GetComponent<InputController>();
        }

        private void Update()
        {
            AllowFreeMovement();
            UpdateCursorMovement();
        }

        private void UpdateCursorMovement()
        {
            if (!allowCursorFreeMovement)
                FollowCamera();
            else
                MoveCursorWithInput();
        }

        private void AllowFreeMovement()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                allowCursorFreeMovement = !allowCursorFreeMovement;

        }

        private void FollowCamera()
        {
            cursorTransform.position = cameraTransform.position + cameraTransform.forward * cursorDistance;
        }

        private void MoveCursorWithInput()
        {
            if (allowCursorFreeMovement)
            {
                float scrollDelta = Input.mouseScrollDelta.y;
                cursorDistance -= scrollDelta * scrollSpeed;
                cursorDistance = Mathf.Clamp(cursorDistance, minCursorDistance, Mathf.Infinity);
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
                    new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, cursorDistance));
                transform.position = cursorWorldPosition;
            }
            else
            {
                transform.Translate(inputController.GetMovementInput());
                //transform.Rotate(inputController.GetRotationInput());
                transform.Rotate(Vector3.right, inputController.GetRotationInput().xRotation, Space.Self);
                transform.Rotate(Vector3.up, inputController.GetRotationInput().yRotation, Space.Self);
            }
        }
        
    }
    
    
}