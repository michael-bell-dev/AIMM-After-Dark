using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public string[] sentences;
    private int index;
    public float typingspeed;
    float currenttypingspeed;

    public GameObject continuebutton;
    public GameObject startbutton;
    public GameObject dialoguePanel;
    public AudioSource click;
    public bool playSound;
    public bool isGameScene;

    public void Start()
    {
        continuebutton.SetActive(false);
        dialoguePanel.SetActive(false);
        if(!isGameScene){
            startbutton.SetActive(false);
            startDialogue();
        }
    }
    public void startDialogue(){
        dialoguePanel.SetActive(true);
        currenttypingspeed=typingspeed;
        StartCoroutine(Type());
        playSound=true;
        if(isGameScene){
            foreach(GameObject anomaliesObj in GameObject.FindGameObjectsWithTag("anomalies"))
            {
                anomaliesObj.SetActive(false);
            }
            GameObject.Find("AnomalyManager").SetActive(false);
        }
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            playSound=false;
            currenttypingspeed=0;
        }
    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            if(letter=='$'){
                TextDisplay.text += "\n";
            }
            else if(letter=='%'){
                if(index<sentences.Length - 1){
                    continuebutton.SetActive(true);
                }
                else if(!isGameScene)
                {
                    startbutton.SetActive(true);
                }
            }
            else{
                TextDisplay.text += letter;
                if(playSound){
                    click.pitch = UnityEngine.Random.Range(0.96f,1.07f);
                    click.Play();
                }
            }
            yield return new WaitForSeconds(currenttypingspeed);
        }
    }
    public void nextsentence()
    {
        currenttypingspeed=typingspeed;
        playSound=true;
        continuebutton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            TextDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            continuebutton.SetActive(false);
            TextDisplay.text = "";
        }
    }
    public void startGame(){
        SceneManager.LoadScene("GameScene");
    }
}
