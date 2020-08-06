using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnClick()
    {
#if UNITY_EDITOR
        Debug.Log("OnClick quit");
#endif

        Application.Quit();
    }
}
