using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoigDylan_VukovicCharlie.Lab6
{  
// Written by Charlie Vukovic
/*
 * class for handling the behavior of making rifle disappear and reappear 
 *  when the weapon switch button is pressed
 */
    public class Rifle : MonoBehaviour
    {
        public bool b = true;
        private float timeToSwitch;
        // Start is called before the first frame update
        void Start()
        {
            b = true;
        }
        public void Toggle(){
            b = !b;
            gameObject.SetActive(b);
        }
    }
}