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

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Camera maincamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        if (rb.linearVelocity.y == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(jump * jumpforce, ForceMode.Impulse);
            }
        }
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
            SceneManager.LoadScene("minigame");
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