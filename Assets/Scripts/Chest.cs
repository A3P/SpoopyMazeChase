using InfusionEdutainment.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Chest : MonoBehaviour
{
    public float minDistance;

    private GameController gameController;
    private FirstPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        player = FirstPersonController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < minDistance)
        {
            gameController.Victory();
        }
    }
}
