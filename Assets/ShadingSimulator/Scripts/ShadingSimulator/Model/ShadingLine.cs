using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ShadingSimulator.Model
{
    /// <summary>
    /// Shading specular point.
    /// </summary>
    public class ShadingLinePoint
    {
        public Vector3 Position { get; private set; }
        public Color   Color    { get; private set; }
        public float   Strength { get; private set; }

        public ShadingLinePoint(Vector3 position, Color color)
        {
            this.Position = position;
            this.Color = color;
            this.Strength = 1.0f;
        }
    }
    
    /// <summary>
    /// Shading specular line model.
    /// </summary>
    public class ShadingLine 
    {
        private List<ShadingLinePoint> points = new List<ShadingLinePoint>();
        public IList<ShadingLinePoint> Points 
        { 
            get
            {
                return this.points;
            }
        }

        public ShadingLine()
        {
            this.points = new List<ShadingLinePoint> ();
        }

        public ShadingLine(IList<ShadingLinePoint> rhs)
        {
            this.points = new List<ShadingLinePoint> ();
            foreach(ShadingLinePoint p in rhs)
            {
                this.points.Add (p);
            }
        }

        public void Add(ShadingLinePoint p)
        {
            this.points.Add (p);
        }
    }

}