using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int stage = 1;
    public int stageProgressCounter = 0;
    public int stageProgressGoal = 10;

    public List<Vector2> levelRanges;

    [SerializeField] TMP_Text stageText;
    [SerializeField] TMP_Text stageProgressText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        stageText.text = stage.ToString();
        stageProgressText.text = stageProgressCounter + " / " + stageProgressGoal;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetLevelOfMonster()
    {
        if (stage < levelRanges.Count)
        {
            return Mathf.FloorToInt(UnityEngine.Random.Range(levelRanges[stage - 1].x, levelRanges[stage - 1].y));
        }
        else
        {
            return Mathf.FloorToInt(UnityEngine.Random.Range(levelRanges[levelRanges.Count - 1].x, levelRanges[levelRanges.Count - 1].y));
        }


    }

    public void IncrementStageProgress()
    {

        if (stageProgressCounter >= stageProgressGoal)
        {
            NextStage();
        }
        else
        {
            stageProgressCounter++;
        }
        stageProgressText.text = stageProgressCounter + "/" + stageProgressGoal;
    }

    public void NextStage()
    {
        stage++;
        stageProgressCounter = 0;
        stageText.text = stage.ToString();
    }
}
