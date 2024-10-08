using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderManager : MonoBehaviour
{
    //public Transform parentSpawnPoint;

    [SerializeField] private List<Transform> spawnPointsList = new List<Transform>();

    public int enemyCtr;
    public int maxEnemy;
    public float delay;

    public Transform spawnPoint;
    public GameObject zone; 

    private bool isSpawning;
    public bool enable;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        enemyCtr = 0;
    
    }
    // Update is called once per frame
    void Update()
    {
        if (enable && enemyCtr < maxEnemy)
        {
            if(!isSpawning) StartCoroutine(SpawnSpiders());
        }
    }
    private IEnumerator SpawnSpiders()
    {
        isSpawning = true;

        GameObject tempEnemy = gameManager.SpiderPool.RequestSpider();
        tempEnemy.transform.position = spawnPoint.position;
        tempEnemy.transform.rotation = spawnPoint.rotation;
        tempEnemy.GetComponent<NavMeshAgent>().enabled = true;
        tempEnemy.SetActive(true);

        enemyCtr++;

        yield return new WaitForSeconds(delay);
        isSpawning = false;
    }

    public void DecreaseEnemyCtr()
    {
        enemyCtr--;
    }
}

