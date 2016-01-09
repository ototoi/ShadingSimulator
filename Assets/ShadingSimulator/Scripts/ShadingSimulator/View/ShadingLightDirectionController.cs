using UnityEngine;
using System.Collections;
using ShadingSimulator.Model;

namespace ShadingSimulator.View
{
    [ExecuteInEditMode]
    public class ShadingLightDirectionController : MonoBehaviour 
    {
        [SerializeField]
        Color color;

        [SerializeField]
        GameObject toObject;

        [SerializeField]
        GameObject fromObject;

        [SerializeField]
        GameObject directionObject;

        public ShadingLine LightLine { get; private set; }

        void Start()
        {
            this.LightLine = GetShadingLine ();
        }

        void Update()
        {
            Light l = fromObject.GetComponent<Light> ();
            if (l != null)
            {
                this.color = l.color;
            }

            Matrix4x4 lineMat = directionObject.transform.worldToLocalMatrix;
            this.LightLine = GetShadingLine();

            Vector3 fromPoint = lineMat.MultiplyVector (LightLine.Points[0].Position);
            Vector3 toPoint = lineMat.MultiplyVector (LightLine.Points[1].Position);
            if (this.directionObject != null)
            {
                ShadingLine line = new ShadingLine();
                line.Add( new ShadingLinePoint(fromPoint, color) );
                line.Add( new ShadingLinePoint(toPoint, color) );
                ShadingLineController shadingLineController = directionObject.GetComponent<ShadingLineController>();
                shadingLineController.Initialize (line);
            }
        }

        private ShadingLine GetShadingLine()
        {
            Vector3 fromPoint = fromObject.transform.localToWorldMatrix.MultiplyPoint(new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 toPoint   = toObject.transform.localToWorldMatrix.MultiplyPoint(new Vector3(0.0f, 0.0f, 0.0f));
            ShadingLine line = new ShadingLine ();
            line.Add (new ShadingLinePoint (fromPoint, this.color));
            line.Add (new ShadingLinePoint (toPoint, this.color));
            return line;
        }
    }
}