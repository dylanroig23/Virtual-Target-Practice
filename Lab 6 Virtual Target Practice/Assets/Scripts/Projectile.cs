using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
namespace RoigDylan_VukovicCharlie.Lab6
{   
    public class Projectile : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        } 

        // Update is called once per frame
        void Update()
        {
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player"){
                Destroy(gameObject);
            }
        }
    }
}