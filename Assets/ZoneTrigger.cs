using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    // Start is called before the first frame update
   
    private GameManager gameManager;
    [SerializeField] private Transform childrenTransform;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.SpiderManager.zone = gameObject;
           gameManager.SpiderManager.spawnPoint = childrenTransform;
           gameManager.SpiderManager.enable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            gameManager.SpiderManager.enable = false;
        }
    }
}
