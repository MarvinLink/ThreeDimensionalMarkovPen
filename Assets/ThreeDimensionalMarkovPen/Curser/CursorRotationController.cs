using UnityEngine;

namespace ThreeDimensionalMarkovPen
{
    public class CursorRotationController:MonoBehaviour
    {
        private Transform cursorTransform;
        private InputController inputController;

        public CursorRotationController(Transform cursorTransform, InputController inputController)
        {
            this.cursorTransform = cursorTransform;
            this.inputController = inputController;
        }

        public void Update()
        {
            Vector3 cursorMovement = inputController.GetMovementInput();
            cursorTransform.Translate(cursorMovement);
        }
    }
}