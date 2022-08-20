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
    [SerializeField] Image healthFill;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;

    [SerializeField] List<MonsterBase> monsters;
    Monster currentMonster;
    AudioSource audioSource;

    bool isAlive = true;

    Vector3 originalScale;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        InitializeMonster();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleClick()
    {
        Damage(1);

        if (currentHealth <= 0 && isAlive)
        {
            StartCoroutine(HandleFaint());
        }
    }

    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= currentMonster.Base.MaxHP / 4)
        {
            healthFill.color = Color.red;
        }
        else if (currentHealth <= currentMonster.Base.MaxHP / 2)
        {
            healthFill.color = Color.yellow;
        }


    }

    void InitializeMonster()
    {
        isAlive = true;
        healthFill.color = Color.green;
        MonsterBase randomMonster = monsters[Random.Range(0, monsters.Count)];
        currentMonster = new Monster(randomMonster, GameManager.instance.GetLevelOfMonster());

        monsterSprite.sprite = currentMonster.Base.Sprite;
        nameText.text = currentMonster.Base.Name;
        levelText.text = "Lvl " + currentMonster.level;

        currentHealth = currentMonster.MaxHP;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;

        audioSource.PlayOneShot(currentMonster.Base.Cry);


    }

    void LoadNewMonster()
    {
        InitializeMonster();
    }

    IEnumerator HandleFaint()
    {
        isAlive = false;
        //shrink the monster over 1 second
        Vector3 originalScale = monsterSprite.GetComponent<RectTransform>().localScale;
        Vector3 newScale = new Vector3(0, 0, 0);
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            monsterSprite.GetComponent<RectTransform>().localScale = Vector3.Lerp(originalScale, newScale, t);
            yield return null;
        }
        PartyController.instance.GainExp(currentMonster.Base.ExpYield);
        GameManager.instance.IncrementStageProgress();

        LoadNewMonster();
        monsterSprite.GetComponent<RectTransform>().localScale = originalScale;
    }
}
