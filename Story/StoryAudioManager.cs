using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAudioManager : MonoBehaviour {

    public static StoryAudioManager instance { get; set; }

    private void Awake()
    {
        instance = this;
    }

    public AudioSource audiosource;

    public AudioClip anta;
    public AudioClip antabattle;
    public AudioClip tod;
    public AudioClip orias;
    public AudioClip valepor;
    public AudioClip kero;
    public AudioClip loim;

    // Use this for initialization
    void Start () {
        var SaveLevel = (GameManager.instance.GetSaveData().Stage - 1) * 3 + GameManager.instance.GetSaveData().Level;
        
        switch(SaveLevel)
        {
            case 1:
                audiosource.clip = anta;
                audiosource.Play();
                break;
            case 2:
                audiosource.clip = tod;
                audiosource.Play();
                break;
            case 3:
                audiosource.clip = orias;
                audiosource.Play();
                break;
            case 4:
                audiosource.clip = valepor;
                audiosource.Play();
                break;
            case 5:
                audiosource.clip = kero;
                audiosource.Play();
                break;
            case 6:
                audiosource.clip = loim;
                audiosource.Play();
                break;
        }
	}
}
