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
            FirstPersonShooter fps = playerObject.GetComponent<FirstPersonShooter>();
            HealthScript hs = healthObject.GetComponent<HealthScript>();

            if(collision.gameObject.tag != "Projectile"){
                Destroy(gameObject);
                if(collision.gameObject.tag == "Target"){
                    if (fps != null && collision.gameObject != null && hit) // hit bool ensures multiple collisions on a single target dont happen
                    {
                        fps.numOfTargetsHit ++;
                        hit = false;

                    }
                    Destroy(collision.gameObject);
                    //targetHitSound.Play();
                    
                } else if(collision.gameObject.tag == "Player" && !shotFromPlayer){
                hs.DecreaseHealth(10);
                }
            }
        }
    }
}