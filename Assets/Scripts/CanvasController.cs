using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameObject reportPanel;
    public bool reportPanelUp;
    public GameObject emergencyPanel;
    public bool reportPanelLocked;
    public TextMeshProUGUI roomText;
    public TextMeshProUGUI panelText;
    // Start is called before the first frame update
    void Start()
    {
        emergencyPanel.SetActive(false);
        reportPanel.SetActive(false);
        reportPanelUp=false;
    }

    public void SetRoom(string roomName)
    {
        roomText.SetText(roomName);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&!reportPanelLocked){
            if(reportPanelUp){
                reportPanel.SetActive(false);
                reportPanelUp=false;
            }
            else{
                reportPanel.SetActive(true);
                reportPanelUp=true;
            }
        }
    }
    public void hideReport(){
        reportPanel.SetActive(false);
        reportPanelUp=false;    
        reportPanelLocked=true;
    }
    public void unlockReport(){
        reportPanelLocked=false;
    }
    public void showEmergency(){
        emergencyPanel.SetActive(true);
    }
    public void hideEmergency(){
        emergencyPanel.SetActive(false);
    }
    public void hugeWave()
    {
        panelText.SetText("A huge wave of anomalies has appeared!");
        emergencyPanel.SetActive(true);
    }
    public void win(){
        print("You win! Yipee!");
    }
}
