using System;
using System.Collections.Generic;
using ThreeDimensionalMarkovPen.Version2._0;
using Unity.VisualScripting;
using UnityEngine;

namespace ThreeDimensionalMarkovPen
{
    public class Curve
    {
        public LineType Type { get;  set; }
        public bool Continuous { get; set; }
        
        public List<Vector3> PointsOnTheLine { get; set; }
        public List<Vector3> ProjectionPoints { get; set; }
        
        public Curve(LineType type, bool continuous)
        {
            Type = type;
            Continuous = continuous;
            PointsOnTheLine = new List<Vector3>();
            ProjectionPoints = new List<Vector3>();
        }
    }
}