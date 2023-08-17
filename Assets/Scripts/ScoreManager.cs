using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int satirlar;
    public int level = 1;

    public int seviyedekiSatirSayisi = 5;

    private int minSatir = 1;
    private int maxSatir = 4;

    public TextMeshProUGUI satirTxt;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI scoreTxt;

    public bool LevelGecildimi = false;

    private void Start()
    {
        ResetFNC();
    }

    public void ResetFNC()
    {
        level = 1;
        satirlar = seviyedekiSatirSayisi * level;
        TextGuncelleFNC();
        
    }
    public void SatirSkoru(int n)
    {
        LevelGecildimi = false;
        n = Mathf.Clamp(n, minSatir, maxSatir);

        switch (n)
        {
            case 1:
                score += 30 * level;
                break;
            case 2:
                score += 50 * level;
                break;
            case 3:
                score += 150 * level;
                break;
            case 4:
                score += 500 * level;
                break;
            case 5:
                score += 750 * level;
                break;
        }

        satirlar -= n;
        if (satirlar<=0)
        {
            LevelAtlaFNC();
        }
        TextGuncelleFNC();
    }

    void TextGuncelleFNC()
    {
        if (scoreTxt)
        {
            scoreTxt.text = BasaSifirEkleFNC(score, 5);
        }

        if (levelTxt)
        {
            levelTxt.text = level.ToString();
        }

        if (satirTxt)
        {
            satirTxt.text = satirlar.ToString();
        }
    }

    string BasaSifirEkleFNC(int score, int rakamSayisi)
    {
        string scoreStr = score.ToString();
        while (scoreStr.Length<rakamSayisi)
        {
            scoreStr = "0" + scoreStr;
        }

        return scoreStr;
    }

    public void LevelAtlaFNC()
    {
        level++;
        satirlar = seviyedekiSatirSayisi * level;
        LevelGecildimi = true;

    }
}
