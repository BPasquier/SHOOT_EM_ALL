using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Component m_MainCamera;
    [SerializeField] private int m_VerticalSpeed;
    [SerializeField] private int m_HorizontalSpeed;
    [SerializeField] private Vector3 ScreenPos;
    public short HP;
    [SerializeField] private short HP_Max;
    [SerializeField] GameObject prefabBullet;
    [SerializeField] float attackSpeed;

    float savedTime = 0;

    void PlayerControl()
    {
        ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (Input.GetKey(KeyCode.UpArrow) == true &&  ScreenPos.y < Screen.height *2/3)
            transform.position += new Vector3(0, 0, 0.1f);
        if (Input.GetKey(KeyCode.DownArrow) == true && ScreenPos.y > 4.8f)
            transform.position += new Vector3(0, 0, -0.2f);
        if (Input.GetKey(KeyCode.RightArrow) == true && ScreenPos.x < Screen.width -40)
            transform.position += new Vector3(0.15f, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow) == true && ScreenPos.x > 40)
            transform.position += new Vector3(-0.15f, 0, 0);
        //Tir
        if (Input.GetKey(KeyCode.Space) == true && (Time.time - savedTime > attackSpeed))
        {
            Instantiate(prefabBullet, transform.position + new Vector3(0f,0f,.5f), transform.rotation);
            savedTime = Time.time;
        }
            
    }

    void awake()
    {
        HP = HP_Max;
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
   
    void OnCollisionEnter()
    {
        HP -= 3;
    }
    
}
