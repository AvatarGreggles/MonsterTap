using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackController : MonoBehaviour
{
    Attack attack;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Image iconType;
    [SerializeField] TMP_Text nameText;

    public void HandleClick()
    {
        if (attack != null)
        {
            Debug.Log(attack.name + " clicked");
        }
        else
        {
            Debug.Log("No attack");
        }
    }

    public void SetAttack(Attack newAttack)
    {
        attack = newAttack;
        iconType.sprite = attack.sprite;
        nameText.text = attack.name;
    }

    public void ClearAttack()
    {
        attack = null;
        iconType.sprite = defaultSprite;
        nameText.text = "-";
    }
}
