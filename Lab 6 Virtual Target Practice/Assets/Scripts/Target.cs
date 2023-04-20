using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoigDylan_VukovicCharlie.Input;
using RoigDylan_VukovicCharlie.Lab6;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
namespace RoigDylan_VukovicCharlie.Lab6
{   
    public class Target : MonoBehaviour
    {
        private float distanceToBob = 0.5f;
        private float bobbingSpeed = 0.5f;
        private Vector3 startingPosition;

        // Start is called before the first frame update
        void Start()
        {
            startingPosition = transform.position;
        } 

        // Update is called once per frame
        void Update()
        {
            Vector3 distanceToMove = new Vector3(0, Mathf.Sin(Time.time * bobbingSpeed) * distanceToBob, 0);
            transform.position = startingPosition + distanceToMove;
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }

        private void OnCollisionEnter(Collision collision)
        {

        }
    }
}