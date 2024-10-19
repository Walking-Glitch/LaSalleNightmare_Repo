using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> bulletList;

    void Start()
    {
        AddEnemiesToPool(poolSize);
    }

    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
                GameObject tempBullet = bulletPrefab;
                GameObject BulletClone = Instantiate(tempBullet);
                BulletClone.SetActive(false);
                bulletList.Add(BulletClone);
                BulletClone.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {

        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                return bulletList[i];
            }

        }
        return null;

    }
}
