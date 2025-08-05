using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerComponent[] playerComponents;
    private PlayerEntity playerEntity;

    public void Dependencies(PlayerEntity playerEntity)
    {
        this.playerEntity = playerEntity;
        playerComponents = GetComponents<IPlayerComponent>();
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
