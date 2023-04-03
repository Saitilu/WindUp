using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private PlayerController playerController;
    private PlatformEffector2D effector;
    private float waitTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("MyPlayer").GetComponent<PlayerController>();
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.inputVector.y < -100)
        {
            effector.rotationalOffset = 180f;
            waitTime = .5f;
        }
        else if (waitTime <= 0)
        {
            waitTime = 0;
            effector.rotationalOffset = 0f;
        }
        else
            waitTime -= Time.deltaTime;

    }
}
