using VRSketchingGeometry.SketchObjectManagement;
using UnityEngine;

namespace ThreeDimensionalMarkovPen.Version2._0
{
    public class ControlPointGizmo : MonoBehaviour
    {
        public LineSketchObject lineSketchObject;

        private Curve Curve;
        private void OnDrawGizmos()
        {
            //Debug.Log("Drawing Gizmos");
            if (lineSketchObject != null)
            {
                foreach (var controlPoint in Curve.PointsOnTheLine)
                {
                    //Debug.Log("Drawing control point");
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(controlPoint, 0.05f); // Adjust sphere size as needed
                }
            }
        }
    }
}

