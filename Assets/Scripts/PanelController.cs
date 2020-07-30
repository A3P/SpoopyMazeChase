using InfusionEdutainment.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    public TMP_Text endGameText;
    public TMP_Text correctVocabText;
    private string[] gameOverLines = {"You got spooked", "Get Spooked", "Boo", 
        "Now we can be together, forever :)", "try again :p", "One more? :p", "gotchu ;)", "Don't give up now! q-q", "RIP q-q", "Rip in peace =)" };
    private string[] victoryLines = { "argh matey.\n you found my Doubloons", "Noooo, not my Doubloons!", "You can take my life, but not my Doubloons q-q"};
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
        endGameText.text = victoryLines[rnd.Next(victoryLines.Length)];
        GameTimer.Instance.EndTimer();
        SetStats();
    }

    public void GameOver()
    {
        endGameText.text = gameOverLines[rnd.Next(gameOverLines.Length)];
        GameTimer.Instance.EndTimer();
        SetStats();
    }

    private void SetStats()
    {
        StatTracker statTracker = StatTracker.Instance;
        correctVocabText.text += statTracker.gatesOpened;
    }
}
