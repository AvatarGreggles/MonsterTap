using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterController : MonoBehaviour
{
    public static MonsterController instance;
    float currentHealth = 5;

    [SerializeField] Image monsterSprite;

    [SerializeField] Slider healthBar;
    [SerializeField] Image healthFill;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;

    float timer = 0;

    [SerializeField] List<MonsterBase> monsters;
    Monster currentMonster;
    public MonsterBase baseMonster;
    AudioSource audioSource;

    bool isAlive = true;

    Vector3 originalScale;

    Button clickableArea;
    [SerializeField] Button captureButton;

    private void Awake()
    {
        instance = this;
        clickableArea = GetComponent<Button>();
    }

    public float timeBetweenAttacks = 5;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        InitializeMonster();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && PartyController.instance.currentMonster.HP > 0)
            {
                timer = 0;
                PartyController.instance.DamageActiveMonster(currentMonster.Base.Attack);
            }
        }

    }

    public void HandleClick()
    {
        if (PartyController.instance.currentMonster.HP > 0)
        {
            Damage(PartyController.instance.currentMonster.GetStat(Stat.Attack));
        }
        else
        {
            Debug.Log("Send out a new Mon");
        }
    }

    void Damage(int damage)
    {
        Debug.Log("Damage: " + damage);
        Debug.Log("health before: " + currentHealth);
        currentHealth -= damage;
        Debug.Log("health after: " + currentHealth);
        healthBar.value = currentHealth;

        Debug.Log("Max: " + currentMonster.MaxHP);
        Debug.Log("Max / 4: " + currentMonster.MaxHP / 4);

        if (currentHealth <= currentMonster.MaxHP / 4)
        {
            healthFill.color = Color.red;
            CaptureController.instance.ToggleActiveButton(true);
        }
        else if (currentHealth <= currentMonster.MaxHP / 2 && currentHealth > currentMonster.MaxHP / 4)
        {
            healthFill.color = Color.yellow;
        }

        if (currentHealth <= 0 && isAlive)
        {
            CaptureController.instance.ToggleActiveButton(false);
            StartCoroutine(HandleFaint());
        }


    }

    void InitializeMonster()
    {
        isAlive = true;
        CaptureController.instance.ToggleActiveButton(false);
        healthFill.color = Color.green;
        baseMonster = monsters[Random.Range(0, monsters.Count)];
        currentMonster = new Monster(baseMonster, GameManager.instance.GetLevelOfMonster());

        monsterSprite.sprite = currentMonster.Base.Sprite;
        nameText.text = currentMonster.Base.Name;
        levelText.text = "Lvl " + currentMonster.level;

        Debug.Log("log " + currentMonster.MaxHP);
        currentHealth = currentMonster.MaxHP;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;

        audioSource.PlayOneShot(currentMonster.Base.Cry);
        timeBetweenAttacks = GameManager.instance.GetBaseAttackSpeed(currentMonster.Base.Speed);


    }

    public void NextMonster()
    {
        GameManager.instance.IncrementStageProgress();
        InitializeMonster();
    }

    IEnumerator HandleFaint()
    {
        isAlive = false;
        clickableArea.interactable = false;
        captureButton.interactable = false;
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
        PartyController.instance.GainExp(currentMonster.Base.ExpYield * (currentMonster.level / 2));

        NextMonster();
        monsterSprite.GetComponent<RectTransform>().localScale = originalScale;
        clickableArea.interactable = true;
        captureButton.interactable = true;
    }

    public IEnumerator HandleCaptureAnimation()
    {
        clickableArea.interactable = false;
        captureButton.interactable = false;
        Vector3 originalScale = monsterSprite.GetComponent<RectTransform>().localScale;
        Vector3 newScale = new Vector3(0, 0, 0);
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            monsterSprite.GetComponent<RectTransform>().localScale = Vector3.Lerp(originalScale, newScale, t);
            yield return null;
        }
        PartyController.instance.GainExp(currentMonster.Base.ExpYield * (currentMonster.level / 2));

        NextMonster();
        monsterSprite.GetComponent<RectTransform>().localScale = originalScale;
        clickableArea.interactable = true;
        captureButton.interactable = true;
    }
}
