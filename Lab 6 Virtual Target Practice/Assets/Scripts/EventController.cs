using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RoigDylan_VukovicCharlie.Lab6
{  
    public class EventController : MonoBehaviour
    {
        public GameOverScreen gameOverScreen;
        public YouWinScreen youWinScreen;
        public void OnEnable(){
            // subscribe
            EventManager.gameLost += OnGameLost;
            EventManager.gameWon += OnGameWon;
        }

        public void OnDisable(){
            EventManager.gameLost -= OnGameLost;
            EventManager.gameWon -= OnGameWon;
        }

        public void OnGameLost(){
            gameOverScreen.Setup();
            // restart the scene
            StartCoroutine(delayEnd());

            //Debug.Log("End game");
        }

        public void OnGameWon(){
            youWinScreen.Setup();
            // restart the scene
            StartCoroutine(delayEnd());

            //Debug.Log("End game");
        }
        IEnumerator delayEnd()
        {
            // wait for 5 seconds
            yield return new WaitForSecondsRealtime(5);
            SceneManager.LoadScene("Virtual Target Practice", LoadSceneMode.Single);
        }
    }
}