using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMemberUI : MonoBehaviour
{
    public TMP_Text levelText;

    Monster currentMonster;

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
        currentMonster = monster;
        GetComponent<Image>().sprite = currentMonster.Base.Icon;
        levelText.text = "Lvl: " + currentMonster.Level;

    }

    public void HandleClick()
    {
        if (currentMonster != null)
        {
            Debug.Log("Clicked on " + currentMonster.Base.Name);
            currentMonster = PartyController.instance.SendOutActiveMonster(currentMonster);
            SetMonster(currentMonster);
            Debug.Log("It is now a " + currentMonster.Base.Name);
        }

    }
}
