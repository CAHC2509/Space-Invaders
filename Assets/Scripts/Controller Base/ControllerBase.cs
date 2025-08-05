using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public virtual void Initialize() => AddListeners();
    public virtual void Conclude() => RemoveListeners();

    protected abstract void AddListeners();
    protected abstract void RemoveListeners();
}
