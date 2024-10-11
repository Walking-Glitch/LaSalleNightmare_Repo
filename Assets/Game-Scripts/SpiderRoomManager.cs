using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRoomManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 initPos; // start position

    // Start is called before the first frame update
    void Start()
    {
        initPos = player.transform.position;
        player = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initial()
    {
        player.transform.position = initPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
           // bites++;
            Initial();
            //RefreshDisplay();
        }
    }
}
