using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoigDylan_VukovicCharlie.Lab6
{  
    public class Rifle : MonoBehaviour
    {
        public bool b;
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
                // activate screen
                b = !b;
                gameObject.SetActive(b);
            }
    }
}