using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMove : MonoBehaviour
{
    public Canvas C;
    public Image Alpha;
    public GameObject Card;
    bool moveDone = false;
    bool doAction = true;
    int counter = 0;
    public int CardPosition = 0;
    public int CharacterNum = 0;

    public float CardMoveSpeed = 0.0125f;
    public float AlphaSpeed = 0.0125f;

    Coroutine FaidIn;
    Coroutine FaidOut;

	// Update is called once per frame
	void Update () 
    {

        if(Input.GetKeyDown(KeyCode.A) && doAction)
        {
            moveDone = false;
            doAction = false;
            StartCoroutine(CardMovingAnim(CardPosition));
        }
        if (Input.GetKeyDown(KeyCode.D) && doAction)
        {
            moveDone = false;
            doAction = false;
            StartCoroutine(CardMovingRightAnim(CardPosition));
        }
        if (C.sortingOrder == 0)
        {
            GameManager.instance.Ch1P = CharacterNum;
        }
    }

    IEnumerator CardMovingAnim(int _card)
    {
        switch(_card)
        {
            case 0: //2
                //50 -50 -> 115 -27
            C.sortingOrder = -1;
            FaidOut = StartCoroutine(FadeOutEffect());
            //transform.position = new Vector3(414f, 233f, 0f); //캔버스 기반 위치 초기화
            while (!moveDone)
            {
                transform.position += new Vector3(1.625f, 0.575f, 0f);
                Card.transform.rotation *= Quaternion.Euler(0, 0, 0.5f);
                counter++;

                
                if (counter == 40)
                {
                    //transform.position += new Vector3(0f, 0f, -1f);
                    CardPosition = -1;
                    counter = 0;
                    moveDone = true;
                    doAction = true;
                    StopCoroutine(FaidOut);
                }
                yield return new WaitForSeconds(CardMoveSpeed);
            }
            yield return null;
                break;

            case -1: //1
                //transform.position = new Vector3(294f, 273f, -1f); //캔버스 기반 위치 초기화
            C.sortingOrder = -2;
            while (!moveDone)
            {
                transform.position += new Vector3(2.5f, 0f, 0f);
                counter++;


                if (counter == 40)
                {
                    //transform.position += new Vector3(0f, 0f, 2f);
                    CardPosition = 1;
                    counter = 0;
                    moveDone = true;
                    doAction = true;
                }
                yield return new WaitForSeconds(CardMoveSpeed);
            }
            yield return null;
                break;

            case 1: //3
                //transform.position = new Vector3(534f, 273f, 1f); //캔버스 기반 위치 초기화
            C.sortingOrder = 0;
            FaidIn = StartCoroutine(FadeInEffect());
            while (!moveDone)
            {
                transform.position += new Vector3(-1.25f, -0.5f, 0f);
                counter++;


                if (counter == 40)
                {
                    //transform.position += new Vector3(0f, 0f, -1f);
                    CardPosition = 0;
                    counter = 0;
                    moveDone = true;
                    doAction = true;
                    StopCoroutine(FaidIn);
                }
                yield return new WaitForSeconds(CardMoveSpeed);
            }
            yield return null;
                break;
            default:
                yield return null;
                break;
        }
    }

    IEnumerator CardMovingRightAnim(int _card)
    {
        switch (_card)
        {
            case 0:
                C.sortingOrder = -1;
                FaidOut = StartCoroutine(FadeOutEffect());
                //transform.position = new Vector3(414f, 233f, 0f); //캔버스 기반 위치 초기화
                while (!moveDone)
                {
                    transform.position += new Vector3(1.25f, 0.5f, 0f);
                    counter++;


                    if (counter == 40)
                    {
                        //transform.position += new Vector3(0f, 0f, -1f);
                        CardPosition = 1;
                        counter = 0;
                        moveDone = true;
                        doAction = true;
                        StopCoroutine(FaidOut);
                    }
                    yield return new WaitForSeconds(CardMoveSpeed);
                }
                yield return null;
                break;

            case -1:
                //transform.position = new Vector3(294f, 273f, -1f); //캔버스 기반 위치 초기화
                C.sortingOrder = 0;
                FaidIn = StartCoroutine(FadeInEffect());
                while (!moveDone)
                {
                    transform.position += new Vector3(1.25f, -0.5f, 0f);
                    counter++;


                    if (counter == 40)
                    {
                        //transform.position += new Vector3(0f, 0f, 2f);
                        CardPosition = 0;
                        counter = 0;
                        moveDone = true;
                        doAction = true;
                        StopCoroutine(FaidIn);
                    }
                    yield return new WaitForSeconds(CardMoveSpeed);
                }
                yield return null;
                break;

            case 1:
                //transform.position = new Vector3(534f, 273f, 1f); //캔버스 기반 위치 초기화
                C.sortingOrder = -2;
                while (!moveDone)
                {
                    transform.position += new Vector3(-2.5f, 0f, 0f);
                    counter++;


                    if (counter == 40)
                    {
                        //transform.position += new Vector3(0f, 0f, -1f);
                        CardPosition = -1;
                        counter = 0;
                        moveDone = true;
                        doAction = true;
                    }
                    yield return new WaitForSeconds(CardMoveSpeed);
                }
                yield return null;
                break;
            default:
                yield return null;
                break;
        }
    }

    IEnumerator FadeInEffect()
    {
        while (Alpha.color.a >= 0)//밝아지기
        {
            Alpha.color += new Color(0, 0, 0, -0.0125f);
            yield return new WaitForSeconds(AlphaSpeed);
        }
        yield return null;
    }


    IEnumerator FadeOutEffect()
    {
        while (Alpha.color.a < 0.5)//어두워지기
        {
            Alpha.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(AlphaSpeed);
        }
        yield return null;
    }
}
