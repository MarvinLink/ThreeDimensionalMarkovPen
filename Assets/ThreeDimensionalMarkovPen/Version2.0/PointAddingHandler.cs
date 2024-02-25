using System;
using System.Collections.Generic;
using System.Linq;
using ThreeDimensionalMarkovPen.Version2._0.ThreeDimensionalMarkovPen.Version2._0;
using Unity.VisualScripting;
using UnityEngine;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;

namespace ThreeDimensionalMarkovPen.Version2._0
{
    public class PointAddingHandler : MonoBehaviour
    {
        public CommandInvoker Invoker;
        public Transform cameraTransform;
        public CursorMovementHandler CursorMovementHandler;
        public LineCreationHandler LineCreationHandler;
        public ProjectionPointFinder ProjectionPointFinder;
        public float pointSpacing = 0.5f;
        public Vector3 cursorWorldPosition;
        private Vector3 firstPoint;


        private void Start()
        {
            Invoker = new CommandInvoker();
            ProjectionPointFinder = new ProjectionPointFinder(Invoker, LineCreationHandler);
        }

        private void Update()
        {
            //CreatePointsOnTheLine();
            if (CursorMovementHandler.allowCursorFreeMovement)
            {
                AddControlPoint();
            }
        }


        private void AddControlPoint()
        {
            if (CursorMovementHandler.allowCursorFreeMovement && LineCreationHandler.initializedLine == true)
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
                    new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, CursorMovementHandler.cursorDistance));

                if (LineCreationHandler.activeLineSketchObject != null && LineCreationHandler.activeCurve == LineCreationHandler.currentCurve)
                    /*{
                        if (LineCreationHandler.activeCurve.ProjectionPoints == null)
                        {
                            firstPoint = cursorWorldPosition;
                        }
    
                        
                        ProjectionPointFinder.PlacePoints(LineCreationHandler.activeLineSketchObject,
                            LineCreationHandler.activeCurve.ProjectionPoints);*/
                    Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.activeLineSketchObject, cursorWorldPosition));


                ProjectionPointFinder.CreatePointsOnTheLine();

                ProjectionPointFinder.PlacePoints(LineCreationHandler.activeLineSketchObject,
                    LineCreationHandler.activeCurve.PointsOnTheLine, cursorWorldPosition, pointSpacing);
            }

            if (LineCreationHandler.continuousLineSketchObject != null &&
                LineCreationHandler.newContinuousCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(
                    LineCreationHandler.continuousLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.newContinuousCurve.PointsOnTheLine.Add(cursorWorldPosition);
                //ProjectionPointFinder.PlacePoints(LineCreationHandler.continuousLineSketchObject,LineCreationHandler.newContinuousCurve.ControlPoints, cursorWorldPosition, pointSpacing);
            }
            else if (LineCreationHandler.baseLineSketchObject != null &&
                     LineCreationHandler.newBaseCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.baseLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.newBaseCurve.PointsOnTheLine.Add(cursorWorldPosition);
                //ProjectionPointFinder.PlacePoints(LineCreationHandler.baseLineSketchObject,LineCreationHandler.newBaseCurve.ControlPoints, cursorWorldPosition, pointSpacing);
            }
            else if (LineCreationHandler.styleLineSketchObject != null &&
                     LineCreationHandler.newStyleCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.styleLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.newStyleCurve.PointsOnTheLine.Add(cursorWorldPosition);
                //ProjectionPointFinder.PlacePoints(LineCreationHandler.styleLineSketchObject,LineCreationHandler.newStyleCurve.ControlPoints, cursorWorldPosition, pointSpacing);
            }
        }
    }


    /// <summary>
    /// Seems To Work
    /// </summary>
    /*public void CreatePointsOnTheLine()
    {
        var activeCurve = LineCreationHandler.activeCurve;
        Vector3 mouseScreenPosition = Input.mousePosition;
        cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, CursorMovementHandler.cursorDistance));

        //Debug.Log($"Mouse Screen Position: {mouseScreenPosition}, Cursor World Position: {cursorWorldPosition}");

        if (activeCurve != null)
        {
            activeCurve.PointsOnTheLine.Add(cursorWorldPosition);
            //Debug.Log($"Added cursorWorldPosition to PointsOnTheLine. Total points: {activeCurve.PointsOnTheLine.Count}");
        }
        else
        {
            //Debug.LogError("activeCurve is null!");
        }
    }*/


    /*public Vector3 GenerateCurserWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, CursorMovementHandler.cursorDistance));
        return cursorWorldPosition;
    }*/

    /*public List<Vector3> GenerateProjectionPoint()
    {
        var projectionPoints = new List<Vector3>();
        var pointsOnTheLine = LineCreationHandler.activeCurve.PointsOnTheLine;
        Debug.Log(pointsOnTheLine);
        var PointBefore = firstPoint;
        if (pointsOnTheLine.Count > 0)
        {
            var filteredVector = pointsOnTheLine.Select(vector =>
            {
                


                var nearestVector = pointsOnTheLine
                    .Where(otherVector => vector != otherVector &&
                                          Math.Abs(Vector3.Distance(vector, otherVector) - pointSpacing) < 1f)
                    .Select(x => (x, Math.Abs(Vector3.Distance(vector, x))))
                    .OrderBy(pair => pair.Item2)
                    .FirstOrDefault().x;
                Debug.Log($"Vector: {vector}, Nearest Vectors: {string.Join(", ", nearestVector)}");

                var result = (vector, nearestVector);
                return result;
            }).ToList();
            projectionPoints.Add(PointBefore);
            var updatedPointBefore = filteredVector.Aggregate(PointBefore, (currentPointBefore, pair) =>
            {
                var vector = pair.vector;
                var nearestVector = pair.nearestVector;

                //Debug.Log($"Vector: {vector}, Nearest Vectors: {string.Join(", ", nearestVector)}");
                if (currentPointBefore == vector)
                {
                    projectionPoints.Add(nearestVector);
                    Debug.Log("added Point");
                    return nearestVector;
                }

                return currentPointBefore;
            });
        }

        return projectionPoints;
    }*/


//Debug.Log($"Vector: {vector}, Nearest Vectors: {string.Join(", ", nearestVector)}");


    /*private void AddControlPoint()
    {
        if (CursorMovementHandler.allowCursorFreeMovement && LineCreationHandler.initializedLine == true)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
                new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, CursorMovementHandler.cursorDistance));

            if (LineCreationHandler.activeLineSketchObject != null &&
                LineCreationHandler.activeCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.activeLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.activeCurve.PointsOnTheLine.Add(cursorWorldPosition);

                ProjectionPointFinder.PlacePoints(LineCreationHandler.activeLineSketchObject,
                    LineCreationHandler.activeCurve.PointsOnTheLine, cursorWorldPosition, pointSpacing);
            }

            if (LineCreationHandler.continuousLineSketchObject != null &&
                LineCreationHandler.newContinuousCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.continuousLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.newContinuousCurve.PointsOnTheLine.Add(cursorWorldPosition);
                ProjectionPointFinder.PlacePoints(LineCreationHandler.continuousLineSketchObject,
                    LineCreationHandler.newContinuousCurve.PointsOnTheLine, cursorWorldPosition, pointSpacing);
            }
            else if (LineCreationHandler.baseLineSketchObject != null &&
                     LineCreationHandler.newBaseCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.baseLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.newBaseCurve.PointsOnTheLine.Add(cursorWorldPosition);
                ProjectionPointFinder.PlacePoints(LineCreationHandler.baseLineSketchObject,
                    LineCreationHandler.newBaseCurve.PointsOnTheLine, cursorWorldPosition, pointSpacing);
            }
            else if (LineCreationHandler.styleLineSketchObject != null &&
                     LineCreationHandler.newStyleCurve == LineCreationHandler.currentCurve)
            {
                Invoker.ExecuteCommand(new AddControlPointCommand(LineCreationHandler.styleLineSketchObject,
                    cursorWorldPosition));
                LineCreationHandler.newStyleCurve.PointsOnTheLine.Add(cursorWorldPosition);
                ProjectionPointFinder.PlacePoints(LineCreationHandler.styleLineSketchObject,
                    LineCreationHandler.newStyleCurve.PointsOnTheLine, cursorWorldPosition, pointSpacing);
            }
        }
    }*/
}