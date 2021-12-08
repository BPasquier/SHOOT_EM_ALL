using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject GOver;
    private bool activated;


    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    public void UnloadMenu()
    {
        Menu.SetActive(false);
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
        }

        //menu echap
        if (Input.GetKeyDown(KeyCode.Escape))
            if (activated == false)
            {
                Menu.SetActive(true);
                activated = true;
            }
           else if (activated == true)
            {
                Menu.SetActive(false);
                activated = false;
            }
    }
}
