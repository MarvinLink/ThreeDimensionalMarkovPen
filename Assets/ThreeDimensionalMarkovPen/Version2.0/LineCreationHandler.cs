using ThreeDimensionalMarkovPen.Curves.New;
using ThreeDimensionalMarkovPen.Version2._0.ThreeDimensionalMarkovPen.Version2._0;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.SketchObjectManagement;

namespace ThreeDimensionalMarkovPen.Version2._0
{
    public class LineCreationHandler : MonoBehaviour

    {
        public DefaultReferences Defaults;
        public CursorMovementHandler CursorMovementHandler;

        public LineSketchObject activeLineSketchObject;
        public LineSketchObject styleLineSketchObject;
        public LineSketchObject baseLineSketchObject;
        public LineSketchObject continuousLineSketchObject;
        public SketchWorld SketchWorld;
        public LineType currentLineType;
        public bool initializedLine = false;
        public float pointSpacing = 0.5f;

        private bool active;
        private bool continuous;
        private bool basis;
        private bool style;


        private bool nDown = true;
        private bool aDown = false;
        private bool bDown = false;
        private bool sDown = false;

        public Curve activeCurve;
        public Curve newContinuousCurve;
        public Curve newBaseCurve;
        public Curve newStyleCurve;

        public Curve currentCurve;
        
      
        
        
        private LineSketchObjectFactory lineSketchObjectFactory;
        private ProjectionPointFinder projectionPointFinder;
        private PointAddingHandler pointAddingHandler;
        private CommandInvoker Invoker;

        private void Start()
        {
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            /*styleLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            baseLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            continuousLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            activeLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            */
            lineSketchObjectFactory = new LineSketchObjectFactory(Defaults);
            pointAddingHandler = new PointAddingHandler();
            Invoker = new CommandInvoker();
            projectionPointFinder = new ProjectionPointFinder(Invoker, this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ToggleA();
                activeCurve = new Curve(LineType.Active, false);
                currentCurve = activeCurve;
                activeLineSketchObject  = NewLine();
                UpdateLineCreation(activeCurve);
            }

            if (aDown == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartNewLine(LineType.Active);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    
                    EndActiveLine(LineType.Active);
                    //pointAddingHandler.GenerateProjectionPoints();

                }
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                ToggleN();
                newContinuousCurve = new Curve(LineType.Continuous, true);
                currentCurve = newContinuousCurve;
                continuousLineSketchObject  = NewLine();
                UpdateLineCreation(newContinuousCurve);
                if (nDown == false)
                {
                    
                    EndActiveLine(LineType.Continuous);
                    //projectionPointFinder.PlacePoints(activeLineSketchObject,  activeCurve.ProjectionPoints, pointAddingHandler.GenerateCurserWorldPosition(), pointSpacing);

                }
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleB();
                newBaseCurve = new Curve(LineType.Base, true);
                currentCurve = newBaseCurve;
                UpdateLineCreation(newBaseCurve);
                baseLineSketchObject = NewLine();
                if (bDown == false)
                {
                    EndActiveLine(LineType.Base);
                    //projectionPointFinder.PlacePoints(activeLineSketchObject,  activeCurve.ProjectionPoints, pointAddingHandler.GenerateCurserWorldPosition(), pointSpacing);

                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ToggleS();
                newStyleCurve = new Curve(LineType.Style, true);
                currentCurve = newStyleCurve;
                UpdateLineCreation(newStyleCurve);
                styleLineSketchObject = NewLine();
                if (sDown == false)
                {
                    EndActiveLine(LineType.Style);
                    //projectionPointFinder.PlacePoints(activeLineSketchObject,  activeCurve.ProjectionPoints, pointAddingHandler.GenerateCurserWorldPosition(), pointSpacing);

                }
            }
        }

        private LineSketchObject NewLine()
        {
            initializedLine = true;
            return lineSketchObjectFactory.CreateLineSketchObject();
        }

        private void UpdateLineCreation(Curve curve)
        {
            if (curve.Type == LineType.Active)
            {
                curve.Continuous = false;
            }
            else
            {
                curve.Continuous = !curve.Continuous;
            }
        }

        private void StartNewLine(LineType currenLineType)
        {
            if (CursorMovementHandler.allowCursorFreeMovement)
            {
                if (currenLineType == LineType.Active)
                {
                    activeLineSketchObject = NewLine();
                }
                else if (currentLineType == LineType.Continuous)
                {
                    continuousLineSketchObject = NewLine();
                }
                else if (currenLineType == LineType.Base)
                {
                    baseLineSketchObject = NewLine();
                }
                else if (currenLineType == LineType.Style)
                {
                    styleLineSketchObject = NewLine();
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

        private void ToggleN()
        {
            nDown = !nDown;
        }
        private void ToggleA()
        {
            aDown = !aDown;
        }
        private void ToggleB()
        {
            bDown = !bDown;
        }
        private void ToggleS()
        {
            sDown = !sDown;
        }
    }
}