using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampainEnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyTab;
    [SerializeField] GameObject[] bossTab;
    protected float[] bossHealth = new float[2];
    int[] enemyPercentages = new int[3];
    [SerializeField] float timeBetweenEnemies = 2.0f;
    public GameObject player;
    int i;

    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Victoire;
    [SerializeField] private GameObject GOver;

    //gestion musiques
    [SerializeField] AudioSource musicManager;
    [SerializeField] AudioClip musicRoute;
    [SerializeField] AudioClip musicBoss1;
    [SerializeField] AudioClip musicBoss2;
    [SerializeField] GameObject bossHealthBar;

    //gestion du temps
    float time;
    bool enemiesPhase;
    bool bossPhase;
    int idBoss = 0;

    private void Start()
    {
        bossHealthBar.SetActive(false);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (Application.isPlaying)
        {
            print(time);

            if (time <= 30) //mobs phase 1
            {
                enemyPercentages[0] = 75;
                enemyPercentages[1] = 25;
                enemyPercentages[2] = 0;
                enemiesPhase = true;
            }

            if (time > 30 && time <= 90) //mobs phase 2
            {
                enemyPercentages[0] = 45;
                enemyPercentages[1] = 30;
                enemyPercentages[2] = 25;
                enemiesPhase = true;
            }

            if (time > 90 && time < 120) //mobs phase 3
            {
                enemyPercentages[0] = 25;
                enemyPercentages[1] = 30;
                enemyPercentages[2] = 45;
                enemiesPhase = true;
            }
            
            if (time == 120) //boss phase 1
            {
                musicManager.clip = musicBoss1;
                musicManager.Play();
                enemiesPhase = false;
                bossHealth[idBoss] = bossTab[idBoss].GetComponent<Entity>().HP;
                print(bossHealth[idBoss]);
                bossPhase = true;
            }

            if (time >=120 && enemiesPhase == false && bossPhase == false)
            {
                enemiesPhase = true;
                enemyPercentages[0] = 15;
                enemyPercentages[1] = 35;
                enemyPercentages[2] = 50;
                timeBetweenEnemies -= 0.5f;
            }

            if (time == 210) //boss phase 2
            {
                idBoss = 1;
                musicManager.clip = musicBoss2;
                musicManager.Play();
                enemiesPhase = false;
                bossHealth[idBoss] = bossTab[idBoss].GetComponent<Entity>().HP;
                bossPhase = true;
            }

            if(time > 210)
            {
                Menu.SetActive(false);
                GOver.SetActive(false);
                Time.timeScale = 0;
                Victoire.SetActive(true);
            }

            if (enemiesPhase == true)
            {
                //Cree un gameObject aleatoire de la liste en fonction des pourcentages donnes dans le tab enemyPercentages
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

            if (bossPhase == true)
            {
                //Cree un gameObject boss
                GameObject boss = Instantiate(bossTab[idBoss], Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)) + new Vector3(Random.Range(-9f, 9f), -Camera.main.transform.position.y, 10f), Quaternion.Euler(0f, 0f, 0f));
                boss.transform.parent = transform;
                bossHealthBar.GetComponent<HP_Bar_Script>().joueur = boss.GetComponent<Entity>();
                bossHealthBar.SetActive(true);

                //on attend que le joueur batte le boss et on desactive le mode boss pour revenir a des vagues d'ennemies normales
                yield return new WaitUntil(() => (boss.GetComponent<Entity>().HP <= 0));
                Destroy(boss);
                bossPhase = false;
                enemiesPhase = true;
                time+=2;
                bossHealthBar.SetActive(false);
                timeBetweenEnemies -= 0.7f;
                //musique d'apres boss
                musicManager.clip = musicRoute;
                musicManager.Play();
            }

        }
    }


}
