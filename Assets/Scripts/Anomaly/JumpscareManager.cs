using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareManager : MonoBehaviour
{
    public int roomNum=0;
    private AudioSource audio;
    private Animator anim;
    private camera camera;
    private PostProcessing pp;

    public GameObject menu;
    private bool isLooping = false;
    public AudioSource tulpaAudio;

    [Header("Saturation")]
    public Vector2 animSpeedRange;
    public float animSpeedShiftFrequency=3f;
    public float animSpeedShiftSpeed=0.05f;
    private float targetAnimSpeed;
    private float currentAnimSpeed;
    private Coroutine animSpeedRoutine;

    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        camera = Camera.main.GetComponent<camera>();
        pp = GameObject.FindWithTag("PostProcessing").GetComponent<PostProcessing>();

        animSpeedRoutine = StartCoroutine(SelectNewAnimSpeed());
        currentAnimSpeed = 1f;
        targetAnimSpeed = 1f;
    }

    void Update()
    {
        currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, targetAnimSpeed, animSpeedShiftSpeed * Time.deltaTime);
        anim.SetFloat("Speed", currentAnimSpeed);
    }

    IEnumerator SelectNewAnimSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.8f,1.2f) * animSpeedShiftFrequency);
            targetAnimSpeed = Random.Range(animSpeedRange.x, animSpeedRange.y);
        }
    }

    public void TriggerJumpscare(string str)
    {
        anim.SetTrigger(str);
        menu.SetActive(false);
        MuteTulpaAudio();
    }

    public void MuteTulpaAudio()
    {
        if(tulpaAudio != null)
            tulpaAudio.mute = true;
    }

    public void LoopJumpscare(string str, bool on)
    {
        isLooping = on;
        anim.SetBool(str, on);
    }

    public void CamShakeSmall(string clipPath)
    {
        JumpscareFX(clipPath, 0.8f, 0.1f);
    }
    public void CamShakeMedium(string clipPath)
    {
        JumpscareFX(clipPath, 1.3f, 1f);
    }
    public void Flashing()
    {
        pp.ModifyExposure(new Vector2(-5f,10f), 0.01f, 10f);
    }

    public void GameOver()
    {
        // SceneManager.LoadScene("RetryScene");
        camera.DeathStatic();
    }

    public void StaticFlash()
    {
        camera.Flash(5f);
    }

    public void StaticFlashSmall()
    {
        camera.Flash(20f);
    }

    private void JumpscareFX(string clipPath, float magnitude, float duration)
    {
        if(camera.currentCam != roomNum) return;
        camera.CamShake(magnitude, duration);

        PlaySFX(clipPath);
    }

    public void PlaySFX(string clipPath)
    {
        if(camera.currentCam != roomNum) return;
        string path = "FX/SFX/"+clipPath;
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);
        if(clips.Length > 0)
        {
            AudioClip clip = clips[Random.Range(0,clips.Length)];
            if(clip != null)
            {
                audio.clip = clip;
                audio.Play();
            }
        }

    }
}
