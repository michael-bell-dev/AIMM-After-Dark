using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera : MonoBehaviour
{
    public int currentCam;
    // public Transform[] camPositions;
    public Transform[] rooms;
    public float camZ=-10f;
    public AnomaliesManager AnomaliesManager;

    [Header("Static Flash")]
    public MeshRenderer mesh;
    private Material mat;

    public float alpha = 0f;
    public float maxAlpha=1f;
    public float flashSpeed=2f;
    public CanvasController CanvasController;
    public int tulpaDurationModifier = 0;
    // Start is called before the first frame update
    void MoveRoom(bool startingCall)
    {
        Vector3 camPos = rooms[currentCam].position;
        camPos.z = camZ;
        transform.position=camPos;
        if(!startingCall){
            AnomaliesManager.moveRoom(currentCam);
        }
    }
    void Start()
    {
        MoveRoom(true);
        mat = new Material(mesh.material);
        mesh.material = mat;
        mat.SetFloat("_Alpha",0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            currentCam=(currentCam+1)%rooms.Length;
            MoveRoom(false);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentCam-1==-1){
                currentCam=rooms.Length-1;
            }
            else{
                currentCam=(currentCam-1);
            }
            MoveRoom(false);
        }

    }

    public IEnumerator CamShakeOnDelay(float shakeMagnitude, float shakeDur, float delay)
    {
        yield return new WaitForSeconds(delay);
        CamShake(shakeMagnitude, shakeDur);
    }

    public void CamShake(float shakeMagnitude, float shakeDur)
    {
        StartCoroutine(CamShakeRoutine(shakeMagnitude, shakeDur));
    }
    //----------------------------------------------------
    // camShake()
    // Shakes the camera, can be remotely called by various gameObjects
    //----------------------------------------------------
    public IEnumerator CamShakeRoutine(float shakeMagnitude, float shakeDur)
    {
        Vector3 originalPos = transform.position;
        float elapsedTime = 0f;
        while(elapsedTime < shakeDur)
        {
            // Shakes the camera for the specified duration
            float xOffset = Random.Range(-0.5f, 0.5f) * shakeMagnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPos.x+xOffset, originalPos.y+yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // Return to the original camera position
        transform.localPosition = originalPos;
    }

    public void Flash(float mod=1f)
    {
        CanvasController.hideReport();
        alpha = 0f;
        StartCoroutine(FlashRoutine(mod));
    }

    IEnumerator FlashRoutine(float mod)
    {
        while(alpha < maxAlpha)
        {
            yield return null;
            mat.SetFloat("_Alpha",alpha);
            alpha += flashSpeed * Time.deltaTime * mod;
        }
        while(alpha > 0)
        {
            yield return null;
            mat.SetFloat("_Alpha",alpha);
            alpha -= flashSpeed * Time.deltaTime * mod;
        }
        CanvasController.unlockReport();
        
    }

    public void DeathStatic(float speedMod=10f)
    {
        alpha = 0f;
        StartCoroutine(DeathStaticRoutine(speedMod));
    }

    IEnumerator DeathStaticRoutine(float speedMod)
    {
        while(alpha < maxAlpha)
        {
            yield return null;
            mat.SetFloat("_Alpha",alpha);
            alpha += flashSpeed * speedMod * Time.deltaTime;
        }

        SceneManager.LoadScene("RetryScene");
    }
}
