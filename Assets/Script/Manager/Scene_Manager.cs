using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Replay();
        }
    }

    void Replay()
    {
        int currentSceneIndex;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene(currentSceneIndex);
    }

    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public bool IsEndGame()
    {
        if(playerTransform.position.y <= -10f)
        {
            return true;
        }
        return false;
    }

    public bool IsAfterEndGame()
    {
        if(playerTransform.position.y <= -20f)
        {
            return true;
        }
        return false;
    }
}
