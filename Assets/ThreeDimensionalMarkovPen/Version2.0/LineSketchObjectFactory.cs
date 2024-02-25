using System;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.SketchObjectManagement;

namespace ThreeDimensionalMarkovPen.Curves.New
{
    public class LineSketchObjectFactory : MonoBehaviour
    {
        private DefaultReferences defaults;
        private Curve curve;

        public LineSketchObjectFactory(DefaultReferences defaults)
        {
            this.defaults = defaults;
        }

        public LineSketchObject CreateLineSketchObject()
        {
            return Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        }
    }
}