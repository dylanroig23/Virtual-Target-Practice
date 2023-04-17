using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RoigDylan_VukovicCharlie.Lab6
{   
    public class HealthScript : MonoBehaviour
    {
        public Image bar;
        public float health = 100f;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void DecreaseHealth(float amount){
            health -= amount;
            bar.fillAmount = health / 100f;
            if(health <= 0){
                // game over here
            }
        }
    }
}