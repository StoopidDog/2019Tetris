﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    public static string nextScene;

    [SerializeField]
    Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    string nextSceneName;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
        loading.allowSceneActivation = false;

        float timer = 0.0f;
        while (!loading.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (loading.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                    loading.allowSceneActivation = true;
            }
            else
            {

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, loading.progress, timer);
                if (progressBar.fillAmount >= loading.progress)
                {
                    timer = 0f;
                }
            }
        }
    }

}
