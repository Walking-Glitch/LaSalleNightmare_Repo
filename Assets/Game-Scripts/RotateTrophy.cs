using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class RotateTrophy : MonoBehaviour
{
    //https://www.youtube.com/watch?v=xk0YFoqXPtI

    // to rotate the trophy 
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    //public Death timer; 

    //[SerializeField] private Canvas canvasWin;

    private void Start()
    {
        //  canvasWin.enabled = false;
        //timer = GetComponent<Death>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //canvasWin.enabled = true; 

            Destroy(gameObject);

            //timer.SaveTime(); 

            //SceneManager.LoadScene("LoadingScene"); 

        }
    }
}
