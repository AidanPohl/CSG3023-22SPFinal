/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Aidan Pohl
 * Last Edited: Apr 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{   

    /**VARIABLES**/
    [Header("General Settings")]
    public int numberOfBalls;           //acts as lives, when no balls, game over
    public int score;                   //current game score
    public Text ballTxt;                //Reference to ball HUD Textbox
    public Text scoreTxt;               //Reference to score HUD textbox
    public GameObject paddle;           //Reference to paddle game object


    [Header("Ball Settings")]
    public float speed;                 //speed of ball, constant
    public float initialForceY;         //vertical force applied by the paddle on the ball

    //Set in script
    public bool isInPlay;              //test if bal; is in play
    private Rigidbody rb;               //Reference to ball ridigbody
    private AudioSource audioSource;    //audio source for ball bounce (set in script)



 


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        //Updates UI
        ballTxt.text = "Balls: " + numberOfBalls;   //updates ball Text UI with current number of balls
        scoreTxt.text = "Score: " + score;          //updates score Text UI with current score;

        if (!isInPlay)//If ball not in play
        {   
            //Move the ball with the paddle
            Vector3 ballPos = transform.position;   //Get current ball position
            ballPos.x = paddle.transform.position.x;//Sets the x value to paddle's current x position;
            transform.position = ballPos;                //Applies modification to ball position;

            if (Input.GetKeyDown(KeyCode.Space))//Ball not in play and spacebar is pressed
            {
                isInPlay = true;                    //Set ball to be in play
                Move();                             //Begin moving the ball
            }//end if (Input.GetKeyDown(Key.SPACE))

        }//end if (!isInPlay)

    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)//If ball currently in play
        {
            Vector3 velocity = rb.velocity;     //gets rb velocity
            velocity.Normalize();               //Normalizes the velocity
            velocity *= speed;                  //multiplies normalized velocity by speed;
            rb.velocity = velocity;             //applies new velocity
        }// end if(isInPlay)
    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;                                                       //ball is not in play
        rb.velocity = Vector3.zero;                                             //set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x;                                    //x position of paddle
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y;    //Y position of paddle plus it's height

        transform.position = pos;                                               //set starting position of the ball 
    }//end SetStartingPos()

    void Move()
    {
        Debug.Log("Moving ball");
        rb.AddForce(0, initialForceY,0);         //adds a vertical initialForce to the ball
    }//end Move()

    void OnCollisionEnter(Collision other)
    {

        if (isInPlay) { audioSource.Play(); }                     //Plays the audioSource if ball currently bouncing around
        GameObject otherGO = other.gameObject;  //gets the gameObject of the collision
        if(otherGO.tag == "Brick")//if otherGO is a "Brick"
        {
            score += 100;                       // adds 100 to the score
            Destroy(otherGO);                   // destroys the collided with gameObject
        }//end if(otherGO.tag == "Brick")
    } //end OnColliderEnter()

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "OutBounds") //if ball has left bounds
        {
            numberOfBalls--;                    //deincriments the number of balls
            if (numberOfBalls > 0)              //if player has more balls
            {
                Invoke("SetStartingPos", 2f);   //Calls SetStartingPos() after 2 second delay;
            }//end if (numberOfBalls > 0)

        }//end if(other.gameObject.tag == "OutBounds")

    }//end void OnTriggerEnter(Collider other)






}
