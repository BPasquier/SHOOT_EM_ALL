using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubone : MonoBehaviour
{
    [SerializeField]
    float range;
    Animator anim;
    [SerializeField]
    GameObject player;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && isAttacking==false)
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
    }
}
