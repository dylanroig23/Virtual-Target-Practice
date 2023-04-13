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
        [SerializeField] private Camera cameraFollower;

        private InputAction moveActionRef;
        private InputAction deltaMouseActionRef;
        private float cameraSpeed = 1000f;
        private float playerSpeed = 5f;
        Vector2 mouseRotation; //the mouse movement that corresponds to the rotation

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Initialize(InputAction moveAction, InputAction deltaMouseAction)
        {
            moveAction.Enable();
            deltaMouseAction.Enable();
            moveActionRef = moveAction;
            deltaMouseActionRef = deltaMouseAction;
        }

        private void Update()
        {
            /*
             * Handle Rotating the Camera in Mouse Direction Movement
             */
            this.mouseRotation += deltaMouseActionRef.ReadValue<Vector2>();
            this.mouseRotation.y = Mathf.Clamp(this.mouseRotation.y, -90, 90);

            Quaternion toRotation = Quaternion.Euler(-this.mouseRotation.y, this.mouseRotation.x, 0); //create a Quaternion from the updated rotation
            var cameraStep = cameraSpeed * Time.deltaTime;
            cameraFollower.transform.rotation = Quaternion.RotateTowards(cameraFollower.transform.rotation, toRotation, cameraStep); //rotate the camera


            /*
             * Handle Moving in Direction of the Camera
             */
            Vector3 cameraDirectionForward = cameraFollower.transform.forward; //get camera forward vector
            Vector3 cameraDirectionRight = cameraFollower.transform.right; //get camera right vector

            Vector2 movementVector = moveActionRef.ReadValue<Vector2>();
            float leftRightComponent = movementVector.x;
            float forwardBackCompenent = movementVector.y;

            Vector3 movement = cameraDirectionForward * forwardBackCompenent + cameraDirectionRight * leftRightComponent;
            movement += new Vector3(0, -movement.y, 0); //prevent the character from flying

            var objectStep = playerSpeed * Time.deltaTime;
            Vector3 currentPosition = playerToMove.transform.position;
            playerToMove.transform.position = Vector3.MoveTowards(currentPosition, currentPosition + movement, objectStep); 
        }
    }
}
