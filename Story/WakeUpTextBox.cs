using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpTextBox : MonoBehaviour {

    bool isLock = false;

	// Use this for initialization
	void Start () {
        isLock = false;
        StoryReader.instance.GoToStory();
        BackGround.instance.changeBackground();
    }
	
	// Update is called once per frame
	void Update () {
        var st = StoryReader.instance;
        if ((st.Stage == 1 && st.storyline == 24) && !isLock)
        {
            isLock = true;
            st.AntaAfter();
        }
        else if ((st.Stage == 2 && st.storyline == 19) && !isLock)
        {
            isLock = true;
            st.TodAfter();
        }
        else if ((st.Stage == 3 && st.storyline == 14) && !isLock)
        {
            isLock = true;
            st.OriasAfter();
        }
        else if ((st.Stage == 4 && st.storyline == 32) && !isLock)
        {
            isLock = true;
            st.ValeporAfter();
        }
        else if ((st.Stage == 5 && st.storyline == 15) && !isLock)
        {
            isLock = true;
            st.KeroAfter();
        }
        else if ((st.Stage == 6 && st.storyline == 18) && !isLock)
        {
            isLock = true;
            st.LoimAfter();
        }
    }
}
