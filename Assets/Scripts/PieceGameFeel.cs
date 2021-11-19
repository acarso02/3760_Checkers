using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGameFeel : MonoBehaviour
{
    public AudioSource jiggleAudioSource;
    public List<AudioClip> jiggleSoundList;

    public AudioSource moveAudioSource;
    public List<AudioClip> moveSoundList;

    public GameObject deathExplosion;

    public float jiggleTimeInSeconds;
    public float dropTimeInSeconds;
    public float stepAmountInSeconds;

    private Coroutine curRoutine;

    public void Start() {
        curRoutine = null;
    }

    public void DoPieceJiggle() {
        StopCurRoutine();
        curRoutine = StartCoroutine(PieceJiggle(jiggleTimeInSeconds));
    }


    //This should end/reset when the piece moves
    private IEnumerator PieceJiggle(float lengthInSeconds) {
        float curTime = 0;
        float curPercent = 0;
        float scale = 0;

        PlayRandomSound(jiggleSoundList, jiggleAudioSource);

        while (curTime < lengthInSeconds) {
            //calculate the new scale acording to this graph https://pasteboard.co/up1v1vn2ojOi.png
            scale = (Mathf.Sin(curPercent/0.16f + 1.6f)+3)/4;

            transform.localScale = new Vector3(scale,scale,scale);
            jiggleAudioSource.pitch = scale;

            //increase the amount of time
            curTime += stepAmountInSeconds;
            //recalculate the percentage of time past
            curPercent = curTime / lengthInSeconds; 
            //wait
            yield return new WaitForSeconds(stepAmountInSeconds);
        }
    }

    public void DoPieceDrop() {
        StopCurRoutine();
        curRoutine = StartCoroutine(PieceDrop(dropTimeInSeconds));
    }

    private IEnumerator PieceDrop(float lengthInSeconds) {
        float curTime = 0;
        float curPercent = 0;
        float height = 0;

        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        while (curTime < lengthInSeconds) {
            //calculate the new height acording to the graph y= -x^3 + 1
            height = -1 * curPercent * curPercent * curPercent + 1;

            transform.position = new Vector3(xPos, height + yPos, zPos);

            //increase the amount of time
            curTime += stepAmountInSeconds;
            //recalculate the percentage of time past
            curPercent = curTime / lengthInSeconds; 
            //wait
            yield return new WaitForSeconds(stepAmountInSeconds);
        }

        transform.position = new Vector3(xPos, yPos, zPos);

        PlayRandomSound(moveSoundList, moveAudioSource);
    }

    private void PlayRandomSound(List<AudioClip> soundList, AudioSource aS) {
        int index = Random.Range(0, soundList.Count);
        aS.clip = soundList[index];
        aS.Play();
    }

    public void StopCurRoutine() {
        if (curRoutine != null) StopCoroutine(curRoutine);
        transform.localScale = Vector3.one;
        jiggleAudioSource.Stop();
        moveAudioSource.Stop();
    }

    public void DoPieceExplosion() {
        GameObject.Instantiate(deathExplosion, transform.position, transform.rotation);
    }
}