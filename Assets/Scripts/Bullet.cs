using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    [SerializeField]
    float speed;
    [SerializeField]
    float timeAlive;
    [SerializeField] public float Dammage;
    float sinceBorn;

    private void Start()
    {
        sinceBorn = Time.time;
        HP = HP_Max;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 0f, speed);
        Vector3 posFromScreen = Camera.main.WorldToScreenPoint(transform.position);
        if (posFromScreen.y > Screen.height * 1.5)
            Destroy(gameObject);

    }
    private void Update()
    {
        if (Time.time > sinceBorn + timeAlive)
            Destroy(gameObject);
        //or if hors champ
    }
}
