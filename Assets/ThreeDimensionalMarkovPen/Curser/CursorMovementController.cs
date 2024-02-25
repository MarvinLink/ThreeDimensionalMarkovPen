using UnityEngine;

namespace ThreeDimensionalMarkovPen
{
    public class CursorMovementController:MonoBehaviour
    {
        private Transform cursorTransform;
        private Transform cameraTransform;
        private float cursorDistance;
        private float minCursorDistance;
        private bool allowCursorFreeMovement;
        private InputController inputController;
        private float scrollSpeed;

        public CursorMovementController(Transform cursorTransform, Transform cameraTransform, float cursorDistance,
            float minCursorDistance, bool allowCursorFreeMovement, InputController inputController, float scrollSpeed)
        {
            this.cursorTransform = cursorTransform;
            this.cameraTransform = cameraTransform;
            this.cursorDistance = cursorDistance;
            this.minCursorDistance = minCursorDistance;
            this.allowCursorFreeMovement = allowCursorFreeMovement;
            this.inputController = inputController;
            this.scrollSpeed = scrollSpeed;
        }

        public void Update()
        {
            if (!allowCursorFreeMovement)
                FollowCamera();
            else
                MoveCursorWithInput();
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
                cursorTransform.position = cursorWorldPosition;
            }
            else
            {
                cursorTransform.Translate(inputController.GetMovementInput());
                //cursorTransform.Translate(inputController.GetRotationInput());
                cursorTransform.Rotate(Vector3.right, inputController.GetRotationInput().xRotation, Space.Self);
                cursorTransform.Rotate(Vector3.up, inputController.GetRotationInput().yRotation, Space.Self);
            }
        }
    }
}