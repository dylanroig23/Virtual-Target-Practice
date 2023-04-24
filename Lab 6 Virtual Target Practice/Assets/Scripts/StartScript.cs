using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace RoigDylan_VukovicCharlie.Lab6
{
// Written by Charlie Vukovic
/*
 * script is used to detect when the player presses W which indicates they want 
 * to start the game and deals with the destruction of the start screen
 */
    public class StartScript : MonoBehaviour
    {
        private InputAction moveActionRef;
        public StartScreen startScreen;

        public void Initialize(InputAction moveAction)
        {
            moveAction.Enable();
            moveActionRef = moveAction;
        }

        // Update is called once per frame
        void Update()
        {
            if (moveActionRef.ReadValue<Vector2>() == new Vector2(0.00f, 1.00f))
            {
                startScreen.Setup();
                Destroy(gameObject);
            }
            
        }
    }
}