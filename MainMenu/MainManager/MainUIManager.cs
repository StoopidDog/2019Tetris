using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour {

    static MainUIManager _instance = null; //내용
    //싱글톤
    public static MainUIManager instance //명패
    {
        get //가져온다
        {
            if (_instance == null)
            {
                GameObject goManager = GameObject.Find("MainUIManager");

                if (goManager == null)
                {
                    goManager = new GameObject();
                    _instance = goManager.AddComponent<MainUIManager>();
                    return _instance;
                }

                _instance = goManager.GetComponent<MainUIManager>();

                if (_instance == null)
                {
                    _instance = goManager.AddComponent<MainUIManager>();
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

    private void Awake()
    {
        if (MainUIManager._instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this; //this = 나 자신, 변수 선언 상태에서 사용 불가

    }

    private void Start()
    {
        Logo.SetActive(true);
        OnePlayerMode.SetActive(false);
        TwoPlayerMode.SetActive(false);
        Exit.SetActive(false);
        Title.SetActive(true);
    }

    public GameObject Logo;
    public GameObject OnePlayerMode;
    public GameObject TwoPlayerMode;
    public GameObject Option;
    public GameObject Exit;
    public GameObject Title;

    public void DoStart()
    {
        SceneManager.LoadScene("StageSelect");
        //Option.SetActive(true);
    }
}
