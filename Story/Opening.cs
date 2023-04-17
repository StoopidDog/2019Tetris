using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour {

    public GameObject sprite;
    public SpriteRenderer spRender;

    public GameObject spriteOne_one;

    Color c;
    [SerializeField] private Sprite[] sp = new Sprite[8];

    [SerializeField] private float StoryTime;
    [SerializeField] private float TextTime;
    public Text OpeningText; //UI 텍스트


    public string writeText; //텍스트 쓰기

    private void Start()
    {
        spRender = sprite.GetComponent<SpriteRenderer>();
        c = spRender.color;
        StartCoroutine(TextPractice());
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            StartGame();
        }
    }

    IEnumerator TextPractice()//코루틴 - 텍스트 출력 베이스
    {
        OpeningBGMManager BGM = OpeningBGMManager.instance;

        TextAsset textAsset = (TextAsset)Resources.Load("Story/Opening");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("Story");

        foreach (XmlNode node in nodes)
        {
            string line;
            for (int i = 0; i < 9; i++)
            {
                line = node.SelectSingleNode("Opening_"+i.ToString()).InnerText;
                c.a = 1;
                spRender.color = c;
                if(i == 1)
                {
                    BGM.Coroutine_START();
                }
                if(i == 2)
                {
                    BGM.Coroutine_STOP();
                    spriteOne_one.SetActive(false);
                    BGM.ChangeBGM_openingtwo();
                }
                if(i == 5)
                {
                    BGM.ChangeBGM_openingthree();
                }
                if(i == 6)
                {
                    BGM.ChangeBGM_openingfour();
                }
                if(i == 8)
                {
                    BGM.Coroutine_START();
                }
                if (i < 8)
                {
                    spRender.sprite = sp[i];
                }
                 yield return StartCoroutine(StoryTextWrite(line, i));
            }
            StartGame();
        }
    }

    IEnumerator StoryTextWrite(string Text, int _i) //내용
    {
        int i = 0;
        float a = 1;
        OpeningText.text = Text;
        writeText = ""; //기본값

        for (i = 0; i < Text.Length; i++) //i<텍스트 길이
        {
            writeText += Text[i];
            OpeningText.text = writeText;

            if (_i == 1 && i > Text.Length / 2)
            {
                a -= 0.1f;
                c.a = a;
                spRender.color = c;
            }
            if (_i == 8)
            {
                a -= 0.1f;
                c.a = a;
                spRender.color = c;
            }
            yield return new WaitForSeconds(TextTime);
        }

        yield return new WaitForSeconds(StoryTime);
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
