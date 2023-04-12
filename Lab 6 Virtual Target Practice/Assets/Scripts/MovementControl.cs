using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

namespace RoigDylan_VukovicCharlie.Lab6
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private GameObject playerToMove;
        [SerializeField] private float movementSpeed = 5f;
        private InputAction moveActionRef;

        public void Initialize(InputAction moveAction)
        {
            moveActionRef = moveAction;
            moveAction.Enable();
        }

        private void Update()
        {
            //get the 2D vector components
            Vector2 movementVector = moveActionRef.ReadValue<Vector2>();
            float leftRightComponent = movementVector.x;
            float forwardBackCompenent = movementVector.y;
            
            Vector3 movement = Vector3.forward * forwardBackCompenent + Vector3.right *leftRightComponent;
            
            transform.position += movement * movementSpeed * Time.deltaTime;
        }
    }
}
