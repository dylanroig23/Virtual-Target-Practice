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
        [SerializeField] private GameObject bossPrefab;


        private InputAction moveActionRef;
        private InputAction deltaMouseActionRef;
        private InputAction jumpRef;
        private InputAction sprintRef;
        private float cameraSpeed = 1000f;
        private float playerSpeed = 5f;
        private float sprintMultiplier = 1.5f;
        Vector2 mouseRotation; //the mouse movement that corresponds to the rotation
        private Rigidbody characterRigidBody;
        private MeshRenderer bossWallMeshRender;

        [SerializeField] private AudioSource enterBossSound;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            characterRigidBody = GetComponent<Rigidbody>();
        }

        public void Initialize(InputAction moveAction, InputAction deltaMouseAction, InputAction jumpAction, InputAction sprintAction)
        {
            moveAction.Enable();
            deltaMouseAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
            moveActionRef = moveAction;
            deltaMouseActionRef = deltaMouseAction;
            jumpRef = jumpAction;
            sprintRef = sprintAction;
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
            //Debug.Log(jumpRef.ReadValue<Vector2>());
            
            if (sprintRef.ReadValue<Vector2>() == new Vector2(0.00f, 1.00f))
            {
                objectStep *= sprintMultiplier;
            }

            if (jumpRef.ReadValue<Vector2>() == new Vector2(0.00f, 1.00f) && transform.position.y < 1.48f)
            {
                Debug.Log("Jump Hit");
                characterRigidBody.AddForce(Vector3.up * 25);
            }

            Vector3 currentPosition = playerToMove.transform.position;
            playerToMove.transform.position = Vector3.MoveTowards(currentPosition, currentPosition + movement, objectStep); 

            // Ensure the character always faces forward
            Vector3 targetDirection = cameraDirectionForward; // The direction character should face
            targetDirection.y = 0; // Remove the vertical component to only rotate horizontally
            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                playerToMove.transform.rotation = Quaternion.RotateTowards(playerToMove.transform.rotation, targetRotation, Mathf.Infinity);
            }
        }

        private void OnTriggerEnter(Collider collidedObject)
        {
            if (collidedObject.tag == "Boss Wall")
            {
                characterRigidBody.Sleep(); //let the character walk through the collider trigger
                bossWallMeshRender = collidedObject.GetComponent<MeshRenderer>();
                bossWallMeshRender.enabled = false;
            }
        }

        private void OnTriggerExit(Collider collidedObject)
        { 
            characterRigidBody.WakeUp();
            if (collidedObject.tag == "Boss Wall")
            {
                bossWallMeshRender.enabled = true;
                collidedObject.isTrigger = false; //do not let the player walk back through
                Quaternion rotation = Quaternion.Euler(0, -90, 0);
                GameObject boss = Instantiate(bossPrefab, new Vector3(16f, 1.5f, 18.18f), rotation) as GameObject;
                enterBossSound.Play();

            }
        }
    }
}
