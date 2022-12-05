using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 offset;

    private Scene_Manager scene_Manager;

    void Start()
    {
        scene_Manager = FindObjectOfType<Scene_Manager>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(!scene_Manager.IsEndGame())
        {
            transform.position = player.transform.position + offset;
        }
    }
}
