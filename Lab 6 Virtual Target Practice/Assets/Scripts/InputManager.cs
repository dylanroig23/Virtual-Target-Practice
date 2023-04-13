using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;

namespace RoigDylan_VukovicCharlie.Lab6
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MovementControl movementController;
        private PlayerInputActions inputScheme;

        private void Awake()
        {
            inputScheme = new PlayerInputActions();
            movementController.Initialize(inputScheme.Player.Movement, inputScheme.Player.DeltaMouse);

        }

        private void OnEnable()
        {
            /*
             * Here is where we would put code that involves the firing 
             * aspect. 
             */
        }
    }
}
