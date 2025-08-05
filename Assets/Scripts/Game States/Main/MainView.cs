using UnityEngine;
using UnityEngine.UI;

public class MainView : ViewBase
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmQuitButton;
    [SerializeField] private Button cancelQuitButton;
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject quitWindow;

    private MainEntity mainEntity;

    public void Dependencies(MainEntity mainEntity)
    {
        this.mainEntity = mainEntity;
        defaultSelection = playButton.gameObject;
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        playButton.onClick.AddListener(() => mainEntity.PlayPressed());
        quitButton.onClick.AddListener(() => mainEntity.QuitPressed());
        confirmQuitButton.onClick.AddListener(() => mainEntity.QuitConfirmed());
        cancelQuitButton.onClick.AddListener(() => mainEntity.QuitCanceled());

        mainEntity.OnQuitPressed += ShowQuitWindow;
        mainEntity.OnQuitCancel += HideQuitWindow;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        playButton.onClick.RemoveListener(() => mainEntity.PlayPressed());
        quitButton.onClick.RemoveListener(() => mainEntity.QuitPressed());
        confirmQuitButton.onClick.RemoveListener(() => mainEntity.QuitConfirmed());
        cancelQuitButton.onClick.RemoveListener(() => mainEntity.QuitCanceled());

        mainEntity.OnQuitPressed -= ShowQuitWindow;
        mainEntity.OnQuitCancel -= HideQuitWindow;
    }

    private void ShowQuitWindow()
    {
        mainWindow.SetActive(false);
        quitWindow.SetActive(true);
    }
    private void HideQuitWindow()
    {
        quitWindow.SetActive(false);
        mainWindow.SetActive(true);
    }
}
