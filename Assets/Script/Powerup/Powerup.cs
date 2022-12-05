using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Powerups
    {
        fire,
        lightning,
        balloon,
        radiation,
        star,
        none
    }

public class Powerup : MonoBehaviour
{
    [SerializeField] public Powerups powerupType;

    [SerializeField] public float powerupTime;
}
