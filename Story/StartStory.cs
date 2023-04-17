using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(StoryReader.instance.needStart)
        {
            StoryReader.instance.StartTextPracitce();
            StoryReader.instance.needStart = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
