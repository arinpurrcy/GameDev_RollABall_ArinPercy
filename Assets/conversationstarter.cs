using UnityEngine;
using DialogueEditor;
public class conversationstarter : MonoBehaviour


{
    [SerializeField] private NPCConversation shopConversation1;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                ConversationManager.Instance.StartConversation(shopConversation1);
            }
        }

    }
}
