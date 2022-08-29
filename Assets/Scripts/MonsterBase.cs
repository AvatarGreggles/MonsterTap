using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create a new Creature")]
public class MonsterBase : ScriptableObject
{

    [SerializeField] string id;
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite sprite;
    [SerializeField] Sprite icon;
    [SerializeField] AudioClip cry;

    [SerializeField] MonsterType monsterType1;
    [SerializeField] MonsterType monsterType2;

    // Base stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;
    [SerializeField] int expYield;
    [SerializeField] int catchRate = 255;

    [SerializeField] List<LearnableAttack> learnableAttacks;

    [SerializeField] List<Evolution> evolutions;

    public static int MaxNumOfMoves { get; set; } = 4;

    public int GetExpForLevel(int level)
    {
        return 4 * (level * level * (level * 2)) / 5;
    }



    public string ID
    {
        get { return id; }
    }

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }

    public AudioClip Cry
    {
        get { return cry; }
    }


    public MonsterType MonsterType1
    {
        get { return monsterType1; }
    }

    public MonsterType MonsterType2
    {
        get { return monsterType2; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public int ExpYield => expYield;

    public int CatchRate => catchRate;


    public List<LearnableAttack> LearnableAttacks
    {
        get { return learnableAttacks; }
    }

    public List<Evolution> Evolutions => evolutions;



}

[System.Serializable]
public class Evolution
{
    [SerializeField] MonsterBase evolvesInto;
    [SerializeField] int requiredLevel;

    public MonsterBase EvolvesInto => evolvesInto;
    public int RequiredLevel => requiredLevel;

}

[System.Serializable]
public class LearnableAttack
{
    [SerializeField] Attack attackBase;
    [SerializeField] int level;

    public Attack Base { get => attackBase; set => attackBase = value; }

    public int Level { get => level; set => level = value; }

}



public enum MonsterType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Fairy,
    Dark,
    Steel
}

public enum Stat
{
    Attack,
    Defense,
    SpAttack,
    SpDefense,
    Speed,

    // secondary stats
    Accuracy,
    Evasion
}

public class TypeChart
{
    static public float S = 2f;
    static public float N = 0.5f;
    static public float o = 1f;
    static public float X = 0f;

    static float[][] chart =  {
            //               NOR FIR WAT ELE GRA ICE FIG POI GRO FLY PSY BUG ROC GHO DRA FAI DAR STEEL
        /*NOR*/ new float[]{ o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  N,  X,  o,   o,  o, N },
        /*FIR*/ new float[]{ o,  N,  N,  o,  S,  S,  o,  o,  o,  o,  o,  S,  N,  o,  N,   o,  o, S },
        /*WAT*/ new float[]{ o,  S,  N,  o,  N,  o,  o,  o,  S,  o,  o,  o,  S,  o,  N,   o,  o, o },
        /*ELE*/ new float[]{ o,  o,  S,  N,  N,  o,  o,  o,  X,  S,  o,  o,  o,  o,  N,   o,  o, o },
        /*GRA*/ new float[]{ o,  N,  S,  o,  o,  o,  o,  N,  S,  N,  o,  N,  S,  o,  N,   o,  o, N },
        /*ICE*/ new float[]{ o,  N,  N,  o,  S,  N,  o,  o,  S,  S,  o,  o,  o,  o,  S,   o,  o, N },
        /*FIG*/ new float[]{ S,  o,  o,  o,  o,  S,  o,  N,  o,  N,  N,  N,  S,  X,  o,   N,  S, S },
        /*POI*/ new float[]{ o,  o,  o,  o,  S,  o,  o,  N,  N,  o,  o,  o,  N,  N,  o,   S,  o, X },
        /*GRO*/ new float[]{ o,  S,  o,  S,  N,  o,  o,  S,  o,  X,  o,  N,  S,  o,  o,   o,  o, S },
        /*FLY*/ new float[]{ o,  o,  o,  N,  S,  o,  S,  o,  o,  o,  o,  S,  N,  o,  o,   o,  o, N },
        /*PSY*/ new float[]{ o,  o,  o,  o,  o,  o,  S,  S,  o,  o,  N,  o,  o,  o,  o,   o,  X, N },
        /*BUG*/ new float[]{ o,  N,  o,  o,  S,  o,  N,  N,  o,  N,  S,  o,  o,  N,  o,   N,  S, N },
        /*ROC*/ new float[]{ o,  S,  o,  o,  o,  S,  N,  o,  N,  S,  o,  S,  o,  o,  o,   o,  o, N },
        /*GHO*/ new float[]{ X,  o,  o,  o,  o,  o,  o,  o,  o,  o,  S,  o,  o,  S,  o,   o,  N, o },
        /*DRA*/ new float[]{ o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  o,  S,   X,  o, N },
        /*FAI*/ new float[]{ o,  N,  o,  o,  o,  o,  S,  N,  o,  o,  o,  o,  o,  o,  S,   o,  S, N },
        /*DAR*/ new float[]{ o,  o,  o,  o,  o,  o,  N,  o,  o,  o,  S,  o,  o,  S,  o,   N,  N, o },
        /*STE*/ new float[]{ o,  N,  N,  N,  o,  S,  o,  o,  o,  o,  o,  o,  S,  o,  o,   S,  o, N },
      };

    public static float GetEffectiveness(MonsterType attackType, MonsterType defenseType)
    {
        if (attackType == MonsterType.None || defenseType == MonsterType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];

    }
}
