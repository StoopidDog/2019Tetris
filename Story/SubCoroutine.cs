using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCoroutine : MonoBehaviour {

    public static SubCoroutine instance { get; set; }

    public bool isEnd = false;

    private void Awake()
    {
        instance = this;
    }

    public void StartAngelCorutineText(string text)
    {
        StartCoroutine(AngelText(text));
    }

    public void StartEnemyCorutineText(string text)
    {
        StartCoroutine(EnemyText(text));
    }

    IEnumerator AngelText(string _text)
    {
        var AngelText = UIManager.instance.PlayerText;

        AngelText.text = _text;
        yield return new WaitForSeconds(2f);
        isEnd = true;
    }

    IEnumerator EnemyText(string _text)
    {
        var EnemyText = UIManager.instance.EnemyText;

        EnemyText.text = _text;
        yield return new WaitForSeconds(2f);
        isEnd = true;
    }
}
