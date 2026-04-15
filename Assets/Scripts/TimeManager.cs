using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public TMP_Text hoursText;
    public float timeRemaining = 10;
    public bool timerIsRunning = true;
    public float timeMult;
    public float maxMinutes;
    public CanvasController CanvasController;
    public DialogueManager DialogueManager;
    public AnomaliesManager anomaliesManager;
    bool hugeWave = false;
    bool hugeWave2 = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeMult = 6 / maxMinutes; //666 seconds. Gaster reference?
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (!hugeWave && timeRemaining > 295)
            {
                hugeWave = true;
                anomaliesManager.startHugeWave();
            }
            else if (!hugeWave2 && timeRemaining > 340)
            {
                hugeWave2 = true;
                anomaliesManager.hugeWave2();
            }
            if (timeRemaining < 360)
            {
                timeRemaining += Time.deltaTime*timeMult;
                DisplayTime(timeRemaining);
            }
            else
            {
                if(anomaliesManager.anomaliesNum == 0)
                {
                    CanvasController.hideReport();
                    DialogueManager.startDialogue();
                    timerIsRunning = false;
                    gameObject.SetActive(false);
                }
                else
                {
                    anomaliesManager.goToGameOver();
                }
            }
        }

    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        if(minutes == 0)
        {
            minutes = 12;
        }
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        hoursText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

}
