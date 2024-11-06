using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; 
    private float repeatWidth; 
    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //resets background if it moves a certain distance
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }


}
