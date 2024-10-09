using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> spiderPrefabs;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> SpiderList;

    void Start()
    {
        AddEnemiesToPool(poolSize);
    }

    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if ((i + 1) % 6 == 0)
            {
                GameObject spiderPrefab = spiderPrefabs[2]; // 6th spider
                GameObject spider = Instantiate(spiderPrefab);
                spider.SetActive(false);
                SpiderList.Add(spider);
                spider.transform.parent = transform;
            }
            else if ((i + 1) % 3 == 0)
            {
                GameObject spiderPrefab = spiderPrefabs[0]; // 3rd spider
                GameObject spider = Instantiate(spiderPrefab);
                spider.SetActive(false);
                SpiderList.Add(spider);
                spider.transform.parent = transform;
            }
            else
            {
                GameObject spiderPrefab = spiderPrefabs[1]; // Other spiders
                GameObject spider = Instantiate(spiderPrefab);
                spider.SetActive(false);
                SpiderList.Add(spider);
                spider.transform.parent = transform;
            }

        }
    }

    public GameObject RequestSpider()
    {
     
        for (int i = 0; i < SpiderList.Count; i++)
        {
            if (!SpiderList[i].activeSelf)
            {
                SpiderList[i].GetComponent<NavMeshAgent>().enabled = false;
                return SpiderList[i];
            }

        }
        return null;

    }

}
