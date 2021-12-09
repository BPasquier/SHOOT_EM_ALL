using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubone : Entity
{
    [SerializeField]
    float range;
    Animator anim;
    bool isAttacking = false;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform.parent.GetComponent<EnemiesManager>().player;
    }

    // Update is called once per frame
    void Update()
    {
        //destruction en cas de sortie de l'écran Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //attaque si a la bonne distance du player
        if (Vector3.Distance(transform.position, player.transform.position) < range && transform.position.z > player.transform.position.z - 0.2f && isAttacking == false)
        {
            isAttacking = true;

            //Se tourne vers l'ennemy
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            /*rotation = Quaternion.Slerp(transform.rotation, rotation, Random.Range(0f, 1f));
            Debug.Log(rotation);*/
            transform.rotation = rotation;
            //tourne un petit peu de l'ennemi pour que l'os ne soit pas trop a droite
            transform.Rotate(new Vector3(0f, -5f, 0f));

            anim.SetBool("isAttacking", true);
        }
        //avance sinon
        if (Vector3.Distance(transform.position, player.transform.position) > range && transform.position.z > player.transform.position.z - 0.2f)
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
            transform.position += new Vector3(0f, 0f, -.05f);
        }
    }
}
