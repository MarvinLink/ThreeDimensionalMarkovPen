using UnityEngine;
using UnityEngine.Serialization;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;

namespace ThreeDimensionalMarkovPen
{
    public class SplineGenerator : MonoBehaviour
    {
        [FormerlySerializedAs("Defaults")] public DefaultReferences defaults;
        private CommandInvoker _invoker;

        [FormerlySerializedAs("InputController")] public InputController inputController;
        [FormerlySerializedAs("SketchWorld")] public SketchWorld sketchWorld;
        [FormerlySerializedAs("LineSketchObject")] public LineSketchObject lineSketchObject;
        public Vector3 position;
    
        void Update()
        {
            if (inputController != null)
            {
                // Check for input to add control points
                if (Input.GetMouseButtonDown(0))
                {
                    if (Camera.main != null)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            position = hit.point;
                        }
                    }
                }
            }
        }
        void Start()
        {
            // Erstelle ein LineSketchObject für die Spline-Kurve
            sketchWorld = Instantiate(defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
        
            // Füge Kontrollpunkte zur Spline-Kurve hinzu
            AddControlPointsToSpline();

            // Füge die Spline-Kurve zur SketchWorld hinzu
            lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            _invoker = new CommandInvoker();
            _invoker.ExecuteCommand(new AddControlPointCommand(this.lineSketchObject, position));
        }

        void AddControlPointsToSpline()
        {
            // Füge hier deine Kontrollpunkte zur Spline-Kurve hinzu
            // Beispiel: SplineObject.AddControlPoint(new Vector3(0, 0, 0));
            // ...
        }
    }
}
