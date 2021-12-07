using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    [SerializeField] private Scene Scene;
    private string sceneName;
    [SerializeField] private 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RLoad()
    {
        Scene = SceneManager.GetActiveScene();
        sceneName = Scene.name;
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
