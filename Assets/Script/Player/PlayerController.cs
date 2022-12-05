using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float currentSpeed;
    private Rigidbody playerRb;
    private float fowardInput;
    private float horizontalInput;
    private float playerPowerupTime;

    private GameObject focalPoint;
    private float powerupStrength = 15f;
    [SerializeField] private GameObject powerupIndicator;
    private Powerups playerPowerupType;

    private TrailRenderer trail;
    private Spawn_Manager spawnManager;

    private IEnumerator powerupCountDownCo;

    [SerializeField] private float balloonFlySpeed = 30f;

    [SerializeField] private GameObject balloonOnBall;
    [SerializeField] private float balloonHeight = 5f;
    [SerializeField] private float balloonForce = 100f;

    private bool hasBalloonDrop;
    [SerializeField] private GameObject balloonGroundEffect;
    private Scene_Manager scene_Manager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        powerupIndicator.SetActive(false);
        currentSpeed = speed;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
        spawnManager = FindObjectOfType<Spawn_Manager>();
        balloonOnBall.SetActive(false);

        playerPowerupType = Powerups.none;
        scene_Manager = FindObjectOfType<Scene_Manager>();
    }

    void Update()
    {
        if(scene_Manager.IsEndGame())
        {
            return;
        }

        fowardInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        powerupIndicator.transform.position = transform.position;

        if(playerPowerupType == Powerups.balloon)
        {
            if(transform.position.y >= balloonHeight)
            {
                hasBalloonDrop = true;
                balloonOnBall.SetActive(false);
            }
            if(!hasBalloonDrop)
            {
                playerRb.AddForce(Vector3.up * balloonFlySpeed * Time.deltaTime);
                
            }
        }
    }

    void FixedUpdate()
    {
        if(scene_Manager.IsEndGame())
        {
            return;
        }
        playerRb.AddForce(focalPoint.transform.forward * currentSpeed * fowardInput * Time.deltaTime);
        playerRb.AddForce(focalPoint.transform.right * currentSpeed * horizontalInput * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            if(powerupCountDownCo!=null)
            {
                StopCoroutine(powerupCountDownCo);
            }

            powerupCountDownCo = PowerupCountDownCo();

            EndAllPowerUp();

            Powerup powerup = other.GetComponent<Powerup>();

            playerPowerupType = powerup.powerupType;
            playerPowerupTime = powerup.powerupTime;

            switch(playerPowerupType)
            {
                case Powerups.fire:
                    TriggerFirePowerup();
                    break;
                case Powerups.lightning:
                    TriggerLightningPowerup();
                    break;
                case Powerups.balloon:
                    TriggerBalloonPowerup(other);
                    break;
            }
            
            if(playerPowerupType!=Powerups.balloon)
            {
                StartCoroutine(powerupCountDownCo);
            }
            

            Destroy(other.gameObject); 
            
            StartCoroutine(spawnManager.SpawningPowerupCo());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        switch(playerPowerupType)
        {
            case Powerups.fire:
                CollideFirePowerup(other);
                break; 
            case Powerups.balloon:
                CollideBalloonPowerUp(other);
                break;
        } 
    }

    IEnumerator PowerupCountDownCo()
    {
        yield return new WaitForSeconds(playerPowerupTime);
        EndAllPowerUp();
    }

    void TriggerFirePowerup()
    {
        powerupIndicator.SetActive(true);
    }

    void CollideFirePowerup(Collision other)
    {
        if(other.collider.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.transform.position - transform.position).normalized;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    void EndFirePowerup()
    {
        powerupIndicator.SetActive(false);
    }

    void TriggerLightningPowerup()
    {
        trail.enabled = true;
        currentSpeed = speed * 2;
    }

    void EndLightningPowerup()
    {
        trail.enabled = false;
        currentSpeed = speed;
    }

    void TriggerBalloonPowerup(Collider other)
    {
        balloonOnBall.SetActive(true);
    }

    IEnumerator EndBalloonPowerup()
    {
        playerPowerupType = Powerups.none;
        yield return new WaitForSeconds(3f);
        balloonGroundEffect.SetActive(false);
        
    }

    void CollideBalloonPowerUp(Collision other)
    {
        if((other.collider.CompareTag("Enemy") || other.collider.CompareTag("Ground")) && hasBalloonDrop)
        {
            balloonGroundEffect.SetActive(true);
            Enemy[] enemies;
            enemies = FindObjectsOfType<Enemy>();

            foreach(Enemy enemy in enemies)
            {
                Vector3 flyDir;
                flyDir = (enemy.transform.position - transform.position).normalized;
                enemy.GetComponent<Rigidbody>().AddForce(flyDir * balloonForce, ForceMode.Impulse);
            }

            hasBalloonDrop = false;
            StartCoroutine(EndBalloonPowerup());
        }
    }

    void EndAllPowerUp()
    {
        EndFirePowerup();
        EndLightningPowerup();
        playerPowerupType = Powerups.none;
    }
}
