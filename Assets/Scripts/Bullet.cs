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
    [SerializeField] protected bool rotation;

    private void Start()
    {
        sinceBorn = Time.time;
        HP = HP_Max;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotation)
            transform.position += new Vector3(this.transform.rotation.z, 0f, speed);
        else
            transform.position += new Vector3(0, 0f, speed);
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
