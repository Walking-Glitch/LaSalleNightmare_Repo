using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    // audio effects
   
    public AudioClip splashGround;
    public AudioClip bloodSplashSound;

    // particle system shooting effects
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject spBlood;
    

    private GameManager gameManager;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
      gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Spider"))
        {
            collision.collider.gameObject.GetComponent<Spider_Base>().SpiderHit();
            ShotEffects(collision);

           
            gameManager.Player.RefreshDisplay(gameManager.Player.kills);
            Debug.Log(gameManager.Player.kills);
        }


        else if (collision.collider.CompareTag("Ground"))
        {
            ContactPoint contact = collision.contacts[0];
            Transform objectHit = contact.otherCollider.transform;
            Quaternion rotation = Quaternion.LookRotation(contact.normal);

         
            GameObject copy = Instantiate(water, contact.point, rotation);
            copy.transform.parent = objectHit;

             
            AudioSource.PlayClipAtPoint(splashGround, contact.point);

        }

        gameObject.SetActive(false);
    }

    private void ShotEffects(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Transform objectHit = contact.otherCollider.transform;
        Quaternion rotation = Quaternion.LookRotation(contact.normal);

        GameObject copy = Instantiate(spBlood, contact.point, rotation);
        copy.transform.parent = objectHit;

        AudioSource.PlayClipAtPoint(bloodSplashSound, contact.point);
        gameManager.Player.kills++;
    }

    private void OnDisable()
    {
        timer = 0;
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}
