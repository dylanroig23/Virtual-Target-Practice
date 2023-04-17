using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace RoigDylan_VukovicCharlie.Lab6
{
    public class FirstPersonShooter : MonoBehaviour
    {
        private InputAction shootActionRef;
        private InputAction reloadActionRef;
        public Camera camera;
        public GameObject projectilePrefab;

        private Vector3 whereProjectileHit;
        public Transform shotFrom;
        public float projectileSpeed = 30f;
        private float timeToShoot;
        public float fireRate = 1f;
        public int numOfTargetsHit;
        public int ammo;
        public Text score;
        public Text ammoText;



        /* 
         things to add:
            - weapon switching?
            - more levels?
            - moving targets?
            - jump functionality
        */

        public void Initialize(InputAction shootAction, InputAction reloadAction)
        {
            shootAction.Enable();
            shootActionRef = shootAction;
            reloadAction.Enable();
            reloadActionRef = reloadAction;
        }
        // Start is called before the first frame update
        void Start()
        {
            ammo = 5;
            ammoText.text = ammo.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 shoot = shootActionRef.ReadValue<Vector2>();
            Vector2 reload = reloadActionRef.ReadValue<Vector2>();
            if(shoot == new Vector2(0.00f, 1.00f) && Time.time >= timeToShoot){
                timeToShoot = Time.time + 1/fireRate;
                if(ammo > 0){
                    ShootProjectile();
                }
            }

            if(reload == new Vector2(0.00f, 1.00f)){
                StartCoroutine(Reload());
            }

            score.text = numOfTargetsHit.ToString();
        }

        void ShootProjectile()
        {
            // ammo stuff
            AmmunitionUpdate();

            // ray stuff
            Ray ray = camera.ViewportPointToRay(new Vector3(.5f,.5f,0));
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit)){ // if our ray hits any collider
                whereProjectileHit = hit.point;
                //Debug.Log("hit point" + hit.point);
            } else{
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

        void AmmunitionUpdate(){
            ammo--;
            ammoText.text = ammo.ToString();
            if(ammo == 0){
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            ammoText.text = "--"; // reloading
            yield return new WaitForSeconds(1.5f); // wait for 1.5 seconds
            ammo = 5; // reset ammo
            ammoText.text = ammo.ToString();
        }
    }
}