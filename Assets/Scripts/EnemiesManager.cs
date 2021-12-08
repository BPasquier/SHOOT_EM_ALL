using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyTab;
    [SerializeField]
    int[] enemyPercentages;
    [SerializeField]
    float timeBetweenEnemies;
    int i;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (Application.isPlaying)
        {
            //Crée un gameObject aléatoire de la liste en fonction des pourcentages donnés dans le tab enemyPercentages
            int randomPercent = Random.Range(0, 100);
            int percentSum = 0;
            for (i=0; percentSum <= randomPercent; i++)
            {
                percentSum += enemyPercentages[i];
            }
            GameObject obj = Instantiate(enemyTab[i-1], Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)) + new Vector3(Random.Range(-9f, 9f), -9.5f, 10f), Quaternion.Euler(0f, 180f, 0f));
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }


}
