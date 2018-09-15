using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public string collectibleTag = "Coin";
    public GameObject collectEffect;

    public float speed;
    public float jumpSpeed;
    public float maxDepth;


    public Transform spawnPos;
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundMask;
    public float checkRadius;
    public Transform groundCheck;
    public GameObject canvas;

    public AudioSource walkAudio;
    public AudioSource jumpAudio;

    private bool isGrounded = false;
    private float movement;
    private bool flipped = false;
    public Transform checkpoint;

    public static bool gameIsRunning;
    
    public int collected = 0; //How many puzzles player collected

	void Start () {
        Debug.Log("Start");

        //Get the components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Disable cursor
        Cursor.visible = false;
        gameIsRunning = true;
        //Set checkpoint to spawn
        if (checkpoint == null)
            checkpoint = spawnPos;
        //Place player in correct place
        Respawn();
    }

    private void FixedUpdate()
    {
        //Do nothing if game is not running
        if (!gameIsRunning)
            return;

        //Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);
        //Apply velocity
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        //Adjust animator walk speed
        animator.SetFloat("speed", speed * Mathf.Abs(movement));

        //Toggle between walk animation depending on player movement
        if (movement != 0f && isGrounded)
        {
            animator.SetBool("IsWalking", true);
    
            if(!walkAudio.isPlaying)
                walkAudio.Play();
        }
        else
        {
            animator.SetBool("IsWalking", false);
            if(walkAudio.isPlaying)
                walkAudio.Stop();
        }
    }

    //Toggle pause
    public void Toggle()
    {
        gameIsRunning = !gameIsRunning;
        canvas.SetActive(!canvas.activeSelf);
        Cursor.visible = !Cursor.visible;
    }
    public void Quit()
    {
        Application.Quit();
    }

  
    // Update is called once per frame
    void Update ()
    {
        
        //Open pause menu if hit Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }

        //Do nothing if game is not running
        if (!gameIsRunning)
            return;

        //If fell off, restart
        if (transform.position.y < maxDepth)
            Restart();

        #region Movement

        if (Input.touches.Length == 0)
        {
            movement = 0;
            movement = Input.GetAxisRaw("Horizontal");
        }
        else if (Input.touches.Length == 1)
        {
            Vector2 pos = Input.GetTouch(0).position;
            if (pos.x < Screen.width / 2) movement = -1;
            else movement = 1;
        }
        else if (Input.touches.Length == 2)
        {
            Jump();
            movement = 0;
        }

        
        //Flip to make player face correct direction
        if (movement > 0 && flipped)
            Flip();
        if (movement < 0 && !flipped)
            Flip();
           
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
#endregion
    }

    //Flip player  = rotate around Y axis for 180 degrees
    void Flip()
    {
        flipped = !flipped;
        transform.Rotate(new Vector3(0,180,0));
    }

    public  void Restart()
    {
        //If died, fade out and respawn
        SceneChanger.instance.FadeOut();
        Invoke("Respawn",1f);
    }

    void Respawn()
    {
        //Reset player's position to the last checkpoint
        SceneChanger.instance.FadeIn();
        transform.position = checkpoint.position;
    }


    void Jump()
    {
        //Dont jump if mid air
        if (!isGrounded)
            return;
        jumpAudio.Play();
        //Apply speed 
        rb.velocity = Vector2.up * jumpSpeed;
        //Call animation
        animator.SetTrigger("Jump");
    }

    public void SetCheckpoint(Transform point)
    {
        if (checkpoint == point)
            return;
        Debug.Log("checkpoint1");
        checkpoint = point;
        Debug.Log(checkpoint);
    }

    //Interact with exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameIsRunning)
            return;
           
        if(collision.CompareTag(collectibleTag))
        {
            Collect(collision.gameObject);
        }
        if(collision.CompareTag("Exit"))
        {
            //Stop the game
            gameIsRunning = false;
            //End game if touched exit door
            GameManager.instance.EndGame();
            //Destroy the object
            Destroy(gameObject, 0.2f);
        }
    }

    //Draw feet collision wireframe
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    void Collect(GameObject collectible)
    {
        Debug.Log("Collected!");
        collected += 1;
        GameObject effect = (GameObject)Instantiate(collectEffect, collectible.transform.position, Quaternion.identity);
        Destroy(collectible);
        Destroy(effect, 2f);
    }
}
