using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;
    public float jumpforce;
    public Text countText;          
    public Text winText;
    public Text livesText;
    private int count;
    private int lives;
    public Transform teleportPoint;
    public Camera cam;
    public Animator anim;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        SetCountText();
        SetLivesText();
        winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    //physics
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2 (0, jumpforce), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag ("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        } else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count == 4)
        {
            this.gameObject.transform.position = teleportPoint.position;
            //Camera.main.transform.position = new Vector3(-20f, 0f, -10f);
            lives = 3;
            SetLivesText();
        }

        if (count >= 8)
            winText.text = "You win!";
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            winText.text = "You lose!";
            gameObject.SetActive(false);
        }
 
    }
}
