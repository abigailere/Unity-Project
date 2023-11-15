using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   // Debug.Log("Space key was pressed down"); //hwo to write to console
public class Player : MonoBehaviour
{ 
   
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    [SerializeField] private Transform groundCheckTransform = null; //adding serializefield, will expose this variable to the unity console without making it public
    [SerializeField]private LayerMask playerMask;
    int superJumpsRemaining;

    void Start() // Start is called before the first frame update
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check jump in every frame
        //any keypressed check should be written in update, not fixed update
        if(Input.GetKeyDown(KeyCode.Space)){// getkeydown returns true only once, when key is down, getkey is true while key is held down
        jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }
    //FixedUpdate is called once every physics update
    void FixedUpdate(){
         rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0); //only changing the x-comp 

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0){  //equals 1 because in the air, its only colliding with itself
        return;
        }

        if(jumpKeyPressed == true){
            float jumpPower = 5;
            if(superJumpsRemaining > 0){
                jumpPower *= 2;
                superJumpsRemaining--;
            }

            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange); 
            jumpKeyPressed = false;
        }

       
    }

   //collecting coins
    void OnTriggerEnter(Collider other){ //other is the trigger we are colliding with in this case the coin
    if(other.gameObject.layer == 7){ 
        Destroy(other.gameObject);
        superJumpsRemaining++;
    }
    }

}
