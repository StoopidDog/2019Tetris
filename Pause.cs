using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public static Pause instance { get; set; }

    public bool isPaused;
    public GameObject PauseMenu;
    public GameObject PauseOption;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                OnPause();
            }
            else
            {
                Resume();
            }
        }
	}

    public void OnPause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        PauseOption.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void EnablePauseOption()
    {
        PauseOption.SetActive(true);
    }

    public void DisalblePauseOption()
    {
        PauseOption.SetActive(false);
    }
}
