using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour {

    public static CharacterSpriteController instance { get; set; }

    [SerializeField] private Sprite[] sp;

    [SerializeField]SpriteRenderer spriteRe;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spriteRe = GetComponent<SpriteRenderer>();
    }

    public void Shadow()
    {
        spriteRe.sprite = sp[1];
    }

    public void Light()
    {
        spriteRe.sprite = sp[0];
    }
}
