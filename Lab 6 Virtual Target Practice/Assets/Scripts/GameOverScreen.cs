using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RoigDylan_VukovicCharlie.Lab6
{  
// Written by Charlie Vukovic
/*
 *  Script used by the event system to activate the loss screen
 */
    public class GameOverScreen : MonoBehaviour
    {
        public void Setup(){
            // activate screen
            gameObject.SetActive(true);
        }
    }
}