using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerMod : MonoBehaviour {

    public void StartTwoPlayerMode()
    {
        LoadingSceneManager.LoadScene("CharacterSelect2P");
    }

    private void OnMouseDown()
    {
        LoadingSceneManager.LoadScene("CharacterSelect2P");
    }
}