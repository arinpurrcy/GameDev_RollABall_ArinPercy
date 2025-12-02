using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumph;
    public float jumpforce;
    private Vector3 jump;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Vector3 lastSafePosition;
    private float SavePositionDelay;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Camera maincamera;

    // Nick Variables you may want to delete if you dislike the new jump
    float lastJumpTime;
    private float jumpCooldown = 1.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lastSafePosition = transform.position;

        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
        maincamera = Camera.main;

        jump = new Vector3(0f, jumph, 0f);

    }
    public void Move(InputAction.CallbackContext movementValue)
    {
        Vector2 movementVector = movementValue.ReadValue<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void SetLastSafePosition(Vector3 newPosition)
    {
        lastSafePosition = newPosition;
    }

    public void ResetToLastSafePosition()
    {
        transform.position = lastSafePosition;
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 6)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    void FixedUpdate()
    {

        Vector3 movement = Quaternion.Euler(0f, maincamera.transform.eulerAngles.y, 0f) * new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        // if you want to delete all this new code, follow these options
        // replace "if (rb.linearVelocity.y == 0)" with "if (rb.linearVelocity.y == 0)"
        // remove the bool canJump, the entire line

        bool canJump = Time.time - lastJumpTime >= jumpCooldown; //we set the "canjump" bool to true if the last jump time was longer than the jump cooldown, aka the cooldown is over
        
        if (rb.linearVelocity.y <= 0.1) 
        {
            if (canJump && Input.GetKeyDown(KeyCode.Space)) // remove "canJump && " if delete all code
            {
                rb.AddForce(jump * jumpforce, ForceMode.Impulse);
                lastJumpTime = Time.time;   // Remove if delete all code: When you jump it starts counting how long it's been since last jump. 
            }
        }
        if (rb.linearVelocity.y == 0)
        {
            SavePositionDelay += Time.deltaTime;
            if (SavePositionDelay > 1.0f)
            {
                SetLastSafePosition(transform.position);
                SavePositionDelay = 0.0f;
            }
        }
        // rb.AddForce(jump * jumpforce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("DeathZone"))
        {
            ResetToLastSafePosition();
        }
        if (other.gameObject.CompareTag("door1"))
        {
            SceneManager.LoadScene("level1");
        }
        if (other.gameObject.CompareTag("door2"))
        {
            SceneManager.LoadScene("Level2");
        }
        if (other.gameObject.CompareTag("door3"))
        {
            SceneManager.LoadScene("Level3");
        }
        if (other.gameObject.CompareTag("door4"))
        {
            SceneManager.LoadScene("Level4");
        }
        if (other.gameObject.CompareTag("door5"))
        {
            SceneManager.LoadScene("Level5");
        }
        if (other.gameObject.CompareTag("shopdoor"))
        {
            SceneManager.LoadScene("shop");
        }
        if (other.gameObject.CompareTag("Shopexit"))
        {
            SceneManager.LoadScene("minigame");
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
}