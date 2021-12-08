using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : MonoBehaviour
{
    public void exitScene()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
