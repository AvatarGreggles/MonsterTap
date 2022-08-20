using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMemberUI : MonoBehaviour
{
    public TMP_Text levelText;

    public Monster currentMonster;

    Image monsterSprite;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMonster(Monster monster)
    {
        monsterSprite = GetComponent<Image>();

        currentMonster = monster;
        Debug.Log(monsterSprite);

        monsterSprite.sprite = currentMonster.Base.Icon;
        levelText.text = "Lvl: " + currentMonster.Level;

        if (monster.HP <= 0)
        {
            monsterSprite.color = Color.gray;
        }
        else
        {
            monsterSprite.color = Color.white;
        }

    }

    public void HandleClick()
    {
        if (currentMonster != null && currentMonster.HP > 0)
        {
            Debug.Log("Clicked on " + currentMonster.Base.Name);
            currentMonster = PartyController.instance.SendOutActiveMonster(currentMonster);
            SetMonster(currentMonster);
            Debug.Log("It is now a " + currentMonster.Base.Name);
        }

    }
}
