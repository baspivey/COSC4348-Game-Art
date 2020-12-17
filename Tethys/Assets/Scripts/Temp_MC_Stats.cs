using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Temp_MC_Stats : MonoBehaviour
{
    //Temp stats to work on scripts
    public bool IsDead { get; protected set; }

    public GameObject GameOverUI;

    public float curHearts;
    public float curAtkPwr;
    public int potionCount;
    public float MaxHearts = 100f;
    public float attackPwr = 1f;
    public float damageFromEnemy = 0.1f;

    public HealthBar healthbar;



    public int curGold = 0;

    private void Start()
    {
        curHearts = MaxHearts;
        curAtkPwr = attackPwr;
        healthbar.SetMaxHealth(MaxHearts);
        healthbar.SetHealth(curHearts);
        IsDead = false;
    }

    private void Update()
    {
        healthbar.SetHealth(curHearts);
        if (curHearts > MaxHearts)
        {
            curHearts = MaxHearts;
        }
        else if (curHearts <= 0)
        {
            IsDead = true;
        }
    }

    public void DecreaseHealth() {
        if (curHearts > 0) {
            FindObjectOfType<SoundManager>().PlaySound("MCGrunt1");
            curHearts -= damageFromEnemy;
        }

        if (curHearts <= 0)
        {
            FindObjectOfType<SoundManager>().PlaySound("MCDeath1");
            GameOverUI.SetActive(true);
            IsDead = true;
            var isoRenderer = GetComponent<IsoCharacterRenderer>();
            isoRenderer.SetDirection(new Vector2(0, 0));
        }
    }
}
