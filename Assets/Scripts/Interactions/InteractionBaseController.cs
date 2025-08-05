using System;
using UnityEngine;

public abstract class InteractionBaseController : MonoBehaviour
{
    public event Action InteractionBeginCompleted;
    public event Action InteractionFinishCompleted;

    public abstract void Initialize();
    public abstract void BeginInteraction();
    public abstract void FinishInteraction();
    public abstract void Conclude();
}
