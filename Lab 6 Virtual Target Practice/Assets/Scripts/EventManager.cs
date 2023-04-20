using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RoigDylan_VukovicCharlie.Lab6
{  
    public class EventManager
    {
        public static event UnityAction gameLost;
        public static void OnGameLost() => gameLost?.Invoke();

        public static event UnityAction gameWon;
        public static void OnGameWon() => gameWon?.Invoke();

        public static event UnityAction weaponSwitch;
        public static void OnWeaponSwitch() => weaponSwitch?.Invoke();

    }
}