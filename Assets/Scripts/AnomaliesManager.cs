using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnomaliesManager : MonoBehaviour
{
    public List<room> rooms = new List<room>();
    public float minTimeToAnomaly;
    public float maxTimeToAnomaly;
    public float changeInMaxTime;
    public float changeInMinTime;
    public float minMaxTime;
    public float minMinTime;
    public int[] roomNums;
    public List<int> spawnableRoomNums = new List<int>();
    public int currentRoomNum;
    public int anomaliesNum;
    public int reportRoomNum;
    public int reportAnomalyType;
    private camera cam;
    public CanvasController CanvasController;
    public AudioSource deathSiren;
    public AudioSource submit;
    public AudioSource tulpaSFX;
    public float timeToGameOver;
    public int anomalyCountGameOver=4;
    bool hugeWave = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<camera>();
        spawnableRoomNums.Add(roomNums[1]);
        spawnableRoomNums.Add(roomNums[2]);
        StartCoroutine(waitForAnomaly());
        CanvasController.SetRoom(rooms[0].roomName);
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.U))
    //     {
    //         startHugeWave();
    //     }
    // }

    public void moveRoom(int roomNum){
        // print(roomNum);
        for(int i=0;i<spawnableRoomNums.Count;i++)
        {
            if(spawnableRoomNums[i]==roomNum)
            {
                spawnableRoomNums.RemoveAt(i);
            }
        }
        CanvasController.SetRoom(rooms[roomNum].roomName);
        spawnableRoomNums.Add(currentRoomNum);
        currentRoomNum=roomNum;
        if (rooms[roomNum].hasTulpa)
        {
            if(tulpaSFX.clip != rooms[roomNum].tulpaSFX)
            {
                tulpaSFX.clip = rooms[roomNum].tulpaSFX;
                tulpaSFX.volume = rooms[roomNum].tulpaVolume;
                tulpaSFX.Play();
            }
            tulpaSFX.mute = false;
        }
        else
        {
            tulpaSFX.mute = true;
        }
    }
    void spawnAnomaly()
    {
        int random=spawnableRoomNums[Random.Range(0,spawnableRoomNums.Count)];

        anomaly anomalyToAdd = rooms[random].rndAnomaly();
        if (anomalyToAdd.tulpa)
        {
            rooms[random].hasTulpa = true;
            rooms[random].tulpaSFX = anomalyToAdd.getTulpaSFX();
            rooms[random].tulpaVolume = anomalyToAdd.tulpaVolume;
        }

        anomalyToAdd.gameObject.SetActive(true);
        anomalyToAdd.active = true;
        rooms[random].spawnableAnomalies.Remove(anomalyToAdd);
        anomaliesNum++;
        if(maxTimeToAnomaly>=minMaxTime){
            maxTimeToAnomaly-=changeInMaxTime;
        }
        if(minTimeToAnomaly>minMinTime){
            minTimeToAnomaly-=changeInMinTime;
        }
        if(anomaliesNum==anomalyCountGameOver-1){
            if (!hugeWave)
            {
                CanvasController.showEmergency();
                StartCoroutine(waitForAnomaly());
            }
        }
        else if (anomaliesNum==anomalyCountGameOver){
            death();
        }
        else{
            if (!hugeWave)
            {
                StartCoroutine(waitForAnomaly());
            }
        }
    }

    IEnumerator waitForAnomaly()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeToAnomaly, maxTimeToAnomaly));
        if(!hugeWave)
            spawnAnomaly();
    }

    public void startHugeWave()
    {
        cam.tulpaDurationModifier = 1;
        hugeWave = true;
        anomalyCountGameOver = 1000;
        int randomCount = Random.Range(5,8);
        for(int i = 0; i < randomCount; i++)
        {
            spawnAnomaly();
        }
        cam.Flash(15f);
        StartCoroutine(cam.CamShakeOnDelay(Random.Range(0.8f,1.2f),Random.Range(0.4f,0.6f),Random.Range(0f,1f)));
        StartCoroutine(cam.CamShakeOnDelay(Random.Range(0.8f,1.2f),Random.Range(0.4f,0.6f),Random.Range(1f,2f)));
        StartCoroutine(cam.CamShakeOnDelay(Random.Range(0.8f,1.2f),Random.Range(0.4f,0.6f),Random.Range(2f,3f)));
        StartCoroutine(cam.CamShakeOnDelay(Random.Range(0.8f,1.2f),Random.Range(0.4f,0.6f),Random.Range(3f,4f)));
        CanvasController.hugeWave();
    }

    public void hugeWave2()
    {
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++)
        {
            spawnAnomaly();
        }
    }

    public void reportSubmit()
    {
        anomaly anomalyToRemove = null;


        List<anomaly> anomaliesInRoom = new List<anomaly>();
        anomaliesInRoom.AddRange(rooms[reportRoomNum].anomalies);

        for(int i=0;i<anomaliesInRoom.Count;i++)
        {
            if(anomaliesInRoom[i].anomalyType==reportAnomalyType && anomaliesInRoom[i].active)
            {
                print(anomaliesInRoom[i].gameObject.name);
                anomalyToRemove = anomaliesInRoom[i];
            }
        }

        if(anomalyToRemove != null)
        {
            submit.Play();
            if (anomalyToRemove.tulpa)
            {
                rooms[reportRoomNum].hasTulpa = false;
                tulpaSFX.mute = true;
            }
            anomalyToRemove.gameObject.SetActive(false);
            anomalyToRemove.active = false;
            anomaliesNum--;
            print(anomaliesNum);
            if(anomaliesNum==anomalyCountGameOver-2){
                CanvasController.hideEmergency();
            }
            cam.Flash();
        }
    }
    public void death(){
        if (!hugeWave)
        {
            deathSiren.Play();
            cam.DeathStatic(1f);
            Invoke("goToGameOver", timeToGameOver + 2);
        }
    }
    public void goToGameOver(){
        SceneManager.LoadScene("RetryScene");
    }
}
