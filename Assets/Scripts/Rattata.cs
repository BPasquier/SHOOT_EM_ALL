using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rattata : Enemy
{
    [SerializeField] protected float speed;
    [SerializeField] protected float range;
    [SerializeField] protected float dureeAttack;
    protected float timeLastCollision;

    protected override void Start()
    {
        sinceBorn = Time.time;
        anim = GetComponent<Animator>();
        timeLastCollision = Time.time - dureeAttack;
        HP = HP_Max;
    }
    // Update is called once per frame
    protected void FixedUpdate()
    {        
        if (timeLastCollision + dureeAttack < Time.time) //lorsque le rattata a finit d'attaquer, cours
            transform.position += new Vector3(0f, 0f, -speed);
    }
    protected void Update()
    {
        if (Time.time > sinceBorn + timeAlive)
            Destroy(gameObject);
        //or if hors champ
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (range <= 1f && collision.transform.tag == "Player")
        {
            //Debug.Log("attack");
            anim.SetBool("attack", true);
            timeLastCollision = Time.time;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            HP -= collision.gameObject.GetComponent<Bullet>().Dammage;
            if (HP<=0)
            {
                Drop(DropRate);
                Destroy(gameObject);
                BulletHit();
            }
            collision.gameObject.GetComponent<Bullet>().HP -=1;
            if (collision.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(collision.gameObject);
        }
    }
}
