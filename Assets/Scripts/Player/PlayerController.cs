using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;

    private IPlayerComponent[] playerComponents;
    private PlayerEntity playerEntity;

    public void Dependencies(PlayerEntity playerEntity, BulletPool bulletPool)
    {
        this.playerEntity = playerEntity;
        playerComponents = GetComponents<IPlayerComponent>();
        playerShoot.Dependencies(bulletPool);
    }

    public void Initialize()
    {
        foreach (IPlayerComponent component in playerComponents)
            component.Initialize();
    }

    public void Conclude()
    {
        foreach (IPlayerComponent component in playerComponents)
            component.Conclude();
    }

    public void EnableActions()
    {
        foreach (IPlayerComponent component in playerComponents)
            component.ActionEnabled = true;
    }

    public void DisableActions()
    {
        foreach (IPlayerComponent component in playerComponents)
            component.ActionEnabled = false;
    }
}
