using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace ShadingSimulator.View
{

    public class ShadingUIController : MonoBehaviour 
    {
        [SerializeField]
        Slider distroSlider;

        [SerializeField]
        Slider phongPowerSlider;

        public int Distro 
        { 
            get
            {
                return (int)distroSlider.value;
            }
        }

        public float PhongPower 
        { 
            get
            {
                return phongPowerSlider.value;
            }
        }

        public Action OnSimulateCallback = delegate {};


        public void OnClickSimulate()
        {
            OnSimulateCallback ();
        }
    }

}
