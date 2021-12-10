using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected float timeAlive;
    protected Animator anim;
    protected float sinceBorn;

    public delegate void OnHitAction();
    public static event OnHitAction OnBulletHit;

    protected virtual void Start()
    {
        sinceBorn = Time.time;
        anim = GetComponent<Animator>();
        HP = HP_Max;
    }

    public void BulletHit()
    {
        OnBulletHit();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HP -= collision.gameObject.GetComponent<Bullet>().Dammage;
            if (HP<=0)
            {
                Destroy(gameObject);
                OnBulletHit();
            }
            collision.gameObject.GetComponent<Bullet>().HP -=1;
            if (collision.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(collision.gameObject);
        }
    }
}
