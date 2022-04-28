/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Aidan Pohl
 * Last Edited: April 28, 2022
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{   /**VARIABLES**/
    [Header("Paddle Variables")]
    public float speed = 10; //speed of paddle

    /**METHODS**/
    // Update is called once per frame
    void Update()
    {
        Vector3 paddlePos = transform.position;                              //Gets current position
        paddlePos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime; //Adds horizontal input to x multiplied by speed (scaled inversely to frame rate to maintain consistency)
        transform.position = paddlePos;                                      //Updates position with new values
    }//end Update()
}
