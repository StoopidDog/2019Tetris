using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour {

    public void Update()
    {
        if(Input.anyKeyDown)
        MainUIManager.instance.DoStart();
    }
}
