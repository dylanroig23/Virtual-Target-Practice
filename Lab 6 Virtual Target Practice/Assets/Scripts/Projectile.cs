using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
namespace RoigDylan_VukovicCharlie.Lab6
{   
// Written by Charlie Vukovic
/*
 * script defines the behavior of the projectile and deals with what to do on collisions
 */
    public class Projectile : MonoBehaviour
    {
        public GameObject playerObject;
        public GameObject healthObject;
        public bool hit = true;
        public bool shotFromPlayer = false;
        //[SerializeField] private AudioSource targetHitSound; 

        // Start is called before the first frame update
        void Start()
        {
            playerObject = GameObject.Find("Player");
            healthObject = GameObject.Find("HealthScript");
        } 

        // Update is called once per frame
        void Update()
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            // fps will be used to update num of targets hit
            FirstPersonShooter fps = playerObject.GetComponent<FirstPersonShooter>();
            // hs will be used to update player health
            HealthScript hs = healthObject.GetComponent<HealthScript>();

            // make sure the bullets can't hit themselves 
            if(collision.gameObject.tag != "Projectile"){
                // if it hits anything but itself, we want to destroy the bullet
                Destroy(gameObject);
                if(collision.gameObject.tag == "Target"){ 
                    if (fps != null && collision.gameObject != null && hit) // hit bool ensures multiple collisions on a single target don't happen
                    {
                        fps.numOfTargetsHit++;
                        hit = false;
                    }
                    // destroy the target if it is hit
                    Destroy(collision.gameObject);
                    //targetHitSound.Play();
                    
                } else if(collision.gameObject.tag == "Player" && !shotFromPlayer){
                    // if a player is hit by a bullet that is not their own then decrease their health
                    hs.DecreaseHealth(10);
                }
            }
        }
    }
}