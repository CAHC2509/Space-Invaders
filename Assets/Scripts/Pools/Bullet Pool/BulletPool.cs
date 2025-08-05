using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Player bullets")]
    [SerializeField] private BulletController playerBulletPrefab;
    [SerializeField] private int playerBulletsAmount = 10;

    private List<BulletController> playerBullets;

    public void Initialize()
    {
        CreatePlayerBulletsPool();
    }

    public void Conclude()
    {
        DestroyPlayerBulletsPool();
    }

    private void CreatePlayerBulletsPool()
    {
        playerBullets = new List<BulletController>();
        for (int i = 0; i < playerBulletsAmount; i++)
        {
            playerBullets.Add(Instantiate(playerBulletPrefab.gameObject, transform).GetComponent<BulletController>());
            playerBullets[i].gameObject.SetActive(false);
        }
    }

    private void DestroyPlayerBulletsPool()
    {
        foreach (BulletController bullet in playerBullets)
            Destroy(bullet.gameObject);
    }

    public BulletController GetPlayerBullet()
    {
        foreach (BulletController bullet in playerBullets)
        {
            if (!bullet.gameObject.activeSelf)
                return bullet;
        }

        BulletController newBullet = Instantiate(playerBulletPrefab.gameObject, transform).GetComponent<BulletController>();
        playerBullets.Add(newBullet);
        return newBullet;
    }
}
