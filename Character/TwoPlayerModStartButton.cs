using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerModStartButton : MonoBehaviour {

    private void OnMouseDown()
    {
        GameManager.instance.TwoPlayerModStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.TwoPlayerModStart();
        }
    }
}
