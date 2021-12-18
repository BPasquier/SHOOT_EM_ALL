using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampainEnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyTab;
    int[] enemyPercentages = new int[3];
    [SerializeField] float timeBetweenEnemies;
    public GameObject player;
    int i;

    //gestion du temps
    float time;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (Application.isPlaying)
        {
            print(time);

            if (time <= 30)
            {
                enemyPercentages[0] = 75;
                enemyPercentages[1] = 20;
                enemyPercentages[2] = 5;
            }

            if (time > 30 && time <= 90)
            {
                enemyPercentages[0] = 45;
                enemyPercentages[1] = 30;
                enemyPercentages[2] = 25;
            }

            if (time > 90)
            {
                enemyPercentages[0] = 25;
                enemyPercentages[1] = 30;
                enemyPercentages[2] = 45;
            }

            //Crée un gameObject aléatoire de la liste en fonction des pourcentages donnés dans le tab enemyPercentages
            int randomPercent = Random.Range(0, 100);
            int percentSum = 0;
            for (i = 0; percentSum <= randomPercent; i++)
            {
                percentSum += enemyPercentages[i];
            }
            GameObject obj = Instantiate(enemyTab[i - 1], Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)) + new Vector3(Random.Range(-9f, 9f), -Camera.main.transform.position.y, 10f), Quaternion.Euler(0f, 180f, 0f));
            obj.transform.parent = transform;
            yield return new WaitForSeconds(timeBetweenEnemies);

            time += 2;
        }
    }


}
