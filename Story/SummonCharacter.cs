using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCharacter : MonoBehaviour {

    public static SummonCharacter instance { get; set; }

    public GameObject[] Player;
    public GameObject[] Enemy;

	// Use this for initialization
	void Awake () {
        instance = this;

		if(GameManager.instance.CHARACTER == PLAYER_CHARACTER.ADNACIEL)
        {
            Instantiate(Player[1], new Vector3(-5f, -0.6f, 0f), Quaternion.identity);
        }
        if(GameManager.instance.GetSaveData().Level == 1 && GameManager.instance.GetSaveData().Stage == 1)
        {
            Instantiate(Enemy[0], new Vector3(5.5f, -1.3f, 0f), Quaternion.identity);
        }
        if(GameManager.instance.GetSaveData().Level == 2 && GameManager.instance.GetSaveData().Stage == 1)
        {
            Instantiate(Enemy[1], new Vector3(4.8f, 0f, 0f), Quaternion.identity);
        }
        if (GameManager.instance.GetSaveData().Level == 3 && GameManager.instance.GetSaveData().Stage == 2)
        {
            Instantiate(Enemy[5], new Vector3(5.3f, -0.3f, 0f), Quaternion.identity);
        }
    }

    private void Start()
    {
        if (GameManager.instance.GetSaveData().Level == 3 && GameManager.instance.GetSaveData().Stage == 1 && (StoryReader.instance.storyline >= 14 && StoryReader.instance.storyline <= 23))
        {
            Instantiate(Enemy[2], new Vector3(6f, -1f, 0f), Quaternion.identity);
        }
        if (GameManager.instance.GetSaveData().Level == 1 && GameManager.instance.GetSaveData().Stage == 2 && (StoryReader.instance.storyline >= 30))
        {
            Instantiate(Enemy[3], new Vector3(6f, -1f, 0f), Quaternion.identity);
        }
        if (GameManager.instance.GetSaveData().Level == 2 && GameManager.instance.GetSaveData().Stage == 2 && (StoryReader.instance.storyline >= 15))
        {
            Instantiate(Enemy[4], new Vector3(6f, -1f, 0f), Quaternion.identity);
        }
    }

    public void SummonOrias()
    {
        Instantiate(Enemy[2], new Vector3(6f, -1f, 0f), Quaternion.identity);
    }

    public void SummonValepor()
    {
        Instantiate(Enemy[3], new Vector3(6f, -1f, 0f), Quaternion.identity);
    }

    public void SummonKero()
    {
        Instantiate(Enemy[4], new Vector3(6f, -1f, 0f), Quaternion.identity);
    }

    public void DestoryEnemy()
    {
        GameObject gameobject;
        gameobject = GameObject.FindGameObjectWithTag("StoryEnemy");
        Destroy(gameobject);
    }

}
