using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShadingSimulator.Model;

namespace ShadingSimulator.View
{   
    /// <summary>
    /// Shading specular point controller.
    /// </summary>
    [ExecuteInEditMode]
    public class ShadingPointController : MonoBehaviour 
    {
        [SerializeField]
        float radius = 0.5f;

        [SerializeField]
        private GameObject prefabLineObject;

        [SerializeField]
        private GameObject shadingSurface;

        [SerializeField]
        private GameObject specularOffset;

        private List<GameObject> specularLineObjects = new List<GameObject>();

        public Vector3 Normal
        { 
            get
            {
                return this.transform.localToWorldMatrix.MultiplyVector (new Vector3(0.0f,1.0f,0.0f));
            }
        }


        /// <summary>
        /// Initialize the specified lines.
        /// </summary>
        public void Initialize(IList<ShadingLine> lines)
        {
            this.InitializeSpecular (lines);
        }

        private void InitializeSpecular(IList<ShadingLine> lines)
        {
            Transform parent = specularOffset.transform;
            if (this.specularLineObjects.Count != lines.Count)
            {
                foreach (GameObject lineGo in this.specularLineObjects)
                {
                    Destroy (lineGo);
                }
                this.specularLineObjects.Clear ();
                foreach (ShadingLine line in lines)
                {
                    GameObject go = Instantiate (prefabLineObject) as GameObject;
                    if (go != null)
                    {
                        Transform t = go.transform;
                        t.parent = parent;
                        t.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
                        t.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                        this.specularLineObjects.Add (go);
                    }
                }
            }

            for(int i = 0; i < this.specularLineObjects.Count; i++)
            {
                GameObject go = this.specularLineObjects[i];
                if (go != null)
                {
                    ShadingLineController shadingSpecularLineController = go.GetComponent<ShadingLineController> ();
                    shadingSpecularLineController.Initialize (lines[i]);
                }
            }
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        void Update()
        {
            float diam = this.radius * 2.0f;
            if (shadingSurface != null)
            {
                shadingSurface.transform.localScale = new Vector3 (diam, diam, diam);
            }
            if (specularOffset != null)
            {
                specularOffset.transform.localPosition = new Vector3 (0.0f, radius, 0.0f);
            }
        }

    }

}
