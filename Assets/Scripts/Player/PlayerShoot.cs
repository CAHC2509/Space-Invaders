using UnityEngine;

public class PlayerShoot : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;

    private float timeSinceLastShoot;

    private BulletPool bulletPool;

    public bool ActionEnabled { get; set; }

    public void Dependencies(BulletPool bulletPool)
    {
        this.bulletPool = bulletPool;
    }

    public void Initialize()
    {
        timeSinceLastShoot = fireRate;
        AddListeners();
    }

    public void Conclude()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        PlayerInputReader.OnShootInput += Shoot;
    }

    private void RemoveListeners()
    {
        PlayerInputReader.OnShootInput -= Shoot;
    }

    private void Shoot()
    {
        if (!ActionEnabled) return;

        if (Time.time < timeSinceLastShoot + fireRate) return;

        BulletController bullet = bulletPool.GetPlayerBullet();
        bullet.transform.position = firePoint.position;
        bullet.gameObject.SetActive(true);

        timeSinceLastShoot = Time.time;
    }
}
