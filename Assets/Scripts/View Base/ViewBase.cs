using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ViewBase : MonoBehaviour
{
    [SerializeField] protected GameObject viewParent;

    protected GameObject defaultSelection;

    public virtual void Initialize() => AddListeners();
    public virtual void Conclude() => RemoveListeners();
    public virtual void EnableView() { viewParent.SetActive(false); EventSystem.current.SetSelectedGameObject(defaultSelection); }
    public virtual void DisableView() => viewParent.SetActive(false);

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
