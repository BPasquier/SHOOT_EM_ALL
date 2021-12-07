using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Component m_MainCamera;
    [SerializeField] private int m_VerticalSpeed;
    [SerializeField] private int m_HorizontalSpeed;
    [SerializeField] private Vector3 ScreenPos;
    


    void PlayerControl()
    {
        ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (Input.GetKey(KeyCode.UpArrow) == true &&  ScreenPos.y < Screen.height *2/3)
            transform.position += new Vector3(0, 0.1f, 0);
        if (Input.GetKey(KeyCode.DownArrow) == true && ScreenPos.y > 25)
            transform.position += new Vector3(0, -0.2f, 0);
        if (Input.GetKey(KeyCode.RightArrow) == true && ScreenPos.x < Screen.width -40)
            transform.position += new Vector3(0.15f, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow) == true && ScreenPos.x > 40)
            transform.position += new Vector3(-0.15f, 0, 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl();
    }
}
