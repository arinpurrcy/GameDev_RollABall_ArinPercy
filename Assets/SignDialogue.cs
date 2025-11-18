using UnityEngine;
using DialogueEditor;
public class SignDialogue : MonoBehaviour


{
    [SerializeField] private NPCConversation signconversation;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                ConversationManager.Instance.StartConversation(signconversation);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
