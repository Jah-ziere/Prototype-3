using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 5;
    private PlayerController playerControllerScript; 
    private float leftBound = -15;
    private float dashMultiplier = 2f;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            float currentSpeed = playerControllerScript.isDashing ? speed * dashMultiplier : speed;
            transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);
        }
// destroys out of bounds obstacles
        if (transform.position.x < leftBound && !gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }
}
