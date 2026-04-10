using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjustments;

    [Header("Exposure")]
    public Vector2 exposureRange;
    public float exposureShiftFrequency=2f;
    public float exposureShiftSpeed=2f;
    private float targetExposure;
    private Coroutine exposureRoutine;
    public void ModifyExposure(Vector2 range, float shiftFreq, float shiftSpeed)
    {
        exposureRange = range;
        exposureShiftFrequency = shiftFreq;
        exposureShiftSpeed = shiftSpeed;
        StopCoroutine(exposureRoutine);
        exposureRoutine = StartCoroutine(SelectNewExposure());
    }

    [Header("Contrast")]
    public Vector2 contrastRange;
    public float contrastShiftFrequency=2f;
    public float contrastShiftSpeed=2f;
    private float targetContrast;
    private Coroutine contrastRoutine;

    [Header("Saturation")]
    public Vector2 saturationRange;
    public float saturationShiftFrequency=3f;
    public float saturationShiftSpeed=0.05f;
    private float targetSaturation;
    private Coroutine saturationRoutine;

    void Start()
    {
        volume.profile.TryGet(out colorAdjustments);

        exposureRoutine = StartCoroutine(SelectNewExposure());
        contrastRoutine = StartCoroutine(SelectNewContrast());
        saturationRoutine = StartCoroutine(SelectNewSaturation());
    }

    void Update()
    {
        colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, targetExposure, exposureShiftSpeed * Time.deltaTime);
        colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, targetContrast, contrastShiftSpeed * Time.deltaTime);
        colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, targetSaturation, saturationShiftSpeed * Time.deltaTime);
    }

    IEnumerator SelectNewExposure()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.8f,1.2f) * exposureShiftFrequency);
            targetExposure = Random.Range(exposureRange.x, exposureRange.y);
        }
    }
    IEnumerator SelectNewContrast()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.8f,1.2f) * contrastShiftFrequency);
            targetContrast = Random.Range(contrastRange.x, contrastRange.y);
        }
    }
    IEnumerator SelectNewSaturation()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.8f,1.2f) * saturationShiftFrequency);
            targetSaturation = Random.Range(saturationRange.x, saturationRange.y);
        }
    }
}
