using System.Collections;
using UnityEngine;

public abstract class PooledObject: MonoBehaviour
{
    [SerializeField] protected float disablingDelay = 5f;

    protected virtual void OnEnable() => StartCoroutine(DisablingCoroutine(disablingDelay));

    private IEnumerator DisablingCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
