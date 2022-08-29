using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    [SerializeField] Image actorImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject[] UItoHide;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCutscene(Cutscene cutscene)
    {
        actorImage.gameObject.SetActive(true);

        foreach (GameObject UI in UItoHide)
        {
            UI.SetActive(false);
        }

        actorImage.sprite = cutscene.actorSprite;
        backgroundImage.sprite = cutscene.backgroundSprite;
        dialogText.text = cutscene.dialog;
        dialogBox.SetActive(true);
    }

    public void StopCutscene()
    {
        foreach (GameObject UI in UItoHide)
        {
            UI.SetActive(true);
        }

        actorImage.gameObject.SetActive(false);
        dialogBox.SetActive(false);

        Debug.Log("stop cutscene");

    }
}
