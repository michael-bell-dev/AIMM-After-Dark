using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    public List<anomaly> anomalies = new List<anomaly>();
    public List<anomaly> spawnableAnomalies = new List<anomaly>();
    public bool hasTulpa;
    public AudioClip tulpaSFX;
    public float tulpaVolume;
    public string roomName = "";

    void Awake()
    {
        anomalies.Clear();
        GameObject anomaliesObj=transform.GetChild(0).gameObject;
        foreach(Transform child in anomaliesObj.gameObject.transform)
        {
            anomalies.Add(child.gameObject.GetComponent<anomaly>());
            spawnableAnomalies.Add(child.gameObject.GetComponent<anomaly>());
        }
        for(int i=0;i<anomalies.Count;i++)
        {
            anomaly anomaly = anomalies[i];
            if(anomaly == null) Debug.Log("Null anomaly on "+gameObject.name);
            GameObject anomalyObj = anomaly.gameObject;
            anomalyObj.SetActive(false);
        }
    }

    public anomaly rndAnomaly()
    {
        List<anomaly> currentSpawnableAnomalies = new List<anomaly>();
        for(int i = 0; i < spawnableAnomalies.Count; i++)
        {
            if(spawnableAnomalies[i].tulpa)
            {
                if(!hasTulpa)
                    currentSpawnableAnomalies.Add(spawnableAnomalies[i]);
            }
            else
            {
                currentSpawnableAnomalies.Add(spawnableAnomalies[i]);
            }
        }
        if(currentSpawnableAnomalies.Count==0){
            GameObject anomaliesObj=transform.GetChild(0).gameObject;
            foreach(Transform child in anomaliesObj.gameObject.transform)
            {
                spawnableAnomalies.Add(child.gameObject.GetComponent<anomaly>());
            }
            for(int i = 0; i < spawnableAnomalies.Count; i++)
            {
                if(spawnableAnomalies[i].tulpa)
                {
                    if(!hasTulpa)
                        currentSpawnableAnomalies.Add(spawnableAnomalies[i]);
                }
                else
                {
                    currentSpawnableAnomalies.Add(spawnableAnomalies[i]);
                }
            }
        }
        return currentSpawnableAnomalies[Random.Range(0, currentSpawnableAnomalies.Count)];
    }
    
}
