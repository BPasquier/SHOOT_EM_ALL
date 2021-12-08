using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    float speed;
    [SerializeField]
    float timeAlive;
    [SerializeField]
    float range;
    [SerializeField]
    float dureeAttack;
    Animator anim;

    float timeLastCollision;
    float sinceBorn;

    public delegate void OnHitAction();
    public static event OnHitAction OnBulletHit;

    private void Start()
    {
        sinceBorn = Time.time;
        anim = GetComponent<Animator>();
        timeLastCollision = Time.time - dureeAttack;
        HP = HP_Max;
    }
    // Update is called once per frame
    void FixedUpdate()
    {        
        if (timeLastCollision + dureeAttack < Time.time) //lorsque le rattata a finit d'attaquer, cours
            transform.position += new Vector3(0f, 0f, -speed);
    }
    private void Update()
    {
        if (Time.time > sinceBorn + timeAlive)
            Destroy(gameObject);
        //or if hors champ
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (range <= 1f && collision.transform.tag == "Player")
        {
            Debug.Log("attack");
            anim.SetBool("attack", true);
            timeLastCollision = Time.time;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            HP -= 1;
            if (HP<=0)
            {
                Destroy(gameObject);
                OnBulletHit();
            }

        }
    }
}
