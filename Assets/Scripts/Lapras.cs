using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lapras : Boss
{
    bool under60HP = false;
    bool under10HP = false;
    [SerializeField] bool iceBeam;
    [SerializeField] bool iceSpikes;
    [SerializeField] bool iceShards;
    [SerializeField] GameObject iceSpikeObj;
    [SerializeField] GameObject iceShardObj;
    bool iceSpikessaved= false;
    bool iceShardssaved = false;
    GameObject player;
    Transform neck;
    private void Start()
    {
        HP = HP_Max;
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name.Equals("Campain"))
            player = transform.parent.GetComponent<CampainEnemiesManager>().player;
        else if (SceneManager.GetActiveScene().name.Equals("Survival"))
            player = transform.parent.GetComponent<EnemiesManager>().player;
        Transform[] listchildrens = GetComponentsInChildren<Transform>();
        for (int i=0; i<listchildrens.Length;i++)
        {
            if (listchildrens[i].name.Equals("Neck2"))
            {
                neck = listchildrens[i];
                break;
            }      
        }
        StartCoroutine(InstantiateLapras());
        transform.Rotate(0, 180f, 0);
    }
    
    private IEnumerator InstantiateLapras()
    {
        while (transform.position.z > 5f)
        {
            transform.position += new Vector3(0, 0, -.05f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetBool("moving", false);
        yield return new WaitForSeconds(1);
        StartCoroutine(UpdateLapras());
    }
    private IEnumerator UpdateLapras()
    {
        while (true)
        {
            int rand = Random.Range(0, 3);
            
            if (rand==0)
            {
                anim.SetBool("IceBeam", true);
                float timedebut = Time.time;

                while (Time.time< timedebut+10)
                {
                    Vector3 direction = neck.InverseTransformPoint(player.transform.position);
                    direction = neck.InverseTransformPoint(player.transform.position);
                    Debug.Log(direction);
                    if (direction.z < -1)
                        neck.Rotate(new Vector3(0f, -.1f, 0f));
                    else if (direction.z > 1)
                        neck.Rotate(new Vector3(0f, .1f, 0f));
                    yield return new WaitForSeconds(.01f);
                }

                Vector3 rotationNeck = (neck.localRotation).eulerAngles;
                while (rotationNeck.y < -1 || rotationNeck.y > 1)
                {
                    if (rotationNeck.y < -1)
                        neck.Rotate(new Vector3(0f, .1f, 0f));
                    else if (rotationNeck.y > 1)
                        neck.Rotate(new Vector3(0f, -.1f, 0f));
                    rotationNeck = (neck.localRotation).eulerAngles;
                    yield return new WaitForSeconds(.01f);
                }
                anim.SetBool("IceBeam", false);
            }

            else if (rand == 1)
            {
                StartCoroutine(spawnIceSpikes(10));
                yield return new WaitForSeconds(2f);
            }
            else if (rand == 2)
            {
                StartCoroutine(spawnIceShards());
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator spawnIceSpikes(int nbSpikes)
    {
        for (int i = 0; i < nbSpikes; i++)
        {
            Vector3 posIceSpike = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)) + new Vector3(Random.Range(-19f, 19f), -Camera.main.transform.position.y, Random.Range(-9f, 9f));

            Debug.Log("posIceSpike: "+ posIceSpike);
            Quaternion rotationIceSpike = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Destroy(Instantiate(iceSpikeObj, posIceSpike, rotationIceSpike), 0.48f / 0.5f *1.7f);
            yield return new WaitForSeconds(.5f);
        }

    }
    IEnumerator spawnIceShards()
    {
        Vector3 posIceShard = transform.TransformPoint(41f, 35f, 48f);
        Quaternion rotationIceShard = Random.rotation;
        GameObject shard0 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard0.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(.5f);

        posIceShard = transform.TransformPoint(-41f, 35f, 48f);
        rotationIceShard = Random.rotation;
        GameObject shard1 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard1.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(.5f);

        posIceShard = transform.TransformPoint(30f, 51f, 22f);
        rotationIceShard = Random.rotation;
        GameObject shard2 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard2.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(.5f);

        posIceShard = transform.TransformPoint(-30f, 51f, 22f);
        rotationIceShard = Random.rotation;
        GameObject shard3 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard3.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(.5f);
    }

}
