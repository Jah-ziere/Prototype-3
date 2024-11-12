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
    public ParticleSystem explosionParticle; 
    public ParticleSystem dirtParticle; 
    public AudioClip JumpSound; 
    public AudioClip crashSound; 
    private AudioSource playerAudio; 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>(); 
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks for space key, checks if player is on ground call for jump force, makes player jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
             playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
             isOnGround = false;
             playerAnim.SetTrigger("Jump_trig");
             dirtParticle.Stop();
             playerAudio.PlayOneShot(JumpSound, 1.0f);
        }  
    }


     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; 
            dirtParticle.Play();
        }

        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosionParticle.Play();
            dirtParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }


}
