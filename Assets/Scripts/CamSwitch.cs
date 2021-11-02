using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject redCam;
    public GameObject blackCam;

    private bool flag = true;

    void Start() {
        redCam.SetActive(true);
        blackCam.SetActive(false);
        //Debug.Log(Camera.main);
    }


    public void switchCam() {
        if(flag == false) {
            redCam.SetActive(true);
            blackCam.SetActive(false);
        } else {
            redCam.SetActive(false);
            blackCam.SetActive(true);
        }
        flag = !flag;
    }
}
