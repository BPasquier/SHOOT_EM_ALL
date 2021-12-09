using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class Player : Entity
{

    [SerializeField] private Component m_MainCamera;
    [SerializeField] private Vector3 ScreenPos; 
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject Spell1;
    [SerializeField] private long Spell1_ReloadTime;
    [SerializeField] private GameObject Score_Text;
    [SerializeField] private int m_VerticalSpeed;
    [SerializeField] private int m_HorizontalSpeed;
    [SerializeField] float timeBetweenBullet;
    public int score;
    Stopwatch stopWatch = new Stopwatch();
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
        print(stopWatch.ElapsedMilliseconds);
        if (Input.GetKey(KeyCode.Keypad0) && stopWatch.ElapsedMilliseconds>Spell1_ReloadTime)
        {
            stopWatch.Reset();
            stopWatch.Start();
            GameObject spell = Instantiate(Spell1, transform.position + new Vector3(0f, 1f, 1f), Quaternion.Euler(90f,0f,0f));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl();
    }

    void Start()
    {
        HP = HP_Max;
        Enemy.OnBulletHit += Score;
        StartCoroutine(SpawnBullet());
        stopWatch.Start();
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
    
}
