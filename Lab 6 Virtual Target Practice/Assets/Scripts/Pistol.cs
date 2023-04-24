using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoigDylan_VukovicCharlie.Lab6
{  
// Written by Charlie Vukovic
/*
 * class for handling the behavior of making pistol disappear and reappear 
 *  when the weapon switch button is pressed
 */
    public class Pistol : MonoBehaviour
    {
        public bool b;
        private float timeToSwitch;
        public void Toggle(){
            // switch the boolean and set the active status to that 
            b = !b;
            gameObject.SetActive(b);
        }
    }
}