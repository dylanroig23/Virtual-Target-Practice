using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace RoigDylan_VukovicCharlie.Lab6
{
    public class StartScript : MonoBehaviour
    {
        private InputAction moveActionRef;
        public StartScreen startScreen;
        //public GameOverScreen gameOverScreen;
        // Start is called before the first frame update
        void Start()
        {
            
        }

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