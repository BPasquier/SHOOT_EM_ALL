using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        anim = GetComponent<Animator>();
        player = transform.parent.GetComponent<EnemiesManager>().player;
        /*Transform[] listchildrens = GetComponentsInChildren<Transform>();
        for (int i=0; i<listchildrens.Length;i++)
        {
            if (listchildrens[i].name.Equals("Neck3"))
            {
                neck = listchildrens[i];
                break;
            }      
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IceBeam", iceBeam);
        /*if (iceBeam)
        {
            Vector3 direction = transform.InverseTransformPoint(player.transform.position);
            if (direction.x < -1)
                neck.Rotate(new Vector3(-1f, 0f, 0f));
            else if (direction.x > 1)
                neck.Rotate(new Vector3(1f, 0f, 0f));

        }
        else
        {
            neck.rotation = Quaternion.Euler(new Vector3 (neck.rotation.x, neck.rotation.y, neck.rotation.z));
        }*/
        if (iceSpikes && !iceSpikessaved) //regarde si il vient d'etre true
        {
            iceSpikessaved = true;
            StartCoroutine(spawnIceSpikes(5));
        }
        if (!iceSpikes)
            iceSpikessaved = false;
        if (iceShards && !iceShardssaved) //regarde si il vient d'etre true
        {
            iceShardssaved = true;
            StartCoroutine(spawnIceShards());
        }
        if (!iceShards)
            iceShardssaved = false;

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
        new WaitForSeconds(.5f);

        posIceShard = transform.TransformPoint(-41f, 35f, 48f);
        rotationIceShard = Random.rotation;
        GameObject shard1 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard1.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        new WaitForSeconds(.5f);

        posIceShard = transform.TransformPoint(30f, 51f, 22f);
        rotationIceShard = Random.rotation;
        GameObject shard2 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard2.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        new WaitForSeconds(.5f);

        posIceShard = transform.TransformPoint(-30f, 51f, 22f);
        rotationIceShard = Random.rotation;
        GameObject shard3 = Instantiate(iceShardObj, posIceShard, rotationIceShard);
        shard3.GetComponent<AutoGuidedBullet>().direction = (player.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(.5f);
    }

}