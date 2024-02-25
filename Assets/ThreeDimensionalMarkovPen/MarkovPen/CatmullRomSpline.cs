using UnityEngine;
public class CatmullRomSpline : MonoBehaviour
{
    public Transform[] controlPoints; // control points for CatmullRomSpline
    public int resolution = 10; // number of points on the spline

    private Vector3[] splinePoints; // calculated points on the spline

    private void Start()
    {
        //make sure number of control points is valid
        if (controlPoints.Length < 4)
        {
            Debug.LogError("Not enough control points to calculate Catmull-Rom Spline.");
            return;
        }

        // calculates spline points
        CalculateSplinePoints();
    }

    private void CalculateSplinePoints()
    {
        splinePoints = new Vector3[(controlPoints.Length - 3) * resolution + 1];
        int index = 0;

        for (int i = 0; i < controlPoints.Length - 3; i++)
        {
            for (int j = 0; j <= resolution; j++)
            {
                float t = (float)j / resolution;
                splinePoints[index] = CalculateCatmullRomPoint(t, controlPoints[i].position, controlPoints[i + 1].position, controlPoints[i + 2].position, controlPoints[i + 3].position);
                index++;
            }
        }
    }

    private Vector3 CalculateCatmullRomPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // formula for CatmullRomSpline
        float t2 = t * t;
        float t3 = t2 * t;
        float c0 = -0.5f * t3 + t2 - 0.5f * t;
        float c1 = 1.5f * t3 - 2.5f * t2 + 1.0f;
        float c2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
        float c3 = 0.5f * t3 - 0.5f * t2;

        return c0 * p0 + c1 * p1 + c2 * p2 + c3 * p3;
    }

    private void OnDrawGizmos()
    {
        // Zeichnen Sie den Spline mit Linien in der Szeneansicht
        if (splinePoints != null && splinePoints.Length > 1)
        {
            for (int i = 0; i < splinePoints.Length - 1; i++)
            {
                Gizmos.DrawLine(splinePoints[i], splinePoints[i + 1]);
            }
        }
    }
}