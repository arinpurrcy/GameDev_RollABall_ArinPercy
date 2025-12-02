using UnityEngine;
using UnityEngine.Playables;

public class timelinetrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeline.Play();
        }
    }
}

