using UnityEngine;
using System.Collections;
using RoigDylan_VukovicCharlie.Lab6;


namespace RoigDylan_VukovicCharlie.Lab6
{
// Written by Dylan Roig
/*
 * script keeps the camera locked in a first person view no matter where the player goes
 */
    public class FollowWithOffset : MonoBehaviour
    {
        [SerializeField] private Transform target; //the target of the camera
        [SerializeField] private Vector3 offset;

        private void Update()
        {
            //move the camera as our target moves
            transform.position = target.position + offset;
        }
    }
}