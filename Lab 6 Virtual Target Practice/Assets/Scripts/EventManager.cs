using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RoigDylan_VukovicCharlie.Lab6
// Written by Charlie Vukovic
/*
 * class for managing the events used in the game
 */
{  
    public class EventManager
    {
        // event for losing the game
        public static event UnityAction gameLost;
        public static void OnGameLost() => gameLost?.Invoke();

        // event for winning the game
        public static event UnityAction gameWon;
        public static void OnGameWon() => gameWon?.Invoke();

        // event for switching weapon
        public static event UnityAction weaponSwitch;
        public static void OnWeaponSwitch() => weaponSwitch?.Invoke();

    }
}