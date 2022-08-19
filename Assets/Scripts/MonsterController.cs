using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterController : MonoBehaviour
{
    float currentHealth = 5;

    [SerializeField] Image monsterSprite;

    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;

    [SerializeField] List<Monster> monsters;
    Monster currentMonster;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMonster();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleClick()
    {
        Damage(1);

        if (currentHealth <= 0)
        {
            LoadNewMonster();
        }
    }

    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
    }

    void InitializeMonster()
    {
        currentMonster = monsters[Random.Range(0, monsters.Count)];

        monsterSprite.sprite = currentMonster.sprite;
        nameText.text = currentMonster.name;
        levelText.text = "Lvl " + currentMonster.level;

        currentHealth = currentMonster.GetHealth();
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
    }

    void LoadNewMonster()
    {
        InitializeMonster();
    }
}
