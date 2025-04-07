using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier; 
    public bool isOnGround = true;
    private Animator playerAnim; 
    public bool gameOver = false;
    private bool canDoubleJump = false;

   public bool isDashing = false;
   private float dashMultiplier = 2f;
   private float score = 0f;
   private float scoreIncreaseRate = 1f;

    public ParticleSystem explosionParticle; 
    public ParticleSystem dirtParticle; 
    
    public AudioClip JumpSound; 
    public AudioClip crashSound; 
    private AudioSource playerAudio; 

   private bool hasEnteredFrame = false;
   private float entryTargetX = 0f;
   private float entrySpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>(); 
        playerAudio = GetComponent<AudioSource>();
       
        // keeps player from moving everywhere(personal fix)
        playerRb.freezeRotation = true;

       // player starts out in the left side out of frame before walking into frame
       transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
       playerAnim.SetFloat("Speed_f", 0.5f); // walk animation

    }

    // Update is called once per frame
    void Update()
    {
        // checks if the player has entered the scene then switches from walking to running and the game will start for real
        if (!hasEnteredFrame)
       {
           transform.position = Vector3.MoveTowards(transform.position, new Vector3(entryTargetX, transform.position.y, transform.position.z), entrySpeed * Time.deltaTime);
           if (transform.position.x >= entryTargetX)
           {
               hasEnteredFrame = true;
               playerAnim.SetFloat("Speed_f", 1f); // switch to run animation
           }
           return;
       }

        //checks for space key, checks if player is on ground call for jump force, makes player jump and double jump
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround  || canDoubleJump) && !gameOver)
        {
             playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // Reset vertical velocity
             playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

             if (isOnGround)
             {
             isOnGround = false;
             canDoubleJump = true;
             }

             else
             {
                canDoubleJump = false;
             }
             
             // particles stop when the player is off the ground
             playerAnim.SetTrigger("Jump_trig");
             dirtParticle.Stop();
             playerAudio.PlayOneShot(JumpSound, 1.0f);
        }  

        if (!gameOver)
        {
            // checks when the left shift is pressed in order to activate the dash
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isDashing = true;
                score+= scoreIncreaseRate * Time.deltaTime;
                playerAnim.SetFloat("SpeedMultiplier", dashMultiplier);
            }

            else
            {
                isDashing = false;
                score+= scoreIncreaseRate * Time.deltaTime;
                playerAnim.SetFloat("SpeedMultiplier", 1f);
            }
            // keeps score every second
            Debug.Log("Score: " + Mathf.FloorToInt(score));

        }
    }


     private void OnCollisionEnter(Collision collision)
    {
        // lets player jump if its on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; 
            canDoubleJump = false;
            dirtParticle.Play();
        }

        //stops game and tells final score when the player dies
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over! Final Score:" + Mathf.FloorToInt(score));
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosionParticle.Play();
            dirtParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }


}
