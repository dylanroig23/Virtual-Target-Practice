using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

namespace RoigDylan_VukovicCharlie.Lab6
{
    public class FirstPersonShooter : MonoBehaviour
    {
        private InputAction shootActionRef;
        public Camera camera;
        public GameObject projectilePrefab;

        private Vector3 whereProjectileHit;
        public Transform shotFrom;
        public float projectileSpeed = 30f;
        private float timeToShoot;
        public float fireRate = 1f;


        public void Initialize(InputAction shootAction)
        {
            shootAction.Enable();
            shootActionRef = shootAction;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 shoot = shootActionRef.ReadValue<Vector2>();
            if(shoot == new Vector2(0.00f, 1.00f) && Time.time >= timeToShoot){
                timeToShoot = Time.time + 1/fireRate;
                ShootProjectile();
            }
        }

        void ShootProjectile()
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(.5f,.5f,0));
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit)){ // if our ray hits any collider
                whereProjectileHit = hit.point;
                Debug.Log("hit point" + hit.point);
            } else{
                Debug.Log("else");
                whereProjectileHit = ray.GetPoint(1000); // if nothing is hit, get the point 1000 out 
            }

            SpawnProjectile(shotFrom);
        }

        void SpawnProjectile(Transform shotFrom){
            //var projectileInstance = Instantiate(projectilePrefab, shotFrom.position, Quaternion.identity) as GameObject; 
            // projectileInstance.GetComponent<Rigidbody>().velocity = (whereProjectileHit - shotFrom.position).normalized * projectileSpeed;
            var direction = (whereProjectileHit - shotFrom.position).normalized;
            var rotation = Quaternion.LookRotation(direction);
            var projectileInstance = Instantiate(projectilePrefab, shotFrom.position, rotation) as GameObject;
            projectileInstance.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        }
    }
}