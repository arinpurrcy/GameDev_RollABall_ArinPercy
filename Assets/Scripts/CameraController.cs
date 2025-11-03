using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float distanceToPlayer;
    private float xrotation;
    private float yrotation;
    public float xsensitivity;
    public float ysensitivity;
    private Vector2 input;
    public float minangle;
    public float maxangle;

    private void Update()
    {
        xrotation += input.x * xsensitivity * Time.deltaTime;
        yrotation -= input.y * ysensitivity * Time.deltaTime;
        yrotation = Mathf.Clamp(yrotation, minangle, maxangle);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    public void Look(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.eulerAngles = new Vector3(yrotation, xrotation, 0f);
        transform.position = player.transform.position - transform.forward * distanceToPlayer;
        
    }
}
