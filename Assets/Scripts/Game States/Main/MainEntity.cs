using System;

public class MainEntity
{
    public event Action OnPlayPressed;
    public event Action OnQuitPressed;
    public event Action OnQuitConfirm;
    public event Action OnQuitCancel;

    public void PlayPressed() => OnPlayPressed?.Invoke();
    public void QuitPressed() => OnQuitPressed?.Invoke();
    public void QuitConfirmed() => OnQuitConfirm?.Invoke();
    public void QuitCanceled() => OnQuitCancel?.Invoke();
}
