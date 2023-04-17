using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

    public Text CharacterText; //UI 텍스트
    public Text CharacterName;

    public List<KeyCode> skipButton; //키 설정
    bool isButtonClicked = false; //키 눌림 체크

    public string writeText; //텍스트 쓰기

    //[SerializeField]
    //private int LineCount = 1;

    private void Start()
    {
        StartCoroutine(TextPractice());
        //LoadXml(1);
    }

    private void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true; //버튼 눌렀음
            }
        }
    }

    void LoadXml(int _i) //더미코드
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Story/Character_name");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("Character_name");

        foreach (XmlNode node in nodes)
        {
            for (int i = 0; i < 61; i++)
            {
                if ((i >= 0 && i < 30) || (i > 34 && i < 40) || (i > 44 && i < 50))
                {
                    continue;
                }
                Debug.Log(node.SelectSingleNode("Character_name_" + i.ToString()).InnerText);
            }
        }
    }

    IEnumerator TextPractice()//코루틴 - 텍스트 출력 베이스
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Story/Character_name");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("Character_name");

        foreach (XmlNode node in nodes)
        {
            string line;
            for (int i = 0; i < 61; i++)
            {
                if ((i >= 0 && i < 30) || (i > 34 && i < 40) || (i > 44 && i < 50))
                {
                    continue;
                }
                line = node.SelectSingleNode("Character_name_" + i.ToString()).InnerText; //Character_name_30
                yield return StartCoroutine(StoryTextWrite(line));
            }
        }
    }

    IEnumerator StoryTextWrite(string Text) //내용
    {
        int i = 0;
        CharacterText.text = Text;
        writeText = ""; //기본값

        for (i = 0; i < Text.Length; i++) //i<텍스트 길이
        {
            writeText += Text[i];
            CharacterText.text = writeText;
            yield return null;
        }

        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }
}
