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
    [SerializeField] private bool SpellFixe;
    [SerializeField] private int m_VerticalSpeed;
    [SerializeField] private int m_HorizontalSpeed;
    [SerializeField] float timeBetweenBullet;
    [SerializeField] private GameObject Image;
    public int score;
    Stopwatch stopWatch = new Stopwatch();
    public float timing;
    public int nbBullet;
    public short evo;
    [SerializeField] List<int> EvoCondition;
    GameObject[] childrens;
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
        if (Input.GetKey(KeyCode.Keypad0) && timing>1 && evo >= 2)
        {
            stopWatch.Reset();
            stopWatch.Start();
            if (SpellFixe == false)
            {
                GameObject spell = Instantiate(Spell1, transform.position + new Vector3(0f, 1f, 1f), Quaternion.Euler(0f,0f,0f));
                spell.transform.parent = gameObject.transform;
            }
            else
            {
                GameObject spell = Instantiate(Spell1, transform.position + new Vector3(0f, 1f, 1f), Quaternion.Euler(90f,0f,0f));    
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl();
        timing = (float)stopWatch.ElapsedMilliseconds/Spell1_ReloadTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
            StartCoroutine(Dash(.2f, 30f));
        if (EvoCondition.Count > 0)
        {
           if (score > EvoCondition[0])
            {
                transform.GetChild(evo+1).gameObject.SetActive(true);
                transform.GetChild(evo).gameObject.SetActive(false);
                EvoCondition.RemoveAt(0);
                evo ++;
                if (evo == 1)
                {
                    nbBullet ++;
                }
                if (evo == 2)
                {
                    Image.gameObject.SetActive(true);
                }
            }
        }
    }

    void Start()
    {
        HP = HP_Max;
        nbBullet=1;
        Enemy.OnBulletHit += Score;
        StartCoroutine(SpawnBullet());
        stopWatch.Start();
    }

    IEnumerator SpawnBullet()
    {
        while (Application.isPlaying)
        {
            if (nbBullet%2 == 0)
            {
                float pos = 0;
                List<GameObject> obj = new List<GameObject> ();
                for (int i=0; i<nbBullet; i++)
                {
                    obj.Add(Instantiate(bullet, transform.position + new Vector3(pos-(1f/(float)nbBullet) - (float)(nbBullet-2)/(float)nbBullet, 0f, 0f), Quaternion.Euler(0f,0f,0f)));
                    pos += 2f/(float)nbBullet;
                }
            }
            else
            {
                float pos = 0;
                List<GameObject> obj = new List<GameObject> ();
                for (int i=0; i<nbBullet; i++)
                {
                    pos += 20f/(float)nbBullet;
                    obj.Add(Instantiate(bullet, transform.position + new Vector3(0f, 0f, 0f), Quaternion.Euler(0f,0f,pos-20f/(float)nbBullet-(10*nbBullet -10 )/(float)nbBullet)));
                }
            }
            yield return new WaitForSeconds(timeBetweenBullet);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Attack")
        {
            HP -= col.gameObject.GetComponent<Enemy>().Dammage;
        }
        if (col.gameObject.tag == "Boss")
        {
            HP -= col.gameObject.GetComponent<Boss>().Dammage;
        }
        if (col.gameObject.tag == "HP")
        {
            HP += col.gameObject.GetComponent<Bullet>().Dammage;
            col.gameObject.GetComponent<Bullet>().HP -=1;
            if (col.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "FR")
        {
            timeBetweenBullet = timeBetweenBullet/1.5f;
            col.gameObject.GetComponent<Bullet>().HP -=1;
            if (col.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Enemy_Bullet")
        {
            HP -= col.gameObject.GetComponent<Bullet>().Dammage;
            col.gameObject.GetComponent<Bullet>().HP -=1;
            if (col.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Cubone")
        {
            HP -= 1;
        }
        if (col.gameObject.tag == "NbBullet")
        {
            nbBullet += 1;
            col.gameObject.GetComponent<Bullet>().HP -=1;
            if (col.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(col.gameObject);
        }
        if (HP == 0)
        {
            print("coucou");
            Enemy.OnBulletHit -= Score;
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
