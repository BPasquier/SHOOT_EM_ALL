using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{

    [SerializeField] private Component m_MainCamera;
    [SerializeField] private Vector3 ScreenPos; 
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Score_Text;
    [SerializeField] private int m_VerticalSpeed;
    [SerializeField] private int m_HorizontalSpeed;
    [SerializeField] float timeBetweenBullet;
    public int score;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl();
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
            StartCoroutine(Dash(.2f, 30f));
    }

    void Start()
    {
        HP = HP_Max;
        Enemy.OnBulletHit += Score;
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        while (Application.isPlaying)
        {
            GameObject obj = Instantiate(bullet, transform.position + new Vector3(0f, 0f, 1f), Quaternion.Euler(0f,0f,0f));
            yield return new WaitForSeconds(timeBetweenBullet);
        
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            HP -= 3;
            if (HP<=0)
            {
                HP = 0;
                Destroy(gameObject);
            }
        }
    }

    void Score()
    {
        score += 1;
        Score_Text.GetComponent<Text>().text = "Score : " + score;
    }
    
    IEnumerator Dash(float dashTime , float dashSpeed)
    {
        float left = 0, right = 0, up = 0, down=0;
        if (Input.GetKey(KeyCode.UpArrow) == true)
            up = 1;
        if (Input.GetKey(KeyCode.DownArrow) == true)
            down = 1;
        if (Input.GetKey(KeyCode.RightArrow) == true)
            right = 1;
        if (Input.GetKey(KeyCode.LeftArrow) == true)
            left = 1;
        float startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {
            if (up == 1 && ScreenPos.y < Screen.height * 2 / 3)
                transform.position += dashSpeed * Time.deltaTime * new Vector3(0, 0, 1);
            if (down == 1 && ScreenPos.y > 4.8f)
                transform.position += dashSpeed * Time.deltaTime * new Vector3(0, 0, -1);
            if (right == 1 && ScreenPos.x < Screen.width - 40)
                transform.position += dashSpeed * Time.deltaTime * new Vector3(1, 0, 0);
            if (left == 1 && ScreenPos.x > 40)
                transform.position += dashSpeed * Time.deltaTime * new Vector3(-1, 0, 0);
            yield return null;
        }
    }
}
