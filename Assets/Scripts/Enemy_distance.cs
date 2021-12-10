using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_distance : Enemy
{
    [SerializeField] protected float dureeAttack;
    [SerializeField] protected GameObject bulletPrefab; 
     protected float timeBeginningAttack;

    protected override void Start()
    {
        sinceBorn = Time.time;
        anim = GetComponent<Animator>();
        HP = HP_Max;
    }
    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (sinceBorn + 2.2 > Time.time) //avance en haut du terrain
        {
            //Debug.Log("avance");
            transform.position += new Vector3(0f, 0f, -.05f);
            timeBeginningAttack = Time.time;
        }
        else if (sinceBorn + 2.2 + timeAlive > Time.time)//commence a attaquer
        {
            if (timeBeginningAttack + dureeAttack < Time.time)
            {
                anim.SetBool("attack", true);
                timeBeginningAttack = Time.time;
                GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f,0.2f,0f), transform.rotation);
                bullet.transform.Rotate(new Vector3(90f, 0f, 90f));
            }
        }
        else if (sinceBorn + 2.2*2 + timeAlive > Time.time)//se retire
        {
            anim.SetBool("attack", false);
            transform.position += new Vector3(0f, 0f, 0.05f);
        }
        else //est d�truit
        {
            Destroy(gameObject);
        }

    }
}
