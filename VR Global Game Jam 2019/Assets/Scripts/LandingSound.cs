using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSound : MonoBehaviour
{
    public AudioSource audio;

    public void Play()
    {
        DontDestroyOnLoad(this.gameObject);
        audio.time = 3f;
        StartCoroutine(PlayImpl());
    }

    private IEnumerator PlayImpl()
    {
        audio.Play();
        yield return new WaitForSeconds(1);
        audio.volume = 0.75f;
        yield return new WaitForSeconds(2);
        audio.volume = 0.5f;
        yield return new WaitForSeconds(3);
        audio.volume = 0.25f;
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
    
}
