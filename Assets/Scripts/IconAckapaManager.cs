using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconAckapaManager : MonoBehaviour
{
    public Sprite acikIcon;
    public Sprite kapaliIcon;

    private Image IconImg;

    public bool varsayilanIconDurumu = true;

    private void Start()
    {
        IconImg = GetComponent<Image>();

        /*
        if (varsayilanIconDurumu==true)
        {
            IconImg.sprite = acikIcon;
            
        }
        else
        {
            IconImg.sprite = kapaliIcon;
        }
        */
        
        //kısaca

        IconImg.sprite = (varsayilanIconDurumu) ? acikIcon : kapaliIcon;
    }

    public void IconAcKapatFNc(bool IconDurumu)
    {
        if (!IconImg || !acikIcon || !kapaliIcon)
        {
            return;
        }
        else
        {
            IconImg.sprite = (IconDurumu) ? acikIcon : kapaliIcon;
            
        }
    }
    
}
