using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.SketchObjectManagement;

namespace ThreeDimensionalMarkovPen.Version2._0
{
    public class LineCreationHandler1 : MonoBehaviour

    {
        public enum LineType
        {
            Active,
            Continuous,
            Base,
            Style
        }

        public bool continuousLine = false;
        public DefaultReferences Defaults;
        public CursorMovementHandler CursorMovementHandler;

        public LineSketchObject activeLineSketchObject;
        public LineSketchObject styleLineSketchObject;
        public LineSketchObject baseLineSketchObject;
        public LineSketchObject continuousLineSketchObject;
        public SketchWorld SketchWorld;
        public LineType currentLineType;
        public bool initializedLine = false;

        private bool active;
        private bool continuous;
        private bool basis ;
        private bool style;

        private void Start()
        {
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            styleLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            baseLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            continuousLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            activeLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentLineType = LineType.Active;
                UpdateLineCreation(LineType.Active);
                activeLineSketchObject = NewLine();
            }
            if (currentLineType == LineType.Active)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    
                    StartNewLine(LineType.Active);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    EndActiveLine(LineType.Active);
                }
            }
            
            
            if (Input.GetKeyDown(KeyCode.N))
            {
                currentLineType = LineType.Continuous;
                UpdateLineCreation(LineType.Continuous);
                continuousLineSketchObject = NewLine();
            }
            else if (Input.GetKeyUp(KeyCode.N))
            {
                EndActiveLine(LineType.Continuous);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                currentLineType = LineType.Base;
                UpdateLineCreation(LineType.Base);
                baseLineSketchObject = NewLine();
            }
            else if (Input.GetKeyUp(KeyCode.B))
            {
                EndActiveLine(LineType.Base);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                currentLineType = LineType.Style;
                UpdateLineCreation(LineType.Style);
                styleLineSketchObject = NewLine();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                EndActiveLine(LineType.Style);
            }

           
        }

        private LineSketchObject NewLine()
        {
            initializedLine = true;
            LineSketchObject initializedLineSketchObject =
                Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            return initializedLineSketchObject;
        }

        private void UpdateLineCreation(LineType lineType )
        {
            if (lineType == LineType.Active)
            {
                continuousLine = false;
            }
            else
            {
                continuousLine = !continuousLine;
            }
        }

        private void StartNewLine(LineType currenLineType)
        {
            if (CursorMovementHandler.allowCursorFreeMovement )
            {
                if (currenLineType == LineType.Active)
                {
                    activeLineSketchObject =
                        Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
                }
                else if (currenLineType == LineType.Base)
                {
                    baseLineSketchObject =
                        Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
                }
                else if (currenLineType == LineType.Style)
                {
                    styleLineSketchObject =
                        Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
                }
            }
        }

        private void EndActiveLine(LineType linetype)
        {
            if (linetype == LineType.Active)
            {
                activeLineSketchObject = null;
            }
            else if (linetype == LineType.Continuous)
            {
                continuousLineSketchObject = null;
            }
            else if (linetype == LineType.Base)
            {
                baseLineSketchObject = null;
            }
            else if (linetype == LineType.Style)
            {
                styleLineSketchObject = null;
            }

        }

            
    }
}