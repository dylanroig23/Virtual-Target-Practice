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
        public bool hit = true;

        // Start is called before the first frame update
        void Start()
        {
            playerObject = GameObject.Find("Player");
        } 

        // Update is called once per frame
        void Update()
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            FirstPersonShooter fps = playerObject.GetComponent<FirstPersonShooter>();

            if(collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player"){
                Destroy(gameObject);
                if(collision.gameObject.tag == "Target"){
                    if (fps != null && collision.gameObject != null && hit) // hit bool ensures multiple collisions on a single target dont happen
                    {
                        fps.numOfTargetsHit ++;
                        hit = false;

                    }
                    Destroy(collision.gameObject);
                    
                }
            }
        }
    }
}