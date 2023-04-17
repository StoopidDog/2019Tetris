using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningBGMManager : MonoBehaviour {

    public static OpeningBGMManager instance { get; set; }

    public AudioSource audiosource;

    public AudioClip opening_one;
    public AudioClip opening_two;
    public AudioClip opening_three;
    public AudioClip opening_four;

    IEnumerator coroutine;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        coroutine = FadeOut(audiosource, 5f);

        audiosource.clip = opening_one;

        audiosource.Play();
    }
	
    public void ChangeBGM_openingtwo()
    {
        if(audiosource.clip == opening_one)
        {
            audiosource.clip = opening_two;
            audiosource.Play();
            audiosource.volume = GameManager.instance.GetSaveData().Volume;
        }
    }

    public void ChangeBGM_openingthree()
    {
        if (audiosource.clip == opening_two)
        {
            audiosource.clip = opening_three;
            audiosource.Play();
            audiosource.volume = GameManager.instance.GetSaveData().Volume;
        }
    }

    public void ChangeBGM_openingfour()
    {
        if (audiosource.clip == opening_three)
        {
            audiosource.clip = opening_four;
            audiosource.Play();
            audiosource.volume = GameManager.instance.GetSaveData().Volume;
        }
    }

    public IEnumerator FadeOut(AudioSource a, float duration)
    {
        float startVolume = a.volume;

        while (a.volume > 0f)
        {
            a.volume -= startVolume * Time.deltaTime / duration;
            yield return new WaitForFixedUpdate();
        }

        //a.Stop();
    }

    public void Coroutine_START()
    {
        StartCoroutine(coroutine);
    }

    public void Coroutine_STOP()
    {
        StopCoroutine(coroutine);
        audiosource.volume = GameManager.instance.GetSaveData().Volume;
    }
}
