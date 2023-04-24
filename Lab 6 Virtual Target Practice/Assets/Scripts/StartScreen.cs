using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoigDylan_VukovicCharlie.Lab6
{  
// Written by Charlie Vukovic
/*
 * Script used to deactivate the start screen
 */
    public class StartScreen : MonoBehaviour
    {
        public void Setup(){
            // activate screen
            gameObject.SetActive(false);
        }
    }
}