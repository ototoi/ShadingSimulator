using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShadingSimulator.Model;


namespace ShadingSimulator.View
{
	/// <summary>
	/// Shading view controller.
	/// </summary>
    [ExecuteInEditMode]
	public class ShadingViewController : MonoBehaviour 
	{
		[SerializeField]
        private ShadingPointController shadingPointController;

        [SerializeField]
        private ShadingLightDirectionController lightDirectionController;

        [SerializeField]
        private ShadingUIController shadingUIController;

        void Start()
        {
            if (this.shadingUIController != null)
            {
                shadingUIController.OnSimulateCallback = () =>
                {
                    this.Simulate ();
                };
            }
        }

        public void Initialize()
        {

        }

        void Update()
        {
            if ( Application.isPlaying )
            {
                Simulate ();
            }
        }

        /// <summary>
        /// Simulate this instance.
        /// </summary>
        private void Simulate()
        {
            ShadingLine lightLine = lightDirectionController.LightLine;
            Vector3 lightDirection = (lightLine.Points [1].Position - lightLine.Points [0].Position).normalized;
            Vector3 normal = shadingPointController.Normal;
            int nDistro = shadingUIController.Distro;
            List<ShadingLine> shadingLines = GetSpecularShadingLine (lightDirection, normal, nDistro);
            shadingPointController.Initialize (shadingLines);
        }

        private List<ShadingLine> GetSpecularShadingLine(Vector3 lightDirection, Vector3 normal, int nDistro)
        {
            
            Vector3 lightU = Vector3.Dot (-lightDirection, normal) * normal;
            Vector3 lightH = -lightDirection - lightU;
            Vector3 reflect = (-lightDirection - 2.0f * lightH).normalized;

            List<ShadingLine> shadingLines = new List<ShadingLine> ();
            {
                ShadingLine line = new ShadingLine ();
                line.Add( new ShadingLinePoint( new Vector3(), Color.blue ));
                line.Add( new ShadingLinePoint( reflect, Color.blue ));
                shadingLines.Add (line);
            }

            if (Vector3.Dot (lightDirection, normal) > 0)
            {
                return shadingLines;
            }

            float PhongPower = shadingUIController.PhongPower;
            float PhongIntensity = 1.0f;
            while(shadingLines.Count < nDistro+1)
            {
                Vector3 v = GetDistVector ();//-view direction
                if (Vector3.Dot (v, reflect) < 0.0f)
                {
                    v = -v;
                }
                if (Vector3.Dot (v, normal) > 0)
                {
                    float p = Mathf.Pow(Mathf.Abs(Vector3.Dot (reflect, v)), PhongPower) * PhongIntensity;
                    if (p > 0.001)
                    {
                        ShadingLine line = new ShadingLine ();
                        line.Add (new ShadingLinePoint (new Vector3 (), Color.red));
                        line.Add (new ShadingLinePoint (p * v, Color.red));
                        shadingLines.Add (line);
                    }
                }
            }
            return shadingLines;
        }

        private Vector3 GetDistVector()
        {
            float x,y,z,l = 1.0f;
            do
            {
                x = 2.0f * Random.value - 1.0f;
                y = 2.0f * Random.value - 1.0f;
                z = 2.0f * Random.value - 1.0f;
                l = x * x + y * y + z * z;
            }
            while(l > 1.0);
            return (new Vector3 (x, y, z).normalized);
        }
	}
}
