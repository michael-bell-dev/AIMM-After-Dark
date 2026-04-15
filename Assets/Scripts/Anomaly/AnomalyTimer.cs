using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyTimer : MonoBehaviour
{
    public int roomNum;
    private camera camera;
    public float duration=3f;
    private float timeRemaining;
    private EnableJumpscareObj jumpscareObj;
    private SpriteRenderer spriteRenderer;
    public float delay=0.1f;

    void Start()
    {
        camera = Camera.main.GetComponent<camera>();
        jumpscareObj = GetComponent<EnableJumpscareObj>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if(spriteRenderer != null)
            spriteRenderer.enabled = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        timeRemaining = duration + camera.tulpaDurationModifier;
        while (true)
        {
            yield return null;
            if(roomNum == camera.currentCam)
            {
                timeRemaining -= Time.deltaTime;
                if(timeRemaining <= 0 && spriteRenderer.enabled)
                {
                    jumpscareObj.Jumpscare(delay);
                }
            }

        }
    }
}
