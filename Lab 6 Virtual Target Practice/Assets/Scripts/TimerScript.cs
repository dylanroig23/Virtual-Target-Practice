using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RoigDylan_VukovicCharlie.Lab6
{   
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
            float time = Time.time - startTime;
            string min = ((int) time / 60).ToString();
            string sec = (time % 60).ToString("f2");
            timerText.text = min+":"+sec;

            if(end){
                finalTime.text = min+":"+sec;
                end = !end;
            }
        }

    }
}