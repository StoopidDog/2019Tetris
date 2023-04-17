using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryReader : MonoBehaviour
{
    static StoryReader _instance = null; //내용
    public static StoryReader instance
    //명패
    {
        get //가져온다
        {
            if (_instance == null)
            {
                GameObject goManager = GameObject.Find("StoryReader");

                if (goManager == null)
                {
                    goManager = new GameObject();
                    _instance = goManager.AddComponent<StoryReader>();
                    return _instance;
                }

                _instance = goManager.GetComponent<StoryReader>();

                if (_instance == null)
                {
                    _instance = goManager.AddComponent<StoryReader>();
                }

                return _instance;
            }
            return _instance;
        }
        set //설정한다
        {
            _instance = value;
        }
    }
    
    public SpriteRenderer spr;

    public GameObject TextBox;
    public GameObject NameBox;

    public GameObject MirrorAnimJoker;
    public GameObject MirrorAnimHeart;

    public Text CharacterText; //UI 텍스트
    public Text CharacterName;

    public List<KeyCode> skipButton; //키 설정
    [SerializeField]bool isButtonClicked = false; //키 눌림 체크

    public string writeText; //텍스트 쓰기

    public int storyline = 0;

    public string PlayerName;
    public int Stage;

    public bool insideStory = false;
    public bool needStart = false;

    [SerializeField]bool LoadText = false;

    [SerializeField] List<string> StoryList;
    [SerializeField] List<string> CharacterList;

    private void Awake()
    {
        
        if (StoryReader._instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void name_enum()
    {
        switch(GameManager.instance.CHARACTER)
        {
            case PLAYER_CHARACTER.AHU:
                PlayerName = "Ahu";
                break;
            case PLAYER_CHARACTER.ADNACIEL:
                PlayerName = "Adnaciel";
                break;
            case PLAYER_CHARACTER.PARASIEL:
                PlayerName = "Parasiel";
                break;
            case PLAYER_CHARACTER.HANIEL:
                PlayerName = "Haniel";
                break;
            case PLAYER_CHARACTER.MATRIEL:
                PlayerName = "Matriel";
                break;
        }
    }

    private void Start()
    {
        GameManager.instance.GetSaveData().canLoad = true;
        spr = GetComponent<SpriteRenderer>();
        StoryList = new List<string>();
        name_enum();
        Stage = (GameManager.instance.GetSaveData().Stage - 1) * 3 + GameManager.instance.GetSaveData().Level;
        StartCoroutine(CharacterTextPractice());
        StartCoroutine(TextPractice(0, 100));
    }

    public void StartTextPracitce()
    {
        StopAllCoroutines();
        StartCoroutine(TextPractice(0, 100));
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5 && GameManager.instance.needLoad)
        {
            GameManager.instance.needLoad = false;
            StopAllCoroutines();
            StartCoroutine(TextPractice(0, 100));
        }
        if (Input.GetKeyDown(KeyCode.Return) && !Pause.instance.isPaused)
        {
                isButtonClicked = true; //버튼 눌렀음
        }
    }

    IEnumerator TextPractice(int _stroynum, int _endnum)//코루틴 - 텍스트 출력 베이스
    {
         TextAsset textAsset = (TextAsset)Resources.Load("Story/" + PlayerName + "_Story");
         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.LoadXml(textAsset.text);
         XmlNodeList nodes = xmlDoc.SelectNodes("Story");

        foreach (XmlNode node in nodes)
        {
            var AngelText = UIManager.instance.PlayerText;
            var EnemyText = UIManager.instance.EnemyText;

            string line;
            if (!LoadText)
            {
                for (int i = 0; i <= StoryList.Count; i++)
                {
                    if (node.SelectSingleNode(PlayerName + "_" + Stage.ToString() + "_" + i.ToString()) == null)
                    {
                        break;
                    }
                    StoryList.Add(node.SelectSingleNode(PlayerName + "_" + Stage.ToString() + "_" + i.ToString()).InnerText);
                }
                LoadText = true;
            }

            for(storyline = _stroynum; storyline <= StoryList.Count; storyline++)
            {
                if (StoryList[storyline] == "")
                {
                    break;
                }
                line = StoryList[storyline];
                if (Stage == 1)
                {
                    if (storyline == 14)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        storyline++;
                        GoToStage();
                        insideStory = true;
                        break;
                    }
                    else if (insideStory)
                    {
                        if (storyline == 16 || storyline == 20 || storyline == 22)
                        {
                            AngelText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                            EnemyText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        if (storyline == _endnum)
                        {
                            storyline++;
                            yield return new WaitForSeconds(3f);
                            AngelText.text = "";
                            EnemyText.text = "";
                            break;
                        }
                    }
                    else if(storyline == 34)
                    {
                        GameManager.instance.GetSaveData().Level++;
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        GetStage();
                        StageChange();
                        break;
                    }
                    else
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                }
                else if(Stage == 2)
                {
                    if (storyline == 12)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        isButtonClicked = false;
                        storyline++;
                        GoToStage();
                        insideStory = true;
                        break;
                    }
                    else if (insideStory)
                    {
                        if (storyline == 15)
                        {
                            AngelText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                            EnemyText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        if (storyline == _endnum)
                        {
                            storyline++;
                            yield return new WaitForSeconds(3f);
                            AngelText.text = "";
                            EnemyText.text = "";
                            break;
                        }
                    }
                    else if (storyline == 29)
                    {
                        GameManager.instance.GetSaveData().Level++;
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        GetStage();
                        StageChange();
                        break;
                    }
                    else
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                }
                else if (Stage == 3)
                {
                    if(storyline == 7)
                    {
                        SummonCharacter.instance.SummonOrias();
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                    else if (storyline == 10)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        isButtonClicked = false;
                        storyline++;
                        GoToStage();
                        insideStory = true;
                        break;
                    }
                    else if (insideStory)
                    {
                        if (storyline == 12)
                        {
                            AngelText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                            EnemyText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        if (storyline == _endnum)
                        {
                            storyline++;
                            yield return new WaitForSeconds(3f);
                            AngelText.text = "";
                            EnemyText.text = "";
                            break;
                        }
                    }
                    else if (storyline == 20)
                    {
                        Instantiate(MirrorAnimJoker, new Vector3(0, 2), Quaternion.identity);
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                    else if(storyline == 23)
                    {
                        SummonCharacter.instance.DestoryEnemy();
                    }
                    else if (storyline == 29)
                    {
                        GameManager.instance.GetSaveData().Stage++;
                        GameManager.instance.GetSaveData().Level = 1;
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        GetStage();
                        StageChange();
                        break;
                    }
                    else
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                }
                else if (Stage == 4)
                {
                    if (storyline == 5)
                    {
                        SummonCharacter.instance.SummonValepor();
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                    else if (storyline == 23)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        isButtonClicked = false;
                        storyline++;
                        GoToStage();
                        insideStory = true;
                        break;
                    }
                    else if (insideStory)
                    {
                        if (storyline == 24 || storyline == 25 || storyline == 26 || storyline == 29|| storyline == 31)
                        {
                            AngelText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                            EnemyText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        if (storyline == _endnum)
                        {
                            storyline++;
                            yield return new WaitForSeconds(3f);
                            AngelText.text = "";
                            EnemyText.text = "";
                            break;
                        }
                    }
                    else if (storyline == 33)
                    {
                        GameManager.instance.GetSaveData().Level++;
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        GetStage();
                        StageChange();
                        break;
                    }
                    else
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                }
                else if (Stage == 5)
                {
                    if (storyline == 6)
                    {
                        SummonCharacter.instance.SummonKero();
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                    else if (storyline == 11)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        isButtonClicked = false;
                        storyline++;
                        GoToStage();
                        insideStory = true;
                        break;
                    }
                    else if (insideStory)
                    {
                        if (storyline == 13)
                        {
                            AngelText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                            EnemyText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        if (storyline == _endnum)
                        {
                            storyline++;
                            yield return new WaitForSeconds(3f);
                            AngelText.text = "";
                            EnemyText.text = "";
                            break;
                        }
                    }
                    else if (storyline == 22)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        GameManager.instance.GetSaveData().Level++;
                        GetStage();
                        StageChange();
                        break;
                    }
                    else
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                }
                else if (Stage == 6)
                {
                    if (storyline == 14)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        isButtonClicked = false;
                        storyline++;
                        GoToStage();
                        insideStory = true;
                        break;
                    }
                    else if (insideStory)
                    {
                        if (storyline == 17)
                        {
                            AngelText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                            EnemyText.text = line;
                            yield return new WaitForSeconds(2f);
                        }
                        if (storyline == _endnum)
                        {
                            storyline++;
                            yield return new WaitForSeconds(3f);
                            AngelText.text = "";
                            EnemyText.text = "";
                            break;
                        }
                    }
                    else if (storyline == 19)
                    {
                        Instantiate(MirrorAnimHeart, new Vector3(0, 2), Quaternion.identity);
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                    else if (storyline == 32)
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                        GameManager.instance.GetSaveData().Stage = 1;
                        GameManager.instance.GetSaveData().Level = 1;
                        GetStage();
                        StageChange();
                        break;
                    }
                    else
                    {
                        CharacterNameText();
                        yield return StartCoroutine(StoryTextWrite(line));
                    }
                }
            }
            yield return null;
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
            if (!Pause.instance.isPaused)
            {
                if (isButtonClicked)
                {
                    isButtonClicked = false;
                    break;
                }
            }
            yield return null;
        }
    }

    void CharacterNameText()
    {
        switch (GameManager.instance.CHARACTER)
        {
            case PLAYER_CHARACTER.ADNACIEL:
                if (Stage == 1)
                {
                    if ((storyline == 3 || storyline == 6 || storyline == 8) || (storyline == 9) || (storyline == 10) ||
                        (storyline == 27 || storyline == 28) ||
                        (storyline == 29 || storyline == 31 || storyline == 32 || storyline == 33 || storyline == 34))
                    {
                        CharacterName.text = CharacterList[10];
                        CharacterSpriteController.instance.Shadow();
                        CharacterSpriteEnemy.instance.Light();
                    }
                    else
                    {
                        CharacterName.text = CharacterList[1];
                        CharacterSpriteController.instance.Light();
                        CharacterSpriteEnemy.instance.Shadow();
                    }
                }
                if(Stage == 2)
                {
                    if (storyline == 3 || storyline == 7 || storyline == 11)
                    {
                        CharacterName.text = CharacterList[12];
                        CharacterSpriteController.instance.Shadow();
                        CharacterSpriteEnemy.instance.Light();
                    }
                    else if(storyline == 5 || storyline == 20 || storyline == 25 || storyline == 27 || storyline == 29)
                    {
                        CharacterName.text = CharacterList[21];
                        CharacterSpriteController.instance.Shadow();
                        CharacterSpriteEnemy.instance.Light();
                    }
                    else
                    {
                        CharacterName.text = CharacterList[1];
                        CharacterSpriteController.instance.Light();
                        CharacterSpriteEnemy.instance.Shadow();
                    }
                }
                if (Stage == 3)
                {
                    if (storyline > 6 && storyline < 22)
                    {
                        if (storyline == 8 || storyline == 10 || storyline == 14 || storyline == 17 || storyline == 19 || storyline == 20 || storyline == 21 || storyline == 22)
                        {
                            CharacterName.text = CharacterList[9];
                            CharacterSpriteController.instance.Shadow();
                            CharacterSpriteEnemy.instance.Light();
                        }
                        else
                        {
                            CharacterName.text = CharacterList[1];
                            CharacterSpriteController.instance.Light();
                            CharacterSpriteEnemy.instance.Shadow();
                        }
                    }
                    else
                    {
                        CharacterSpriteController.instance.Light();
                        CharacterName.text = CharacterList[1];
                    }
                }
                if (Stage == 4)
                {
                    if (storyline >= 5 && storyline <= 31)
                    {
                        if (storyline == 5 || storyline == 6 || storyline == 8 || storyline == 10 || storyline == 12 || storyline == 14 || storyline == 17 || storyline == 18
                            || storyline == 20 || storyline == 21 || storyline == 22 || storyline == 32)
                        {
                            CharacterName.text = CharacterList[13];
                            CharacterSpriteController.instance.Shadow();
                            CharacterSpriteEnemy.instance.Light();
                        }
                        else if(storyline == 4)
                        {
                            CharacterName.text = CharacterList[22];
                        }
                        else
                        {
                            CharacterName.text = CharacterList[1];
                            CharacterSpriteController.instance.Light();
                            CharacterSpriteEnemy.instance.Shadow();
                        }
                    }
                    else if(storyline == 32)
                    {
                        CharacterName.text = CharacterList[13];
                    }
                    else
                    {
                        CharacterSpriteController.instance.Light();
                        CharacterName.text = CharacterList[1];
                    }
                }
                if (Stage == 5)
                {
                    if (storyline >= 6 && storyline <= 22)
                    {
                        if (storyline == 6 || storyline == 8 || storyline == 10 || storyline == 15 ||storyline == 16 || storyline == 17 || storyline == 18
                            || storyline == 20)
                        {
                            CharacterName.text = CharacterList[14];
                            CharacterSpriteController.instance.Shadow();
                            CharacterSpriteEnemy.instance.Light();
                        }
                        else if (storyline == 4)
                        {
                            CharacterName.text = CharacterList[22];
                        }
                        else
                        {
                            CharacterName.text = CharacterList[1];
                            CharacterSpriteController.instance.Light();
                            CharacterSpriteEnemy.instance.Shadow();
                        }
                    }
                    else if(storyline == 2 || storyline == 4)
                    {
                        CharacterName.text = CharacterList[22];
                        CharacterSpriteController.instance.Shadow();
                    }
                    else
                    {
                        CharacterSpriteController.instance.Light();
                        CharacterName.text = CharacterList[1];
                    }
                }
                if (Stage == 6)
                {
                    if (storyline >= 0)
                    {
                        if (storyline == 0 || storyline == 2 || storyline == 3 || (storyline >= 6 && storyline <= 14) || storyline == 18 ||
                            storyline == 20 || storyline == 21 || storyline == 23 || storyline == 26 ||storyline == 27 || storyline == 29)
                        {
                            CharacterName.text = CharacterList[6];
                            CharacterSpriteController.instance.Shadow();
                            CharacterSpriteEnemy.instance.Light();
                        }
                        else if (storyline == 24)
                        {
                            CharacterName.text = CharacterList[13];
                            CharacterSpriteController.instance.Shadow();
                        }
                        else if (storyline == 25)
                        {
                            CharacterName.text = CharacterList[14];
                            CharacterSpriteController.instance.Shadow();
                        }
                        else
                        {
                            CharacterName.text = CharacterList[1];
                            CharacterSpriteController.instance.Light();
                            CharacterSpriteEnemy.instance.Shadow();
                        }
                    }
                    else
                    {
                        CharacterSpriteController.instance.Light();
                        CharacterName.text = CharacterList[1];
                    }
                }
                break;
        }
    }

    void GoToStage()
    {
        switch (Stage)
        {
            case 1:
            case 2:
            case 3:
                NameBox.SetActive(false);
                TextBox.SetActive(false);
                LoadingSceneManager.LoadScene("FirstStage");
                break;
            case 4:
            case 5:
            case 6:
                NameBox.SetActive(false);
                TextBox.SetActive(false);
                LoadingSceneManager.LoadScene("SecondStage");
                break;
        }
    }

    public void GoToStory()
    {
        NameBox.SetActive(true);
        TextBox.SetActive(true);
    }

    IEnumerator CharacterTextPractice()//코루틴 - 텍스트 출력 베이스
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Story/Character_name");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("Character_name");


        foreach (XmlNode node in nodes)
        {
            for (int i = 0; i < 63; i++)
            {
                if ((i >= 0 && i < 30) || (i > 34 && i < 40) || (i > 44 && i < 50))
                {
                    continue;
                }
                if (node.SelectSingleNode("Character_name_" + i.ToString()) == null)
                {
                    break;
                }
                CharacterList.Add(node.SelectSingleNode("Character_name_" + i.ToString()).InnerText);
            }
            yield return null;
        }
    }

    public void MobText()
    {
        var LoadSave = GameManager.instance.GetSaveData();
        var Mob = MobManager.instance;
            if (LoadSave.Stage == 1 && LoadSave.Level == 1)
            {
                if (Mob.MobHp < Mob.MobHpMax / 2)
                {
                    if (Mob.MobHp <= 0.1)
                    {
                        StopAllCoroutines();
                        StartCoroutine(ButtonCoroutineAnta());
                    }
                    else
                    {
                        StopAllCoroutines();
                        StartCoroutine(TextPractice(15, 20));
                    }
                }
            }
            else if (LoadSave.Stage == 1 && LoadSave.Level == 2)
            {
                if (Mob.MobHp <= Mob.MobHpMax)
                {
                    StopAllCoroutines();
                    StartCoroutine(TextPractice(13, 18));
                }
            }
            else if (LoadSave.Stage == 1 && LoadSave.Level == 3)
            {
                StopAllCoroutines();
                StartCoroutine(TextPractice(11, 13));
            }
            else if(LoadSave.Stage == 2 && LoadSave.Level == 1)
            {
                StopAllCoroutines();
                StartCoroutine(ValeporButton());
            }
            else if (LoadSave.Stage == 2 && LoadSave.Level == 2)
            {
                 StopAllCoroutines();
                 StartCoroutine(TextPractice(12, 14));
            }
        else if (LoadSave.Stage == 2 && LoadSave.Level == 3)
        {
            StopAllCoroutines();
            StartCoroutine(TextPractice(15, 17));
        }
    }

    public void AntaAfter()
    {
        StartCoroutine(TextPractice(24, 34));
    }

    public void TodAfter()
    {
        StartCoroutine(TextPractice(19, 29));
    }

    public void OriasAfter()
    {
        StartCoroutine(TextPractice(14, 29));
    }

    public void ValeporAfter()
    {
        StartCoroutine(TextPractice(32, 33));
    }

    public void KeroAfter()
    {
        StartCoroutine(TextPractice(15, 22));
    }

    public void LoimAfter()
    {
        StartCoroutine(TextPractice(18, 31));
    }

    public void StageChange()
    {
        storyline = 0;
        CharacterName.text = "";
        CharacterText.text = "";
        NameBox.SetActive(false);
        TextBox.SetActive(false);
        StoryList.Clear();
        LoadText = false;
        needStart = true;
        LoadingSceneManager.LoadScene("Story");
    }

    public void StageEnd()
    {
        storyline = 0;
        CharacterName.text = "";
        CharacterText.text = "";
        NameBox.SetActive(false);
        TextBox.SetActive(false);
        StoryList.Clear();
        LoadText = false;
        needStart = true;
        LoadingSceneManager.LoadScene("MainMenu");
    }

    IEnumerator ButtonCoroutineAnta()
    {
        yield return StartCoroutine(TextPractice(21, 23));
        Mirror mirror = GameObject.FindGameObjectWithTag("Mirror").GetComponent<Mirror>();
        mirror.ChangeSpriteAnta();
        StoryGameManager.instance.MakeButton();
        yield return null;
    }

    public IEnumerator ValeporButton()
    {
        yield return StartCoroutine(TextPractice(24, 26));
        yield return null;
    }

    public IEnumerator ValeporDone()
    {
        yield return StartCoroutine(TextPractice(27, 31));
        Mirror mirror = GameObject.FindGameObjectWithTag("Mirror").GetComponent<Mirror>();
        mirror.ChangeSpriteValepor();
        StoryGameManager.instance.MakeButton();
    }

    void GetStage()
    {
        Stage = (GameManager.instance.GetSaveData().Stage - 1) * 3 + GameManager.instance.GetSaveData().Level;
    }
}
