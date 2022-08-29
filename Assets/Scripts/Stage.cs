using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Stage", menuName = "Create/Create a new Stage")]
public class Stage : ScriptableObject
{
    public string name;
    public string description;
    public Sprite bgSprite;
    public AudioClip music;
    public int stageLength;
    public List<MonsterBase> monsters;

    public int minLevel;
    public int maxLevel;

    public bool isTrainer = false;

    public Cutscene cutscene;
    public bool isCutscene = false;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
