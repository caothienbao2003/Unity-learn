using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    private float horizontalInput;
    private GameObject player;
    private Scene_Manager scene_Manager;

    void Start()
    {
        player = GameObject.Find("Player");
        scene_Manager = FindObjectOfType<Scene_Manager>();
    }

    void Update()
    {
        if(scene_Manager.IsEndGame())
        {
            return;
        }
        
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
