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
// Written by Charlie Vukovic and Dylan Roig
/*
 * Script handles the aspects of the game related to shooting
 * including the spawning of bullets, reloading, displaying
 * weapon information, and weapon switching
 */
    public class FirstPersonShooter : MonoBehaviour
    {
        private InputAction shootActionRef;
        private InputAction reloadActionRef;
        private InputAction switchActionRef;
        public Camera camera;
        public GameObject projectilePrefab;

        private Vector3 whereProjectileHit;
        public Transform shotFrom;
        public float projectileSpeed = 30f;
        private float timeToShoot;
        private float timeToSwitch;
        public float fireRate = 1f;
        public int numOfTargetsHit;
        public int rifleAmmo;
        public int pistolAmmo;
        public bool isRifle = true;
        public Text score1;
        public Text score2;
        public Text score3;
        public Text ammoText;

        [SerializeField] private AudioSource shootingSound;
        [SerializeField] private AudioSource reloadSound;

        // written by Charlie
        public void Initialize(InputAction shootAction, InputAction reloadAction, InputAction switchAction)
        {
            shootAction.Enable();
            shootActionRef = shootAction;
            reloadAction.Enable();
            reloadActionRef = reloadAction;
            switchAction.Enable();
            switchActionRef = switchAction;
        }

        // written by Charlie
        void Start()
        {
            // Set initial ammo values
            rifleAmmo = 10;
            pistolAmmo = 5;
            ammoText.text = rifleAmmo.ToString();
        }

        // written by Charlie
        void Update()
        {
            Vector2 shoot = shootActionRef.ReadValue<Vector2>();
            Vector2 reload = reloadActionRef.ReadValue<Vector2>();
            float switchWeapon = switchActionRef.ReadValue<float>();
            // make sure the player can't shoot too fast
            if(shoot == new Vector2(0.00f, 1.00f) && Time.time >= timeToShoot){
                timeToShoot = Time.time + 1/fireRate;
                // make sure the weapon has ammo before projectile is spawned
                if(isRifle && rifleAmmo > 0){
                    ShootProjectile();
                }else if(pistolAmmo > 0) {
                    ShootProjectile();
                }
            }

            // reload if the player presses the reload button 
            if(reload == new Vector2(0.00f, 1.00f)){
                StartCoroutine(Reload());
            }

            // make sure player can't switch weapons too quickly
            if(switchWeapon == 1 && Time.time >= timeToSwitch){
                timeToSwitch = Time.time + 1.3f;
                isRifle = !isRifle; // switch the bool that specifies weapon
                EventManager.OnWeaponSwitch(); 
                // display the new correct ammo amount
                if(isRifle){
                    ammoText.text = rifleAmmo.ToString();
                }else {
                    ammoText.text = pistolAmmo.ToString();
                }
            }
            // update score
            var score = numOfTargetsHit.ToString();
            score1.text = score;
            score2.text = score;
            score3.text = score;
            
        }

        // written by Charlie
        void ShootProjectile()
        {
            // ammo stuff
            AmmunitionUpdate();

            // ray stuff
            Ray ray = camera.ViewportPointToRay(new Vector3(.5f,.5f,0));
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit)){ // if our ray hits any collider
                whereProjectileHit = hit.point;
            } else{
                whereProjectileHit = ray.GetPoint(1000); // if nothing is hit, get the point 1000 out so bullet can be shot out into space
            }
            SpawnProjectile(shotFrom);
        }

        void SpawnProjectile(Transform shotFrom){
            // written by Charlie
            // spawn the projectile prefab with the direction from the tip of the gun to hit point
            var direction = (whereProjectileHit - shotFrom.position).normalized; // normalize for constant speed
            var rotation = Quaternion.LookRotation(direction); // set rotation so bullet is pointing forward
            var projectileInstance = Instantiate(projectilePrefab, shotFrom.position, rotation) as GameObject;
            projectileInstance.GetComponent<Projectile>().shotFromPlayer = true; // mark this instance as shot from player for collision stuff later
            projectileInstance.GetComponent<Rigidbody>().velocity = direction * projectileSpeed; // set the speed of the bullet

            // written by Dylan
            shootingSound.Play();
        }

        // written by Charlie
        void AmmunitionUpdate(){
            // decrease and dispaly the appropriate ammo 
            if(isRifle){
                rifleAmmo--;
                ammoText.text = rifleAmmo.ToString();
            }else {
                pistolAmmo--;
                ammoText.text = pistolAmmo.ToString();
            }
            
            if(rifleAmmo == 0 || pistolAmmo == 0){
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            // written by Charlie
            ammoText.text = "--"; // reloading
            yield return new WaitForSeconds(1.5f); // wait for 1.5 seconds
            
            //reload the appropriate ammo
            if(isRifle){
                rifleAmmo = 10;
                ammoText.text = rifleAmmo.ToString();
            }else {
                pistolAmmo = 5;
                ammoText.text = pistolAmmo.ToString();
            }

            // Written by Dylan
            reloadSound.Play();
        }
    }
}