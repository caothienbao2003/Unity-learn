using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform camTransform;
    private GameObject[] powerups;

    void Start()
    {
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-camTransform.forward, camTransform.up); 
    }
}
