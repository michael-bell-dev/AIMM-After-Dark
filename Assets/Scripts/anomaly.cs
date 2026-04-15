using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anomaly : MonoBehaviour
{
    public int anomalyType;
    public bool tulpa;
    public bool active;
    public string tulpaSFXPath="Tulpa";
    public float tulpaVolume = 0.25f;

    public AudioClip getTulpaSFX()
    {
        string path = "FX/SFX/"+tulpaSFXPath;
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);
        if(clips.Length > 0)
        {
            AudioClip clip = clips[Random.Range(0,clips.Length)];
            if(clip != null)
            {
                return clip;
            }
        }
        return null;
    }
}
