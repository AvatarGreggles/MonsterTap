using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Create/Create a new Cutscene")]
public class Cutscene : ScriptableObject
{
    public Sprite actorSprite;
    public Sprite backgroundSprite;
    // [SerializeField] Dialog dialog;
    public string dialog;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
