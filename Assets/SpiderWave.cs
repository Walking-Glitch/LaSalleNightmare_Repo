using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWave : MonoBehaviour
{
    public GameObject spiderPrefabPurple;
    public GameObject spiderPrefabRed;
    public GameObject spiderPrefabBlack;

    public Transform spawnPoint;
    public GameObject zone;

    private int maxBlackSpiders = 8, maxPurpleSpiders = 10, maxRedSpiders = 5;
    public int BlackSpidersAlive, RedSpidersAlive, PurpleSpidersAlive, TotalSpidersAlive;
    private bool enable;

    

    private void Start()
    {
        InvokeRepeating("SpiderCounter", 0f, 1f);
    }

    private void Update()
    {
        SpawnSpiderBlack(enable);
        SpawnSpiderRed(enable);
        SpawnSpiderPurple(enable);
    }



    private void SpiderCounter()
    {
        PurpleSpidersAlive = GameObject.FindGameObjectsWithTag("Spider1").Length;
        RedSpidersAlive = GameObject.FindGameObjectsWithTag("Spider2").Length;
        BlackSpidersAlive = GameObject.FindGameObjectsWithTag("Spider3").Length;

        TotalSpidersAlive = PurpleSpidersAlive + RedSpidersAlive + BlackSpidersAlive;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnSpiderPurple(true);
            SpawnSpiderRed(true);
            SpawnSpiderBlack(true);
            enable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SpawnSpiderPurple(false);
            SpawnSpiderRed(false);
            SpawnSpiderBlack(false);
            enable = false;
        }
    }


    public bool SpawnSpiderPurple(bool enable)
    {
        if (!enable)
        {
            return false;
        }

        else
        {
            while (PurpleSpidersAlive != maxPurpleSpiders)
            {
                Instantiate(spiderPrefabPurple, spawnPoint.position, spawnPoint.rotation);
                PurpleSpidersAlive++;
            }

            return true;
        }

    }

    public bool SpawnSpiderRed(bool enable)
    {
        if (!enable)
        {
            return false;
        }

        else
        {
            while (RedSpidersAlive != maxRedSpiders)
            {
                Instantiate(spiderPrefabRed, spawnPoint.position, spawnPoint.rotation);
                RedSpidersAlive++;
            }

            return true;
        }

    }

    public bool SpawnSpiderBlack(bool enable)
    {
        if (!enable)
        {
            return false;
        }

        else
        {
            while (BlackSpidersAlive != maxBlackSpiders)
            {
                Instantiate(spiderPrefabBlack, spawnPoint.position, spawnPoint.rotation);
                BlackSpidersAlive++;
            }

            return true;
        }

    }
}
