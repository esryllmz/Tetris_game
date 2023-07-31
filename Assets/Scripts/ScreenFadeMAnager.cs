using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenFadeMAnager : MonoBehaviour
{
    public float baslangicAlpha = 1f;
    public float bitisAlpha = 0f;


    public float beklemeSuresi = 0f;
    public float fadeSuresi = 1f;

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = baslangicAlpha;
        StartCoroutine(FadeRoutineFNC());
        
    }

    IEnumerator FadeRoutineFNC()
    {
        yield return new WaitForSeconds(beklemeSuresi);
        GetComponent<CanvasGroup>().DOFade(bitisAlpha, fadeSuresi);
    }
}
