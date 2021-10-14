using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VolumeImageSwitch : MonoBehaviour
{
    public Sprite FirstImage;
    public Sprite SecondImage;
    public Toggle givenToggle;

    public void ChangeToggleImage()
    {
        Debug.Log("TEST" + givenToggle.GetComponent<Image>());
        print(givenToggle.GetComponent<Image>());
        givenToggle.image.sprite = SecondImage;
        //if (givenToggle.GetComponent<Image>()).equals(FirstImage);
            //givenToggle.
    }
}
