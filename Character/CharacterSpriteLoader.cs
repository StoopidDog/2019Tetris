using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteLoader : MonoBehaviour {


    public int Base = 0;

    public SpriteRenderer Sp;
    public Sprite[] Character1P;
    public Sprite[] Character2P;

    // Use this for initialization
    void Start () {
        Sp = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Base == 1)
        {
            Sp.sprite = Character1P[GameManager.instance.Ch1P];
        }
        if (Base == 2)
        {
            Sp.sprite = Character2P[GameManager.instance.Ch2P];
        }
	}
}
