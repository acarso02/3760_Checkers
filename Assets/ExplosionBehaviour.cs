using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public ParticleSystem pS;
    public AudioSource aS;
    public List<AudioClip> soundList;
    // Start is called before the first frame update
    void Start()
    {
        pS.Play();
        PlayRandomSound(soundList, aS);

        //Self destroy after exploding in pS.someAttrWhichRefersToLength seconds
    }

    private void PlayRandomSound(List<AudioClip> soundList, AudioSource aS) {
        int index = Random.Range(0, soundList.Count);
        aS.clip = soundList[index];
        aS.Play();
    }
}
