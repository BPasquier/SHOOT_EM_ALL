using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirtle : Boss
{
    GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = transform.parent.GetComponent<EnemiesManager>().player;
        StartCoroutine(UpdateSquirtle());
    }

    private IEnumerator UpdateSquirtle()
    {
        while (true)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)  //attaque bulle d'eau
            {
                //Se dirige en face de player
                Vector3 direction = transform.InverseTransformPoint(player.transform.position);
                Debug.Log(direction);
                anim.SetBool("moving", true);
                yield return new WaitForSeconds(.5f);
                while (direction.x < -1 || direction.x > 1)
                {
                    if (direction.x < -1)
                    {
                        transform.position += new Vector3(-.1f, 0f, 0f);
                        Debug.Log("-");
                    }
                    else if (direction.x > 1)
                    {
                        transform.position += new Vector3(.1f, 0f, 0f);
                        Debug.Log("+");
                    }
                    direction = transform.InverseTransformPoint(player.transform.position);
                    yield return new WaitForSeconds(0.01f);
                }
                anim.SetBool("moving", false);
                yield return new WaitForSeconds(1);

                //attaque
                anim.SetBool("bubble", true);
                yield return new WaitForSeconds(Random.Range(7, 10));
                anim.SetBool("bubble", false);
                yield return new WaitForSeconds(1);
                //retourne au milieu
                anim.SetBool("moving", true);
                while (transform.position.x <-0.05  || transform.position.x > 0.05)
                {
                    if (transform.position.x < 0)
                        transform.position += new Vector3(.1f, 0f, 0f);
                    else if (transform.position.x > 0)
                        transform.position += new Vector3(-.1f, 0f, 0f);
                    yield return new WaitForSeconds(0.01f);
                }
                anim.SetBool("moving", false);
            }
            else if (rand == 1) //charge
            {
                Vector3 direction = transform.InverseTransformPoint(player.transform.position);
                Debug.Log(direction);
                anim.SetBool("moving", true);
                yield return new WaitForSeconds(.5f);
                int advancecount = 0;
                while (Vector3.Distance(player.transform.position, transform.position) > 1f)
                {
                    transform.position -= direction.normalized * .1f;
                    yield return new WaitForSeconds(0.01f);
                    advancecount += 1;
                }
                Debug.Log("return");
                while (advancecount>0)
                {
                    transform.position += direction.normalized * .1f;
                    yield return new WaitForSeconds(0.01f);
                    advancecount -= 1;
                }
                anim.SetBool("moving", false);
            }
            yield return new WaitForSeconds(Random.Range(1, 2));
        }
    }
}
