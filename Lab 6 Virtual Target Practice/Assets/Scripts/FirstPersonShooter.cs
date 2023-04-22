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


        public void Initialize(InputAction shootAction, InputAction reloadAction, InputAction switchAction)
        {
            shootAction.Enable();
            shootActionRef = shootAction;
            reloadAction.Enable();
            reloadActionRef = reloadAction;
            switchAction.Enable();
            switchActionRef = switchAction;
        }
        // Start is called before the first frame update
        void Start()
        {
            rifleAmmo = 10;
            pistolAmmo = 5;
            ammoText.text = rifleAmmo.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 shoot = shootActionRef.ReadValue<Vector2>();
            Vector2 reload = reloadActionRef.ReadValue<Vector2>();
            float switchWeapon = switchActionRef.ReadValue<float>();
            if(shoot == new Vector2(0.00f, 1.00f) && Time.time >= timeToShoot){
                timeToShoot = Time.time + 1/fireRate;
                if(isRifle){
                    if(rifleAmmo > 0){
                        ShootProjectile();
                    }
                }else {
                    if(pistolAmmo > 0){
                        ShootProjectile();
                    }
                }
            }

            if(reload == new Vector2(0.00f, 1.00f)){
                StartCoroutine(Reload());
            }

            if(switchWeapon == 1 && Time.time >= timeToSwitch){
                timeToSwitch = Time.time + 1.3f;
                isRifle = !isRifle;
                EventManager.OnWeaponSwitch();
                if(isRifle){
                    ammoText.text = rifleAmmo.ToString();
                }else {
                    ammoText.text = pistolAmmo.ToString();
                }
            }
            var score = numOfTargetsHit.ToString();
            score1.text = score;
            score2.text = score;
            score3.text = score;
            
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
            shootingSound.Play();
            var projectileInstance = Instantiate(projectilePrefab, shotFrom.position, rotation) as GameObject;
            projectileInstance.GetComponent<Projectile>().shotFromPlayer = true;
            projectileInstance.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

        }

        void AmmunitionUpdate(){
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
            ammoText.text = "--"; // reloading
            yield return new WaitForSeconds(1.5f); // wait for 1.5 seconds
            reloadSound.Play();
            if(isRifle){
                rifleAmmo = 10;
                ammoText.text = rifleAmmo.ToString();
            }else {
                pistolAmmo = 5;
                ammoText.text = pistolAmmo.ToString();
            }
            
        }
    }
}