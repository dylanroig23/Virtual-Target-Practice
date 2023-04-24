using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RoigDylan_VukovicCharlie.Lab6
{   
// Written by Charlie Vukovic
/*
 * script is used to update the player's healths
 */
    public class HealthScript : MonoBehaviour
    {
        public Image bar;
        public float health = 100f;

        public void DecreaseHealth(float amount){
            health -= amount;
            // reduce the fill of the health bar to convey a decrease in health
            bar.fillAmount = health / 100f;
            if(health <= 0){
                // check for loss and call appropriate event
                EventManager.OnGameLost();
            }
        }
    }
}