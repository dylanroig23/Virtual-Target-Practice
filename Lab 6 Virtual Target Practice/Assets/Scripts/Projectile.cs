using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
namespace RoigDylan_VukovicCharlie.Lab6
{   
    /*
        current issues:
        - bullet rotates when it is shot
        - if player is walking forward and shoots they bump into the bullet and mess it up
        - bullets don't shoot quite right when you look at walls / are close to wall?
           - not really sure what's causing this, will need to test more
    */

    public class Projectile : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        } 

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player"){
                Destroy(gameObject);
            }
        }
    }
}