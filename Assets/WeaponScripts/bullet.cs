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

    // killed spiders counter
    [SerializeField] private Text txtKills;
    [SerializeField] public int kills = 0;
    const string preText1 = "SPIDERS KILLED: ";

    // particle system shooting effects
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject spBlood;
    [SerializeField] private Canvas aimCanvas;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Spider"))
        {
            collision.collider.gameObject.GetComponent<Spider_Base>().SpiderHit();
            ShotEffects(collision);
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

        Destroy(this.gameObject);
    }

    private void ShotEffects(Collision collision)
    {

        ContactPoint contact = collision.contacts[0];
        Transform objectHit = contact.otherCollider.transform;
        Quaternion rotation = Quaternion.LookRotation(contact.normal);

        GameObject copy = Instantiate(spBlood, contact.point, rotation);
        copy.transform.parent = objectHit;

        AudioSource.PlayClipAtPoint(bloodSplashSound, contact.point);


        kills++;
        //RefreshDisplay();
    }
}
