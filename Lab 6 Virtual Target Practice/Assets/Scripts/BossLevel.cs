using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RoigDylan_VukovicCharlie.Lab6{
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

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // find the player 
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time >= timeToShoot){
                timeToShoot = Time.time + 1/fireRate;
                ShootProjectile();
            }
        }

        void ShootProjectile()
        {
            GameObject enemyShootPoint = GameObject.FindWithTag("EnemyShootPoint");
            shotFrom = enemyShootPoint.transform;
            Ray ray = new Ray(shotFrom.position, player.position - shotFrom.position);
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit))
            { // if our ray hits any collider
                whereProjectileHit = hit.point;
            }

            SpawnProjectile(shotFrom);
        }

        void SpawnProjectile(Transform shotFrom){
            var direction = (whereProjectileHit - shotFrom.position).normalized;
            var rotation = Quaternion.LookRotation(direction);
            var projectileInstance = Instantiate(projectilePrefab, shotFrom.position, rotation) as GameObject;
            projectileInstance.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        }



        private void OnCollisionEnter(Collision collision)
        {
            bossHealth -= 10;
            if(bossHealth <= 0){
                Destroy(gameObject);
            }
        }
    }
}