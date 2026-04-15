using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyHideSprite : MonoBehaviour
{
    public SpriteRenderer anomalySprite1;
    public SpriteRenderer anomalySprite2;
    public void HideSprite1()
    {
        anomalySprite1.enabled = false;
    }
    public void HideSprite2()
    {
        anomalySprite2.enabled = false;
    }
}
