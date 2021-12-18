using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGuidedBullet : Bullet
{
    public Vector3 direction;
    float timeBeginning;
    // Start is called before the first frame update
    void Start()
    {
        timeBeginning = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeginning + 1 > Time.time)
            transform.position += new Vector3(0, .1f, 0) * Time.deltaTime;
        else if (timeBeginning + 5 > Time.time)
            transform.position += direction * Time.deltaTime * 10f;
        else
            Destroy(gameObject);
        //detruire si y<0 ou si durée trop longue
    }
}
