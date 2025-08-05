using UnityEngine;

public class MainController : ControllerBase
{
    private MainEntity mainEntity;

    public void Dependencies(MainEntity mainEntity)
    {
        this.mainEntity = mainEntity;
    }

    protected override void AddListeners()
    {
        mainEntity.OnQuitConfirm += QuitGame;
    }

    protected override void RemoveListeners()
    {
        mainEntity.OnQuitConfirm -= QuitGame;
    }

    private void QuitGame() => Application.Quit();
}
