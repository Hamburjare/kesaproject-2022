using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ampuminen : MonoBehaviour
{


    [SerializeField]
    private GameObject luoti;

    [SerializeField]
    private Transform luotiSpawn;

    [SerializeField]
    private float minShootWaitTime = 1f, maxShootWaitTime = 3f;

    private float waitTime;

    [SerializeField]
    private List<LuotiController> bullets;

    private bool canShoot;
    private int bulletIndex;

    [SerializeField]
    private int initialBulletCount = 2;

    private Transform playerTransform;

    private Vector3 direction;
    private float angle;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        for (int i = 0; i < initialBulletCount; i++)
        {
            // while instantiating the bullet game object also get the SpiderBullet component
            bullets.Add(Instantiate(luoti).GetComponent<LuotiController>());
            bullets[i].gameObject.SetActive(false);
        }

        waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime);
    }

    private void Update()
    {
        FacePlayersDirection();
    }

    void Shoot()
    {
        canShoot = true;
        bulletIndex = 0;

        while (canShoot)
        {
            if (!bullets[bulletIndex].gameObject.activeInHierarchy)
            {
                bullets[bulletIndex].gameObject.SetActive(true);

                // set the rotation of the bullet to the rotation of the spider(parent game object)
                bullets[bulletIndex].transform.rotation = transform.rotation;
                bullets[bulletIndex].transform.position = luotiSpawn.position;

                // call the shoot bullet function to shoot the bullet
                bullets[bulletIndex].ShootBullet(transform.up);

                canShoot = false;
            }
            else
            {
                bulletIndex++;
            }

            if (bulletIndex == bullets.Count)
            {
                // while
                bullets.Add(Instantiate(luoti, luotiSpawn.position, transform.rotation).GetComponent<LuotiController>());

                // access the bullet we just created by subtracting 1 from
                // the total bullet count in the list
                bullets[bullets.Count - 1].ShootBullet(playerTransform.position);

                canShoot = false;
            }
        }
    }

    void FacePlayersDirection()
    {
        direction = playerTransform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);

        if (Time.time > waitTime)
        {
            waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime);
            Shoot();
        }
    }

} // class