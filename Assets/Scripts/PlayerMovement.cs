using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float move;

    public float jump;
    public bool isJumping;

    [SerializeField] public TextMeshProUGUI ScoreText;
    private int scoreCounter;
    // Start is called before the first frame update

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scoreCounter = PlayerPrefs.GetInt("score");
        ScoreText.text = "Score: " + scoreCounter;
        Debug.Log(PlayerPrefs.GetInt("score"));
        Debug.Log(PlayerPrefs.GetInt("level"));
        Debug.Log(PlayerPrefs.GetString("username"));

    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        PlayerPrefs.SetInt("score", scoreCounter);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (other.gameObject.CompareTag("Death"))
        {
            scoreCounter++;
            ScoreText.text = "Score: " + scoreCounter;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

}
