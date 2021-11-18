using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGameFeel : MonoBehaviour
{
    public AudioSource aS;

    public float jiggleTimeInSeconds;
    public float stepAmountInSeconds;

    //If this were a real software company, I would probably have to talk to HR for these names
    private bool isJiggling;

    public void Start() {
        isJiggling = false;
    }

    public void doPieceJiggle() {
        if (!isJiggling) StartCoroutine(pieceJiggle(jiggleTimeInSeconds));
    }

    private IEnumerator pieceJiggle(float lengthInSeconds) {
        float curTime = 0;
        float curPercent = 0;
        float scale = 0;

        isJiggling = true;

        aS.Play();
        while (curTime < lengthInSeconds) {
            //calculate the new scale acording to this graph https://pasteboard.co/up1v1vn2ojOi.png
            scale = (Mathf.Sin(curPercent/0.16f + 1.6f)+3)/4;

            transform.localScale = new Vector3(scale,scale,scale);
            aS.pitch = scale;

            //increase the amount of time
            curTime += stepAmountInSeconds;
            //recalculate the percentage of time past
            curPercent = curTime / lengthInSeconds; 
            //wait
            yield return new WaitForSeconds(stepAmountInSeconds);
        }

        isJiggling = false;
    }
}