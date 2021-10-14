using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MuteAudioToggle : MonoBehaviour
{
    public Sprite OnImage;
    public Sprite OffImage;
    public Image AudioImage;

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
            AudioImage.sprite = OffImage;
        }
        else
        {
            AudioListener.volume = 1;
            AudioImage.sprite = OnImage;
        }
    }
    
}
