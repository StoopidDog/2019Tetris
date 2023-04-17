using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour {

    public void NewGame()
    {
        GameManager.instance.GetSaveData().Stage = 1;
        GameManager.instance.GetSaveData().Level = 1;
        GameManager.instance.GetSaveData().HighScore = 0;
        GameManager.instance.GetSaveData().canLoad = false;
        LoadingSceneManager.LoadScene("CharacterSelect");
    }

    public void LoadGame()
    {
        GameManager.instance.needLoad = true;
        GameManager.instance.GetSaveData().HighScore = 0;
        LoadingSceneManager.LoadScene("Story");
    }

    public void ExitScreen()
    {
        Application.Quit();
    }

    public void CheckLoad()
    {
        if(GameManager.instance.GetSaveData().canLoad)
        {
            StageUIManager.instance.TrueLoad.SetActive(true);
        }
    }

    public void DisableLoad()
    {
        StageUIManager.instance.TrueLoad.SetActive(false);
    }

    public void ClickOption()
    {
        StageUIManager.instance.Option.SetActive(true);
    }

    public void DisableOption()
    {
        StageUIManager.instance.Option.SetActive(false);
    }
}
