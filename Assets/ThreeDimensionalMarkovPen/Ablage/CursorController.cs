using System;
using UnityEngine;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.Commands.Ribbon;
using VRSketchingGeometry.Commands.Patch;
using VRSketchingGeometry.Commands.Group;
using VRSketchingGeometry.Commands.Selection;
using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry.Serialization;
using VRSketchingGeometry.Meshing;
using VRSketchingGeometry.Export;

namespace ThreeDimensionalMarkovPen
{
    public class CursorController : MonoBehaviour
    {
        public Transform cameraTransform;
        public float cursorDistance = 5.0f;
        public float minCursorDistance = 1.0f;
        public bool allowCursorFreeMovement = false;
        public InputController inputController;
        public float scrollSpeed = 1.0f;
        public LineSketchObject _lineSketchObject;
        [SerializeField] private bool isAddingPoints = false;
        public bool continuousLine = false;
        private Vector3 _lastPointPosition;
        private LineSketchObject activeLineSketchObject;
        private LineSketchObject styleLineSketchObject;
        private LineSketchObject baseLineSketchObject;

        private CommandInvoker Invoker;
        public DefaultReferences Defaults;
        public SketchWorld SketchWorld;


        public LineSketchObject GetStyleLineSketchObject()
        {
            return styleLineSketchObject;
        }
        public LineSketchObject GetBaseLineSketchObject()
        {
            return baseLineSketchObject;
        }
        private void Start()
        {
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            styleLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            baseLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            _lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker = new CommandInvoker();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                allowCursorFreeMovement = !allowCursorFreeMovement;

            if (!allowCursorFreeMovement)
                FollowCamera();
            else
                MoveCursorWithInput();
            if (Input.GetKeyDown(KeyCode.C))
            {
                continuousLine = !continuousLine;
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                NewLine("new");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                NewLine("base");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                NewLine("style");
            }

            if (!continuousLine)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartNewLine();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    EndActiveLine();
                }
            }

            if (Input.GetMouseButton(0))
            {
                AddPointAndControlPoint();
            }
        }

        private void FollowCamera()
        {
            transform.position = cameraTransform.position + cameraTransform.forward * cursorDistance;
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
                transform.Rotate(Vector3.right, inputController.GetRotationInput().xRotation, Space.Self);
                transform.Rotate(Vector3.up, inputController.GetRotationInput().yRotation, Space.Self);
                //transform.Rotate(inputController.GetRotationInput());
            }
        }


        private void RotateCursorWithInput()
        {
            Vector3 cursorMovement = inputController.GetMovementInput();
            transform.Translate(cursorMovement);
        }

        private void AddPointWithInput()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
                new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, cursorDistance));
            transform.position = cursorWorldPosition;
            _lastPointPosition = cursorWorldPosition;
        }

        private void AddPointAndControlPoint()
        {
            if (allowCursorFreeMovement)
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 cursorWorldPosition = cameraTransform.GetComponent<Camera>().ScreenToWorldPoint(
                    new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, cursorDistance));

                if (_lineSketchObject != null && continuousLine)
                {
                    Invoker.ExecuteCommand(new AddControlPointCommand(_lineSketchObject, cursorWorldPosition));
                }
                else if (activeLineSketchObject != null && !continuousLine)
                {
                    Invoker.ExecuteCommand(new AddControlPointCommand(activeLineSketchObject, cursorWorldPosition));
                }
                else if (baseLineSketchObject != null && continuousLine)
                {
                    Invoker.ExecuteCommand(new AddControlPointCommand(activeLineSketchObject, cursorWorldPosition));
                }
                else if (styleLineSketchObject != null && continuousLine)
                {
                    Invoker.ExecuteCommand(new AddControlPointCommand(activeLineSketchObject, cursorWorldPosition));
                }
                else
                {
                    Debug.LogWarning("LineSketchObject is not properly initialized.");
                }
            }
        }

        private LineSketchObject CreateLineSketchObjectIfNotExists()
        {
            // Check if a LineSketchObject already exists, if not, create a new one
            LineSketchObject newLineSketchObject = FindObjectOfType<LineSketchObject>();

            if (newLineSketchObject == null)
            {
                newLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
                // You might want to set additional properties of the newLineSketchObject here if needed
            }

            return newLineSketchObject;
        }

        private void StartNewLine()
        {
            if (allowCursorFreeMovement)
            {
                activeLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            }
        }

        private void EndActiveLine()
        {
            if (activeLineSketchObject != null)
            {
                activeLineSketchObject = null; 
            }
        }

        private LineSketchObject NewLine(string type)
        {
            LineSketchObject lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

            if (type == "style")
            {   
                StartNewLine();
                styleLineSketchObject = lineSketchObject;
            }
            else if (type == "base")
            {
                StartNewLine();
                baseLineSketchObject = lineSketchObject;
            }
            else if (type == "new")
            {
                _lineSketchObject = lineSketchObject;
            }
            else
            {
                Debug.LogError("Invalid line type.");
            }

            return lineSketchObject;
        }
        



    }
}