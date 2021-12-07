using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    float speed;
    [SerializeField]
    float timeAlive;

    float sinceBorn;

    private void Start()
    {
        sinceBorn = Time.time;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 0f, -speed);
    }
    private void Update()
    {
        if (Time.time > sinceBorn + timeAlive)
            Destroy(gameObject);
        //or if hors champ
    }
}
