using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaptureController : MonoBehaviour
{
    public static CaptureController instance;
    [SerializeField] Image captureImage;
    [SerializeField] TMP_Text amountText;
    Button captureButton;

    [SerializeField] int amountOfCaptureItems = 5;

    private void Awake()
    {
        instance = this;
        captureButton = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {



    }

    public void HandleCapture()
    {
        if (amountOfCaptureItems > 0)
        {
            amountOfCaptureItems--;
            PartyController.instance.AddPartyMember(MonsterController.instance.baseMonster);
            amountText.text = amountOfCaptureItems.ToString();
            captureButton.interactable = false;
            StartCoroutine(MonsterController.instance.HandleCaptureAnimation());
        }
        else
        {
            Debug.Log("No more capture items");
        }
    }

    public void ToggleActiveButton(bool isActive)
    {
        if (isActive && amountOfCaptureItems > 0)
        {
            captureImage.color = Color.white;
            captureButton.interactable = true;
            amountText.color = Color.white;
        }
        else
        {
            captureImage.color = Color.black;
            captureButton.interactable = false;
            amountText.color = Color.black;
        }
    }
}
