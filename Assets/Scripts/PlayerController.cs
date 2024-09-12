using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horsePower;
    private float turnSpeed = 20.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] float speed;

    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;
  
    // Update is called once per frame
    void  Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }
    void FixedUpdate()
    {
        if (IsOnGround())
        {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput  = Input.GetAxis("Vertical");
        // we 'll move the vehicle forward
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        // we rotate the vehicle 
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);

        speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // 3.6 for kph
        speedometerText.SetText("Speed: " + speed + "mph");

        rpm = (speed % 30) * 40;
        rpmText.SetText("Rpm: " + rpm);
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
            wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
