using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject endGamePanel;

    private float timeScore;
    private Scene_Manager scene_Manager;
    // Start is called before the first frame update
    void Start()
    {
        scene_Manager = FindObjectOfType<Scene_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_Manager.IsEndGame())
        {
            DisplayEndGamePanel();
            return;
        }

        timeScore += Time.deltaTime;
        

        timeText.text = timeScore.ToString("F2");
    }

    public void UpdateWaveText(int number)
    {
        waveText.text = "WAVE " + number.ToString();
    }

    void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
}
