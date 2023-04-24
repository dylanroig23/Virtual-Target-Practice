using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;

namespace RoigDylan_VukovicCharlie.Lab6
{
// Written by Charlie Vukovic and Dylan Roig
// specific blocks of code are commented to indicate who did what
/*
 * initializes and passes the correct information to scripts that require inputAction maps
 */
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MovementControl movementController;
        [SerializeField] private FirstPersonShooter firstPersonShooter;
        [SerializeField] private StartScript startScript;
        private PlayerInputActions inputScheme;

        private void Awake()
        {
            // Written by Dylan
            inputScheme = new PlayerInputActions();
            movementController.Initialize(inputScheme.Player.Movement, inputScheme.Player.DeltaMouse, inputScheme.Player.Jump,inputScheme.Player.Sprint);

            // written by Charlie
            firstPersonShooter.Initialize(inputScheme.Player.FPS, inputScheme.Player.Reload,inputScheme.Player.Switch);
            startScript.Initialize(inputScheme.Player.Movement);
        }

        private void OnEnable()
        {

        }
    }
}
