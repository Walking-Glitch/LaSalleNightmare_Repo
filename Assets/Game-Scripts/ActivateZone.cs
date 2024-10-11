using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateZone : MonoBehaviour
{
    private EnemyWaves spiders;
    //[SerializeField] private BoxCollider col; 
    //[SerializeField] private string zone; 
    //[SerializeField] private bool inside = false;
    //[SerializeField] private bool outside = true; 


    //private BoxCollider box;

    public void Start()
    {
        //spiders = GetComponent<EnemyWaves>();
        //box = GetComponent<BoxCollider>(); 
        //spiders = (EnemyWaves)GetComponent<EnemyWaves>().SpawnWave();
        spiders = GameObject.Find("MiniManager").GetComponent<EnemyWaves>(); 
    }

    //public void Awake()
    //{
    //    stopSpiders = true;
    //    activeSpiders = false; 
    //}

    //public bool SpawnSpiderPurple(bool enable)
    //{
    //    //enable = true;
    //    if (enable == true)
    //    {
    //        Instantiate(spiderPrefabPurple, spawnPoint1.position, spawnPoint1.rotation);
    //        //Instantiate(spiderPrefabPurple, spawnPoint2.position, spawnPoint2.rotation);

    //        test = true;
    //        return true;
    //    }
    //    else if (enable == false)
    //    {
    //        //Instantiate(spiderPrefab, spawnPoint.position, spawnPoint.rotation);
    //        test = false;
    //        return false;
    //    }
    //    return enable;
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter Zone");
            
            //spiders.SpawnSpiderPurple(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("ExitZone");
            spiders.SpawnSpiderPurple(false);
        }
    }
}


///////////////////////////////////*using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ActivateZone : MonoBehaviour
//{
//    private EnemyWaves spiders;

//    public void Start()
//    {
//        spiders = GetComponent<EnemyWaves>();
//        //spiders = (EnemyWaves)GetComponent<EnemyWaves>().SpawnWave();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Enter Zone");
//            //spiders.spawnPoint.gameObject.SetActive(true);
//            spiders.enabled = true;

//        }
//    }

//    //private void OnTriggerStay(Collider other)
//    //{
//    //    if (other.gameObject.tag == "Player")
//    //    {
//    //        Debug.Log("Stay Zone");
//    //        //spiders.spawnPoint.gameObject.SetActive(true);
//    //        spiders.enabled = true;
//    //    }
//    //}

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("ExitZone");
//            //spiders.spawnPoint.gameObject.SetActive(false);
//            spiders.enabled = false;
//        }
//    }
//}
//*//