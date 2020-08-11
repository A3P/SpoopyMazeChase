using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    public Texture[] movementTutorial;
    public Texture[] flashLightTutorial;
    public Texture[] stunGhostTutorial;
    public Texture[] runTutorial;
    public Texture[] openGateTutorial;
    public Texture[] findChestTutorial;

    public float animationDuration;
    public Button nextButton;
    public Button previousButton;
    public RawImage tutorialImage;

    private Texture[] currentTutorial;
    private int currentIndex = 0;
    private List<Texture[]> tutorials = new List<Texture[]>();

    // Start is called before the first frame update
    void Awake()
    {
        tutorials.Add(movementTutorial);
        tutorials.Add(runTutorial);
        tutorials.Add(openGateTutorial);
        tutorials.Add(findChestTutorial);
        tutorials.Add(flashLightTutorial);
        tutorials.Add(stunGhostTutorial);
        ShowTutorial(0);
        StartCoroutine(PlayTutorial());
    }

    public void ShowPrevious()
    {
        if (currentIndex - 1 >= 0)
        {
            ShowTutorial(currentIndex - 1);
        }
    }

    public void ShowNext()
    {
        if (currentIndex + 1 < tutorials.Count)
        {
            ShowTutorial(currentIndex + 1);
        }
    }

    public void ShowTutorial(int index)
    {
        currentIndex = index;
        currentTutorial = tutorials[index];
        SetButtons();
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Title");
    }

    private void SetButtons()
    {
        if(currentIndex > 0)
        {
            previousButton.gameObject.SetActive(true);
        } else
        {
            previousButton.gameObject.SetActive(false);
        }
        if (currentIndex < tutorials.Count - 1)
        {
            nextButton.gameObject.SetActive(true);
        } else
        {
            nextButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayTutorial()
    {
        for (int i = 0; currentTutorial != null; i++)
        {
            i = i % currentTutorial.Length;
            tutorialImage.texture = currentTutorial[i];
            yield return new WaitForSeconds(animationDuration);
        }
        yield break;
    }

}
