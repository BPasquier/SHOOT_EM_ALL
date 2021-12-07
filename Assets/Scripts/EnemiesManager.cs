using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    GameObject ennemy;
    [SerializeField]
    float timeBetweenEnemies;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (Application.isPlaying)
        {
            GameObject obj = Instantiate(ennemy, Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)) + new Vector3(Random.Range(-9f, 9f),-9.5f,10f), Quaternion.Euler(0f,180f,0f));
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }


}
