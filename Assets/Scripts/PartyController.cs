using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyController : MonoBehaviour
{

    public static PartyController instance;

    [SerializeField] List<MonsterBase> party;
    public Monster currentMonster;
    public Monster activeMonster;
    int currentMonsterIndex = 0;
    [SerializeField] Image monsterIcon;
    [SerializeField] Image type1Sprite;
    [SerializeField] Image type2Sprite;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] Slider expBar;

    [SerializeField] GameObject partyPanel;
    [SerializeField] GameObject attackPanel;

    TMP_Text type1Text;
    TMP_Text type2Text;

    [SerializeField] List<AttackController> attackControllers;

    [SerializeField] List<GameObject> partyMembersUI;

    private void Awake()
    {
        instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {

        InitializeParty();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeParty()
    {
        currentMonster = new Monster(party[currentMonsterIndex], 2);
        for (var i = 1; i < party.Count; i++)
        {
            Monster otherMonster = new Monster(party[i], 2);
            partyMembersUI[i - 1].GetComponent<PartyMemberUI>().SetMonster(otherMonster);
        }
        monsterIcon.sprite = currentMonster.Base.Icon;
        nameText.text = currentMonster.Base.Name;
        levelText.text = "Lvl " + currentMonster.level;
        type1Text = type1Sprite.GetComponentInChildren<TMP_Text>();
        type2Text = type2Sprite.GetComponentInChildren<TMP_Text>();

        Color type1Color = GetTypeColor(currentMonster.Base.MonsterType1);
        Color type2Color = GetTypeColor(currentMonster.Base.MonsterType2);

        Debug.Log(type1Color);
        Debug.Log(type2Color);

        type1Sprite.color = type1Color;
        type2Sprite.color = type2Color;
        type1Text.text = currentMonster.Base.MonsterType1.ToString();
        type2Text.text = currentMonster.Base.MonsterType2.ToString();


        expBar.maxValue = currentMonster.Base.GetExpForLevel(currentMonster.level);
        expBar.value = currentMonster.Exp;

        for (int i = 0; i < currentMonster.Attacks.Count; i++)
        {
            attackControllers[i].SetAttack(currentMonster.Attacks[i]);
        }

        partyPanel.SetActive(false);
        attackPanel.SetActive(true);
    }

    public Monster SendOutActiveMonster(Monster chosenMonster)
    {
        activeMonster = currentMonster;
        currentMonster = chosenMonster;
        SetAttacks(currentMonster);

        monsterIcon.sprite = currentMonster.Base.Icon;
        nameText.text = currentMonster.Base.Name;
        levelText.text = "Lvl " + currentMonster.level;
        type1Text = type1Sprite.GetComponentInChildren<TMP_Text>();
        type2Text = type2Sprite.GetComponentInChildren<TMP_Text>();

        Color type1Color = GetTypeColor(currentMonster.Base.MonsterType1);
        Color type2Color = GetTypeColor(currentMonster.Base.MonsterType2);

        type1Sprite.color = type1Color;
        type2Sprite.color = type2Color;
        type1Text.text = currentMonster.Base.MonsterType1.ToString();
        type2Text.text = currentMonster.Base.MonsterType2.ToString();


        expBar.maxValue = currentMonster.Base.GetExpForLevel(currentMonster.level);
        expBar.value = currentMonster.Exp;

        TogglePartyPanel();

        return activeMonster;
    }

    public void GainExp(int amount)
    {
        currentMonster.Exp += amount;
        expBar.value = currentMonster.Exp;

        if (currentMonster.Exp >= currentMonster.Base.GetExpForLevel(currentMonster.level))
        {
            GainLevel();
        }
    }

    public void SetAttacks(Monster monster)
    {

        for (int i = 0; i < attackControllers.Count; i++)
        {
            if (i < monster.Attacks.Count)
            {
                Debug.Log("Setting" + monster.Attacks[i] + monster.Base.Name);
                attackControllers[i].SetAttack(monster.Attacks[i]);
            }
            else
            {
                attackControllers[i].ClearAttack();
            }
        }
    }

    Color GetTypeColor(MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Bug:
                return new Color(131, 33, 163);
            case MonsterType.Dark:
                return new Color(131, 33, 163);
            case MonsterType.Dragon:
                return new Color(0.2f, 0.4f, 0.6f);
            case MonsterType.Electric:
                return new Color(131, 33, 163);
            case MonsterType.Fairy:
                return new Color(131, 33, 163);
            case MonsterType.Fighting:
                return new Color(131, 33, 163);
            case MonsterType.Fire:
                return new Color(209, 53, 36);
            case MonsterType.Flying:
                return new Color(0.44706f, 0.86667f, 0.96863f);
            case MonsterType.Ghost:
                return new Color(131, 33, 163);
            case MonsterType.Grass:
                return new Color(0.16471f, 0.61176f, 0.13333f);
            case MonsterType.Ground:
                return new Color(131, 33, 163);
            case MonsterType.Ice:
                return new Color(131, 33, 163);
            case MonsterType.Normal:
                return new Color(0.85490f, 0.89020f, 0.85490f);
            case MonsterType.Poison:
                return new Color(0.51373f, 0.12941f, 0.63922f);
            case MonsterType.Psychic:
                return new Color(131, 33, 163);
            case MonsterType.Rock:
                return new Color(131, 33, 163);
            case MonsterType.Steel:
                return new Color(131, 33, 163);
            case MonsterType.Water:
                return new Color(131, 33, 163);
            default:
                return Color.black;
        }
    }

    void GainLevel()
    {
        int difference = currentMonster.Exp - currentMonster.Base.GetExpForLevel(currentMonster.level);
        currentMonster.Exp = difference;
        expBar.value = currentMonster.Exp;
        currentMonster.level++;
        levelText.text = "Lvl " + currentMonster.level;
        expBar.maxValue = currentMonster.Base.GetExpForLevel(currentMonster.level);

        // This teaches the first four possible moves
        foreach (var attack in currentMonster.Base.LearnableAttacks)
        {
            if (attack.Level == currentMonster.level)
            {
                currentMonster.Attacks.Add(attack.Base);
            }
        }

        for (int i = 0; i < currentMonster.Attacks.Count; i++)
        {
            attackControllers[i].SetAttack(currentMonster.Attacks[i]);
        }
    }

    public void TogglePartyPanel()
    {
        partyPanel.SetActive(!partyPanel.activeSelf);
        attackPanel.SetActive(!attackPanel.activeSelf);
    }
}
