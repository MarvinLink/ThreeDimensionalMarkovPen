using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using VRSketchingGeometry.Commands.Line;

namespace ThreeDimensionalMarkovPen.Version2._0
{
    using UnityEngine;
    using VRSketchingGeometry.Commands;
    using VRSketchingGeometry.SketchObjectManagement;

    namespace ThreeDimensionalMarkovPen.Version2._0
    {
        public class ProjectionPointFinder : MonoBehaviour
        {
            private CommandInvoker Invoker;
            private LineCreationHandler LineCreationHandler;
            private PointAddingHandler PointAddingHandler;
            public CursorMovementHandler CursorMovementHandler;

            public ProjectionPointFinder(CommandInvoker invoker, LineCreationHandler lineCreationHandler)
            {
                Invoker = invoker;
                LineCreationHandler = lineCreationHandler;
            }


            /*public void PlacePoints(LineSketchObject lineSketchObject, List<Vector3> ProjectionPoints)
            {
                if (ProjectionPoints != null && lineSketchObject != null)
                {
                    var projectionPoints =  ProjectionPoints;
                    /*Vector3 pointBefore = projectionPoints.First();
                    Vector3 pointBeforeBefore = new Vector3(0, 0, 0);#1#
                    if (projectionPoints.Count > 0)
                    {
                        projectionPoints.ForEach(x =>
                        {
                            if (x != null) 
                            {
                                
                                float sphereScale = 0.3f;
                                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                sphere.transform.position = x;
                                sphere.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
                            }
                        });
                    }
                }*/
            
            public void CreatePointsOnTheLine()
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                var cursorWorldPosition = new Vector3(0,0,0);
                if (PointAddingHandler.cursorWorldPosition != new Vector3(0,0,0))
                {
                     cursorWorldPosition = PointAddingHandler.cursorWorldPosition;
                }
               
                if(LineCreationHandler.activeCurve != null)
                {
                    Debug.Log($"Mouse Screen Position: {mouseScreenPosition}, Cursor World Position: {cursorWorldPosition}");

                    LineCreationHandler.activeCurve.PointsOnTheLine.Add(cursorWorldPosition);
                    Debug.Log($"Added cursorWorldPosition to PointsOnTheLine. Total points: {LineCreationHandler.activeCurve.PointsOnTheLine.Count}");
                }
                else
                {
                    Debug.LogError("activeCurve is null!");
                }
            }

            public void PlacePoints(LineSketchObject lineSketchObject, List<Vector3> ControlPoints,
                Vector3 cursorWorldPosition, float pointSpacing)
            {
                if (ControlPoints != null && lineSketchObject != null)
                {
                    var controlPoints = ControlPoints;
                    Vector3 pointBefore = controlPoints.First();
                    Vector3 pointBeforeBefore = new Vector3(0, 0, 0);


                    if (controlPoints.Count > 2)
                    {
                        Vector3 direction = (cursorWorldPosition - pointBefore).normalized;
                        float distance = Vector3.Distance(pointBefore, cursorWorldPosition);

                        int numSpheres = Mathf.FloorToInt(distance / pointSpacing);
                        float step = distance / numSpheres;
                        var filteredVectors = controlPoints.Select(vector =>
                        {
                            var nearestVector = controlPoints.Where(otherVector =>
                                vector != otherVector && vector != pointBeforeBefore &&
                                Vector3.Distance(vector, otherVector) <= pointSpacing).ToList();

                            Debug.Log($"Vector: {vector}, Nearest Vectors: {string.Join(", ", nearestVector)}");

                            return (vector, nearestVector);
                        }).ToList();


                        if (filteredVectors.Count > 0)
                        {
                            Vector3 nextPoint = filteredVectors
                                .SelectMany(x => x.nearestVector)
                                .OrderBy(v => Vector3.Distance(v, cursorWorldPosition))
                                .FirstOrDefault();

                            if (nextPoint != null)
                            {
                                float sphereScale = 0.3f;
                                pointBeforeBefore = pointBefore;
                                pointBefore = nextPoint;
                                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                sphere.transform.position = nextPoint;
                                sphere.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
                            }
                        }
                    }
                }
            }
        }
    }
}