using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RoigDylan_VukovicCharlie.Lab6
{  
// Written by Charlie Vukovic
/*
 * Script used to handle events
 */
    public class EventController : MonoBehaviour
    {
        public GameOverScreen gameOverScreen;
        public YouWinScreen youWinScreen;
        public Rifle rifle;
        public Pistol pistol;
        public void OnEnable(){
            // subscribe events
            EventManager.gameLost += OnGameLost;
            EventManager.gameWon += OnGameWon;
            EventManager.weaponSwitch += OnWeaponSwitch;
        }

        public void OnDisable(){
            // unsubscribe 
            EventManager.gameLost -= OnGameLost;
            EventManager.gameWon -= OnGameWon;
            EventManager.weaponSwitch -= OnWeaponSwitch;
        }

        public void OnGameLost(){
            gameOverScreen.Setup();
            // restart the scene
            StartCoroutine(delayEnd());
        }

        public void OnGameWon(){
            youWinScreen.Setup();
            TimerScript.end = true;
            // restart the scene
            StartCoroutine(delayEnd());
        }

        public void OnWeaponSwitch(){
            // toggle them both
            // since one starts as enabled and one as disabled, toggling 
            // them both every time will flip which one is enabled and 
            // which is disabled
            rifle.Toggle();
            pistol.Toggle();
        }


        IEnumerator delayEnd()
        {
            // wait for 5 seconds
            yield return new WaitForSecondsRealtime(5);
            // reload the entire scene, effectively restarting the level
            SceneManager.LoadScene("Virtual Target Practice", LoadSceneMode.Single);
        }
    }
}