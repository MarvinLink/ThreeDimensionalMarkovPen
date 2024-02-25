using UnityEngine;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;

namespace ThreeDimensionalMarkovPen
{
    public class PointAdditionController:  MonoBehaviour
    
    {
        private Transform cursorTransform;
        private Transform cameraTransform;
        private LineSketchObject lineSketchObject;
        private CommandInvoker invoker;
        private bool allowCursorFreeMovement;
        private float cursorDistance;

       
        
        public PointAdditionController(Transform cursorTransform, Transform cameraTransform,
            LineSketchObject lineSketchObject, CommandInvoker invoker, bool allowCursorFreeMovement,
            float cursorDistance) 
        {
            this.cursorTransform = cursorTransform;
            this.cameraTransform = cameraTransform;
            this.lineSketchObject = lineSketchObject;
            this.invoker = invoker;
            this.allowCursorFreeMovement = allowCursorFreeMovement;
            this.cursorDistance = cursorDistance; 
        }

        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                AddPointAndControlPoint();
            }
        }

        private void AddPointAndControlPoint()
        {
            if (allowCursorFreeMovement)
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
                    new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, cursorDistance));

                if (lineSketchObject != null)
                {
                    invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, cursorWorldPosition));
                }
                else
                {
                    Debug.LogWarning("LineSketchObject is not properly initialized.");
                }
            }
        }
    }
}

