using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour {

    public static StageUIManager instance { get; set; }

    private void Awake()
    {
        instance = this;
    }

    public bool HaveSaveFile = false;

    public GameObject TrueLoad;
    public Button loadbutton;
    public ColorBlock theColor;
    public GameObject Option;

    // Use this for initialization
    void Start () {
        HaveSaveFile = GameManager.instance.GetSaveData().canLoad;

        if(!HaveSaveFile)
        {
            theColor.normalColor = new Color32(211, 211, 211, 255);
            theColor.highlightedColor = new Color32(211, 211, 211, 255);
            theColor.pressedColor = new Color32(211, 211, 211, 255);
            theColor.disabledColor = new Color32(211, 211, 211, 255);
            theColor.colorMultiplier = 1;

            loadbutton.colors = theColor;
        }
    }
}
