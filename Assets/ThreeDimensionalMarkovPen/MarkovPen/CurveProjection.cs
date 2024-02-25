using System.Collections.Generic;
using UnityEngine;

namespace ThreeDimensionalMarkovPen
{
    public class CurveProjection : MonoBehaviour
    {
        public List<Vector3> baseCurvePoints;
        public List<Vector3> styleCurvePoints;

        void Start()
        {
            // Generate baseCurvePoints and styleCurvePoints...

            List<Vector3> projectedPoints = ProjectStyleCurveOnBaseCurve(baseCurvePoints, styleCurvePoints);
        }

        List<Vector3> ProjectStyleCurveOnBaseCurve(List<Vector3> baseCurve, List<Vector3> styleCurve)
        {
            List<Vector3> projectedPoints = new List<Vector3>();

            foreach (Vector3 stylePoint in styleCurve)
            {
                Vector3 closestBasePoint = FindClosestPoint(stylePoint, baseCurve);
                Vector3 projectionVector = closestBasePoint - stylePoint;
                Vector3 projectedPoint = stylePoint + projectionVector;

                projectedPoints.Add(projectedPoint);
            }

            return projectedPoints;
        }

        Vector3 FindClosestPoint(Vector3 point, List<Vector3> curve)
        {
            Vector3 closestPoint = Vector3.zero;
            float closestDistance = Mathf.Infinity;

            foreach (Vector3 curvePoint in curve)
            {
                float distance = Vector3.Distance(point, curvePoint);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = curvePoint;
                }
            }

            return closestPoint;
        }
    }
}