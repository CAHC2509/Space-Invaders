using UnityEngine;

public abstract class ViewBase : MonoBehaviour
{
    [SerializeField] protected GameObject viewParent;

    public virtual void Initialize() => AddListeners();
    public virtual void Conclude() => RemoveListeners();
    public virtual void EnableView() => viewParent.SetActive(true);
    public virtual void DisableView() => viewParent.SetActive(false);

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
