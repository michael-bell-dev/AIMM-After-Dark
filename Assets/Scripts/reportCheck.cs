using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class reportCheck : MonoBehaviour
{
    public TMP_Text[] locationTexts;
    public bool isLocation;
    public AnomaliesManager AnomaliesManager;
    public AudioSource click;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<locationTexts.Length;i++){
            uncheck(i);
        }
    }

    public void uncheck(int textToUncheck){
        locationTexts[textToUncheck].text="[  ]";
    }
    public void check(int textToCheck){
        click.Play();
        locationTexts[textToCheck].text="[X]";
        for (int i=0;i<locationTexts.Length;i++){
            if(i!=textToCheck){
                uncheck(i);
            }
        }
        if(isLocation){

            AnomaliesManager.reportRoomNum=textToCheck;
        }
        else{
            AnomaliesManager.reportAnomalyType=textToCheck;
        }
    }

}
