using UnityEngine;
using UnityEngine.Events;

public class EndGameTrigger : MonoBehaviour
{
    public UnityEvent OnEndGameTriggered = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour player = other.GetComponentInParent<PlayerBehaviour>();
        if (player != null)
        {
            OnEndGameTriggered.Invoke();
            Debug.Log("End game triggered");
        }
    }
}
