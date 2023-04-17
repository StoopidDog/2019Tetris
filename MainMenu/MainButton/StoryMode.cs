using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMode : MonoBehaviour {

	public void StartStoryMode()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
