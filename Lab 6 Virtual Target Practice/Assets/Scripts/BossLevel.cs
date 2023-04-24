using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RoigDylan_VukovicCharlie.Lab6{
// Written by both Charlie Vukovic and Dylan Roig
// specific blocks of code are commented to indicate who did what
/*
 * Handles all the boss behavior including movement and shooting
 */
    public class BossLevel : MonoBehaviour
    {
        public int bossHealth = 100;
        //public float movementSpeed = .5f;
        public float fireRate = .5f;
        public float projectileSpeed = 30f;
        private float timeToShoot;
        private Vector3 whereProjectileHit;
        public GameObject projectilePrefab;
        public Transform player;
        public Transform shotFrom;

        private Rigidbody bossBody;

        void Start()
        {
            // written by Charlie
            player = GameObject.FindGameObjectWithTag("Player").transform; // find the player 

            // written by Dylan
            bossBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            // written by Charlie
            // make sure the boss can't shoot too quickly
            if(Time.time >= timeToShoot){
                timeToShoot = Time.time + 1/fireRate;
                ShootProjectile();
            }


            // written by Dylan
            Vector3 vectorBetween = player.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorBetween, 5 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            bossBody.AddForce(transform.forward * 500);                
            
        }

        // written by Charlie
        void ShootProjectile()
        {
            // get the point at the tip of the boss' gun to spawn the bullets from that point
            GameObject enemyShootPoint = GameObject.FindWithTag("EnemyShootPoint");
            shotFrom = enemyShootPoint.transform;
            // cast a ray to the player
            Ray ray = new Ray(shotFrom.position, player.position - shotFrom.position);
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit))
            { // if our ray hits any collider
                whereProjectileHit = hit.point;
            }

            SpawnProjectile(shotFrom);
        }

        // written by Charlie
        void SpawnProjectile(Transform shotFrom){
            // spawn the projectile prefab with the direction from the tip of the gun to hit point
            var direction = (whereProjectileHit - shotFrom.position).normalized; // normalize for constant speed
            var rotation = Quaternion.LookRotation(direction); // set rotation so bullet is pointing forward
            var projectileInstance = Instantiate(projectilePrefab, shotFrom.position, rotation) as GameObject;
            projectileInstance.GetComponent<Rigidbody>().velocity = direction * projectileSpeed; // set the speed of the bullet
        }


        // written by Charlie
        private void OnCollisionEnter(Collision collision)
        {
            Collider collider = collision.collider;
            // if the boss is hit by a projectile 
            if (collider.tag == "Projectile")
            {
                // decrease the boss health 
                bossHealth -= 10;
                if (bossHealth <= 0) // check if it should be dead
                {
                    // destroy and fire game won event
                    Destroy(gameObject);
                    EventManager.OnGameWon();
                }
            }
        }
    }
}