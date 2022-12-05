using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float destroyHeight = -20f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = (player.transform.position - transform.position).normalized;
        lookDir.y = 0;
        enemyRb.AddForce(lookDir * speed * Time.deltaTime);

        if(transform.position.y < destroyHeight)
        {
            Destroy(this.gameObject);
        }
    }
}
