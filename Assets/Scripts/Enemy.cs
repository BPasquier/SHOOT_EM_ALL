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

    private void Start()
    {
        sinceBorn = Time.time;
        anim = GetComponent<Animator>();
        timeLastCollision = Time.time - dureeAttack;
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
        //Debug.Log("OnCollisionEnter()");
        if (range <= 1f && collision.transform.tag == "Player")
        {
            Debug.Log("attack");
            anim.SetBool("attack", true);
            timeLastCollision = Time.time;
        }
    }
}
