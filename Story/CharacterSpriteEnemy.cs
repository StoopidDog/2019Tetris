using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteEnemy : MonoBehaviour {

    public static CharacterSpriteEnemy instance { get; set; }

    [SerializeField] private Sprite[] sp;
    [SerializeField] Sprite[] spbaby;

    [SerializeField]SpriteRenderer spriteRe;
    [SerializeField]SpriteRenderer[] spriteBaby;

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
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                spriteBaby[i].sprite = spbaby[1];
            }
        }
    }

    public void Light()
    {
        spriteRe.sprite = sp[0];
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                spriteBaby[i].sprite = spbaby[0];
            }
        }
    }
}
