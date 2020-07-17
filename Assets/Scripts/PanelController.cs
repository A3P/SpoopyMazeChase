using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    public TMP_Text text;
    private string[] gameOverLines = {"You got spooked", "Get Spooked", "Doot Doot", "Boo", "Now we can be together :)"};
    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {
        text.text = "argh matey.\n you found my doubloons";
    }

    public void GameOver()
    {
        text.text = gameOverLines[rnd.Next(gameOverLines.Length)];
    }
}
