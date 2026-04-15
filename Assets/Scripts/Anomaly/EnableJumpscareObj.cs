using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableJumpscareObj : MonoBehaviour
{
    public string jumpscareString;
    public JumpscareType jumpscareType;
    public enum JumpscareType
    {
        Loop,
        Trigger
    }
    public JumpscareManager manager;
    public bool jumpscareOnEnable=true;

    void OnEnable()
    {
        if(jumpscareOnEnable)
            StartCoroutine(JumpscareRoutine(0.1f));
    }

    void OnDisable()
    {
        switch(jumpscareType)
        {
            case JumpscareType.Loop:
                manager.LoopJumpscare(jumpscareString, false);
                break;
        }
    }

    public void Jumpscare(float delay)
    {
        StartCoroutine(JumpscareRoutine(delay));
    }

    IEnumerator JumpscareRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        switch(jumpscareType)
        {
            case JumpscareType.Loop:
                manager.LoopJumpscare(jumpscareString, true);
                break;
            case JumpscareType.Trigger:
                manager.TriggerJumpscare(jumpscareString);
                break;
        }
    }
}
