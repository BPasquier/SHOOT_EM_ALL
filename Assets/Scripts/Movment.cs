using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{

    [SerializeField] private Component m_MainCamera;
    [SerializeField] private int m_VerticalSpeed;
    [SerializeField] private int m_HorizontalSpeed;


    void PlayerControl()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
            transform.position += new Vector3(0, 0.3f, 0);
        if (Input.GetKey(KeyCode.DownArrow) == true)
            transform.position += new Vector3(0, -0.3f, 0);
        if (Input.GetKey(KeyCode.RightArrow) == true)
            transform.position += new Vector3(0.3f, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow) == true)
            transform.position += new Vector3(-0.3f, 0, 0);

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
