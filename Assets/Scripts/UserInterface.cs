using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject Menu;

    [SerializeField] private GameObject GOver;
    private bool activated;

    [SerializeField] private Scene Scene;
    private string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    public void RLoad()
    {
        Scene = SceneManager.GetActiveScene();
        sceneName = Scene.name;
        SceneManager.LoadScene(sceneName);
    }

    public void UnloadMenu()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Gestion game over
        if (player.HP <= 0)
        {
            if (activated == true)
            {
                Menu.SetActive(false);
                activated = false;
            }
            GOver.SetActive(true);
            Time.timeScale = 0;
        }
        else if (activated == false)
            Time.timeScale = 1;
        //menu echap
        if (Input.GetKeyDown(KeyCode.Escape))
            if (activated == false)
            {
                Menu.SetActive(true);
                Time.timeScale = 0;
                activated = true;
            }
           else if (activated == true)
            {
                Menu.SetActive(false);
                Time.timeScale = 1;
                activated = false;
            }
    }
}
