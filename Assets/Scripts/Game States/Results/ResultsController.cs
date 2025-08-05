using UnityEngine;

public class ResultsController : MonoBehaviour
{
    private ResultsEntity resultsEntity;

    public void Dependencies(ResultsEntity resultsEntity)
    {
        this.resultsEntity = resultsEntity;
    }
}
