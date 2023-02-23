using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    //// Spawn Spiders 
    //// https://www.youtube.com/watch?v=n2DXF1ifUbU&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=7
    /// SpawnRandom [] 
    /// https://www.youtube.com/watch?v=6jJ6T8M_DcM
    /// </summary>

    //public static int spidersAlive = 0;

    public Transform spiderPrefabPurple;  
    public Transform spiderPrefabRed; 
    public Transform spiderPrefabBlack;

    public Transform spawnPoint1;   
   // public Transform spawnPoint2;

    public float timeBetweenWaves = 5.0f;
    [SerializeField] private float countDown = 2.0f;
    
    [SerializeField] private int waveIndex = 1;

    private bool test;


    private void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWavePurple());
            StartCoroutine(SpawnWaveRed());
            StartCoroutine(SpawnWaveBlack());
            //SpawnWave();
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
    }

    public IEnumerator SpawnWavePurple()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnSpiderPurple(test);
            yield return new WaitForSeconds(0.5f);
        }
        waveIndex++;
    }

    public IEnumerator SpawnWaveRed()
    {
        for (int i = 0; i < waveIndex; i++)

        {
            SpawnSpiderRed(test);
            yield return new WaitForSeconds(3.0f);
        }
    }

    public IEnumerator SpawnWaveBlack()
    {
        for (int i = 0; i < waveIndex; i++)

        {
            SpawnSpiderBlack(test);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnSpiderPurple(true);
            SpawnSpiderRed(true);
            SpawnSpiderBlack(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnSpiderPurple(false);
            SpawnSpiderRed(false);
            SpawnSpiderBlack(false);
        }
    }

    public bool SpawnSpiderPurple(bool enable)
    {        
        //enable = true;
        if (enable == true)
        {
            Instantiate(spiderPrefabPurple, spawnPoint1.position, spawnPoint1.rotation);
            //Instantiate(spiderPrefabPurple, spawnPoint2.position, spawnPoint2.rotation);

            test = true;
            return true;
        }
        else if (enable == false)
        {
            //Instantiate(spiderPrefab, spawnPoint.position, spawnPoint.rotation);
            test = false;
            return false;
        }
        return enable;
    }

    public bool SpawnSpiderRed(bool enable)
    {
        //enable = true; 
        if (enable == true)
        {

            Instantiate(spiderPrefabRed, spawnPoint1.position, spawnPoint1.rotation);
            // Instantiate(spiderPrefabRed, spawnPoint2.position, spawnPoint2.rotation);
            test = true;
            return true;
        }
        else if (enable == false)
        {
            test = false;
            return false;
        }
        return enable;
    }

    public bool SpawnSpiderBlack(bool enable)
    {
        //enable = true; 
        if (enable == true)
        {

            Instantiate(spiderPrefabBlack, spawnPoint1.position, spawnPoint1.rotation);
            // Instantiate(spiderPrefabBlack, spawnPoint2.position, spawnPoint2.rotation);

            test = true;
            return true;
        }
        else if (enable == false)
        {
            test = false;
            return false;
        }
        return enable;
    }



    ////public GameObject spidersPrefab;

    //public int count;
    //public float rate;

    //public Wave[] waves;

    //public Transform spiderPrefab;

    ////public GameObject spiderPrefab; 

    //public Transform spawnPoint;
    ////public Transform spawnPoint1;
    ////public Transform spawnPoint2;
    ////public Transform spawnPoint3;

    //public float timeBetweenWaves = 5.0f;
    ////keeps track of when the next wave is spawn
    //private float countdown = 2.0f;

    //private int waveIndex = 0;

    //private void Update()
    //{
    //    ////if (spidersAlive > 0)
    //    ////{
    //    ////    return;
    //    ////}

    //    //SpawnSpiders();
    //    if (countdown <= 0.0f)
    //    {
    //        StartCoroutine(SpawnWave());
    //        countdown = timeBetweenWaves;
    //    }
    //    countdown -= Time.deltaTime;
    //}

    //// it waits to spawn the next enemy
    //IEnumerator SpawnWave()
    //{
    //    //Wave wave = waves[waveIndex];

    //    //spidersAlive = wave.count;

    //    for (int i = 0; i < count; i++)
    //    {
    //        //SpawnSpiders();
    //        yield return new WaitForSeconds(1.0f / rate);
    //    }
    //    waveIndex++;

    //    //if (waveIndex == waves.Length)
    //    //{
    //    //    Debug.Log("End Level");
    //    //    this.enabled = false;
    //    //}
    //}

    //void SpawnSpiders(GameObject spiders)
    //{
    //    Instantiate(spiders, spawnPoint.position, spawnPoint.rotation);
    //    //spidersAlive++;
    //    //Instantiate(spiderPrefab, spawnPoint1.position, spawnPoint1.rotation);
    //    //Instantiate(spiderPrefab, spawnPoint2.position, spawnPoint2.rotation);
    //    //Instantiate(spiderPrefab, spawnPoint3.position, spawnPoint3.rotation);
    //}


    ////void OnGUI()
    ////{
    ////    //https://docs.unity3d.com/ScriptReference/Random.Range.html
    ////    if (GUI.Button(new Rect(10, 10, 100, 50), "Instantiate!"))
    ////    {
    ////        var position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
    ////        Instantiate(spiderPrefab, position, Quaternion.identity); 
    ////    }
    ////    //Instantiate(spiderPrefab, spawnPoint.position, spawnPoint.rotation); 
    ////}


}
