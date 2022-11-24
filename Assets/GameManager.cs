using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Battle,
    Cutscene,
}
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] List<Stage> stages;
    public Stage stage;
    public int currentStage = 0;
    public int stageProgressCounter = 1;

    [SerializeField] TMP_Text stageText;
    [SerializeField] TMP_Text stageProgressText;

    [SerializeField] Image background;

    public bool autoTapActive = false;

    public GameState gameState = GameState.Battle;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        stage = stages[currentStage];
        background.sprite = stage.bgSprite;
        stageText.text = stage.name.ToString();
        stageProgressText.text = stageProgressCounter + " / " + stage.stageLength;

        if (stage.isCutscene)
        {
            gameState = GameState.Cutscene;
            CutsceneManager.instance.PlayCutscene(stage.cutscene);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Battle)
        {


        }
        else if (gameState == GameState.Cutscene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CutsceneManager.instance.StopCutscene();
                IncrementStageProgress();
                gameState = GameState.Battle;

            }

        }

    }

    public int GetLevelOfMonster()
    {
        return Random.Range(stage.minLevel, stage.maxLevel);
    }

    public void IncrementStageProgress()
    {
        Debug.Log("IncrementStageProgress");

        if (stageProgressCounter >= stage.stageLength)
        {
            NextStage();
        }
        else
        {
            Debug.Log("countinng");
            stageProgressCounter++;
        }
        stageProgressText.text = stageProgressCounter + "/" + stage.stageLength;
    }

    public void NextStage()
    {
        if (currentStage < stages.Count - 1)
        {
            currentStage++;
            stage = stages[currentStage];

            if (stage.isCutscene)
            {
                gameState = GameState.Cutscene;
                CutsceneManager.instance.PlayCutscene(stage.cutscene);
            }
            else
            {
                gameState = GameState.Battle;
                background.sprite = stage.bgSprite;
                stageProgressCounter = 1;
                stageText.text = stage.name.ToString();

                MonsterController.instance.InitializeMonster();
            }
        }
        else if (currentStage == stages.Count - 1)
        {
            stage = stages[currentStage];
            background.sprite = stage.bgSprite;
            stageProgressCounter = 1;
            stageText.text = stage.name.ToString();
        }
        else
        {
            stageProgressCounter = 1;
        }

    }

    public float GetBaseAttackSpeed(float monsterSpeed)
    {

        float modifier = currentStage * 0.5f;
        return monsterSpeed + modifier;
    }
}
