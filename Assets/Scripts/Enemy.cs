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
    
    [SerializeField] protected GameObject[] DropTab;
    [SerializeField] protected int[] DropsPercentages;
    [SerializeField] protected int DropRate;
    public float Dammage;


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
                Drop(DropRate);
                Destroy(gameObject);
                OnBulletHit();
            }
            collision.gameObject.GetComponent<Bullet>().HP -=1;
            if (collision.gameObject.GetComponent<Bullet>().HP <= 0)
                Destroy(collision.gameObject);
        }
    }

    protected void Drop(int dropRate)
    {
        if (dropRate>Random.Range(0,100) && DropTab.Length != 0 && DropsPercentages.Length != 0)
        {
            int randomPercent = (int)Random.Range(0, 100);
            int percentSum = 0;
            int i;
            for (i=0; percentSum <= randomPercent; i++)
            {
                percentSum += DropsPercentages[i];
            }
            GameObject obj = Instantiate(DropTab[i-1], transform.position + new Vector3(0f, 0.35f, -1f), Quaternion.Euler(90f, 0f, 0f));
        }
    }
}
