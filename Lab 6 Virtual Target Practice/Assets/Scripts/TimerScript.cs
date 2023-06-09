using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RoigDylan_VukovicCharlie.Lab6
{   
// Written by Charlie Vukovic
/*
 * scripts to handle the conversion and display of the time in the overlay
 */
    public class TimerScript : MonoBehaviour
    {
        public Text timerText;
        public Text finalTime;
        public static bool end = false;
        public static float startTime;
        // Start is called before the first frame update
        void Start()
        {
            startTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            // convert to string
            float time = Time.time - startTime;
            string min = ((int) time / 60).ToString();
            string sec = (time % 60).ToString("f2");
            timerText.text = min+":"+sec;

            // when the end is reached, display it on the end screen
            if(end){
                finalTime.text = min+":"+sec;
                end = !end;
            }
        }

    }
}