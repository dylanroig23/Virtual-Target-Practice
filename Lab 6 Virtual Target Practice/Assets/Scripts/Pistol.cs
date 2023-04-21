using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoigDylan_VukovicCharlie.Lab6
{  
    public class Pistol : MonoBehaviour
    {
        public bool b;
        private float timeToSwitch;
        // Start is called before the first frame update
            void Start()
            {
                b = true;
            }

            // Update is called once per frame
            void Update()
            {
                
            }

            public void Toggle(){
                //if(Time.time >= timeToSwitch){
               //     timeToSwitch = Time.time + 1.3f;
                    b = !b;
                    gameObject.SetActive(b);
               // }
            }
    }
}