using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster
{
    [SerializeField] MonsterBase _base;

    int numberDefeated = 0;
    int numberCaptured = 0;

    public int NumberDefeated { get; set; }
    public int NumberCaptured { get; set; }

    public float maxHealth;

    public int level;

    public float expYield = 1;

    public List<Attack> attacks;

    public Dictionary<Stat, int> Stats { get; private set; }
    public Dictionary<Stat, int> StatBoosts { get; private set; }

    public MonsterBase Base
    {
        get
        {
            return _base;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
    }

    public int Exp { get; set; }

    public int HP { get; set; }

    public int Attack
    {
        get { return GetStat(Stat.Attack); }
    }

    public int Defense
    {
        get { return GetStat(Stat.Defense); }
    }

    public int SpAttack
    {
        get { return GetStat(Stat.SpAttack); }
    }
    public int SpDefense
    {
        get { return GetStat(Stat.SpDefense); }
    }
    public int Speed
    {
        get { return GetStat(Stat.Speed); }
    }

    public int MaxHP
    {
        get; private set;
    }

    public List<Attack> Attacks { get; set; }


    public void Init()
    {


        Attacks = new List<Attack>();

        // This teaches the first four possible moves
        foreach (var attack in Base.LearnableAttacks)
        {
            if (attack.Level <= Level)
            {
                Attacks.Add(attack.Base);

                if (Attacks.Count >= MonsterBase.MaxNumOfMoves)
                    break;
            }
        }

        Exp = 0;

        CalculateStats();

        HP = MaxHP;
    }


    void CalculateStats()
    {
        Stats = new Dictionary<Stat, int>();
        Stats.Add(Stat.Attack, Mathf.FloorToInt(Base.Attack * Level / 100f) + 5);
        Stats.Add(Stat.Defense, Mathf.FloorToInt(Base.Defense * Level / 100f) + 5);
        Stats.Add(Stat.SpAttack, Mathf.FloorToInt(Base.SpAttack * Level / 100f) + 5);
        Stats.Add(Stat.SpDefense, Mathf.FloorToInt(Base.SpDefense * Level / 100f) + 5);
        Stats.Add(Stat.Speed, Mathf.FloorToInt(Base.Speed * Level / 100f) + 5);

        int oldMaxHP = MaxHP;

        MaxHP = Mathf.FloorToInt(Base.MaxHP * Level / 100f) + 10 + Level;

        if (oldMaxHP != 0)
            HP += MaxHP - oldMaxHP;
    }

    public int GetStat(Stat stat)
    {
        int statVal = Stats[stat];

        int boost = StatBoosts[stat];
        var boostValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

        if (boost >= 0)
        {
            statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
        }
        else
        {
            statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);
        }

        return statVal;
    }


    public Monster(MonsterBase cBase, int cLevel)
    {
        _base = cBase;
        level = cLevel;

        Init();
    }

    public void IncrementNumberDefeated()
    {
        // Journal.i.UpdateCreatureNumberDefeatedEntry(this);
    }

    public void IncrementNumberCaptured()
    {
        // Journal.i.UpdateCreatureNumberCapturedEntry(this);
    }

    public float GetHealth()
    {
        return maxHealth * (level * 1.15f);
    }
}
