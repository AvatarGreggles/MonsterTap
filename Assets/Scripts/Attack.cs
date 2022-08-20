using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Normal,
    Grass,
    Fire,
    Water
}
[CreateAssetMenu(fileName = "New Attack", menuName = "Create/New Attack")]
public class Attack : ScriptableObject
{
    public Sprite sprite;
    public string name;
    public int damage;
    public AttackType type;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
