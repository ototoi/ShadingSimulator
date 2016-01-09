using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShadingSimulator.Model;

namespace ShadingSimulator.View
{
	/// <summary>
	/// Specular line controller.
	/// </summary>
	public class ShadingLineController : MonoBehaviour 
	{
        [SerializeField]
        private LineRenderer lineRenderer;

        public void Initialize(ShadingLine line)
        {
            List<Vector3> positions = new List<Vector3>();
            foreach(ShadingLinePoint p in line.Points)
            {
                positions.Add (p.Position);
            }
            lineRenderer.SetPositions ( positions.ToArray() );
            if (line.Points.Count >= 2)
            {
                Color c0 = line.Points[0].Color;
                Color c1 = line.Points[line.Points.Count-1].Color;
                lineRenderer.SetColors (c0, c1);
            }
        }
		
	}
}
