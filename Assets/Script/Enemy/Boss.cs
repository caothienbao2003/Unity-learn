using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    [SerializeField] private float absortSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir;
        lookDir = (transform.position - player.transform.position).normalized;
        playerRb.AddForce(lookDir * absortSpeed * Time.deltaTime);
    }
}
