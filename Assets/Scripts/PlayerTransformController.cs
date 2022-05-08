using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformController : MonoBehaviour
{
    // script handles movement of the player
    [Header("Movement")]
    Vector3 moveH, moveV, move;
    [SerializeField] Transform playerHead;
    [SerializeField] float moveSpeed, gravity, jumpVelocity, playerJumpVelocity; // set in editor for controlling
    float gravityValue, verticalVelocity; // hidden because is calculated
    bool landed;
    [SerializeField] bool magnetised; // if we are magnetised, do not apply gravity
    // our input values
    float pAxisV, pAxisH, pAxisVL, pAxisHL;
    [SerializeField] float moveLerpTime, mouseLerpTime; // how quickly we speed up and slow down
    float finalxRotate, finalyRotate;


    [Header("Camera")]
    [SerializeField] float aimSensitivity;
    [SerializeField] float minYAngle, maxYAngle;
    float currentSensitivity, yRotate, xRotate;
    bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); } }

    private void Start()
    {
        // set our mouse lerp to be 10% of the aim sensitivity if it is 0
        if (mouseLerpTime == 0)
        { mouseLerpTime = aimSensitivity * 0.1f; }
    }


    // Update is called once per renderered frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // declare our motion
        pAxisV = Input.GetAxisRaw("Vertical");
        pAxisH = Input.GetAxisRaw("Horizontal");

        // lets lerp our input movement values so that the movement is smoother

        pAxisVL = Mathf.Lerp(pAxisVL, pAxisV, moveLerpTime * Time.deltaTime);
        pAxisHL = Mathf.Lerp(pAxisHL, pAxisH, moveLerpTime * Time.deltaTime);
        moveV = playerHead.forward * pAxisVL;
        moveH = playerHead.right * pAxisHL;

        RaycastHit groundedHit;
        Physics.Raycast(transform.position, Vector3.down, out groundedHit, 1.5f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        // movement application
        // jump calculations
        if (!magnetised)
        { // if we are not magnetised to a surface, then apply gravity
            gravityValue = gravity;
        }
        else if (magnetised)
        {
            gravityValue = 0;
            gravity = 0;
        }

        if (groundedHit.transform == null)
        {
            playerJumpVelocity += gravityValue * Time.deltaTime;
            landed = false;
        }
        else if (groundedHit.transform != null)
        {
            // jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerJumpVelocity = Mathf.Sqrt(jumpVelocity * -3.0f * gravity);
            }
            else if (!landed)
            {
                playerJumpVelocity = 0f;
                landed = true;
            }
        }

        verticalVelocity = playerJumpVelocity;

        move = new Vector3((moveH.x + moveV.x), verticalVelocity / moveSpeed, (moveH.z + moveV.z));
        transform.position += (move * Time.deltaTime * moveSpeed);

        // check our dynamic bool to see if our mouse is on the screen
        if (IsMouseOverGameWindow)
        {
            // our camera control
            currentSensitivity = aimSensitivity;
            // run math to rotate the head of the player as we move the mouse
            yRotate += (Input.GetAxis("Mouse Y") * -currentSensitivity * Time.deltaTime);
            // clamp the rotation so we don't go around ourselves
            yRotate = Mathf.Clamp(yRotate, minYAngle, maxYAngle);
            // calculate our X rotation
            xRotate += (Input.GetAxis("Mouse X") * currentSensitivity * Time.deltaTime);
            // add in our rotate mods if we have any
            finalxRotate = Mathf.Lerp(finalxRotate, xRotate, mouseLerpTime * Time.deltaTime);
            finalyRotate = Mathf.Lerp(finalyRotate, yRotate, mouseLerpTime * Time.deltaTime);
            // apply it to our head
            playerHead.eulerAngles = new Vector3(finalyRotate, finalxRotate, 0f);
        }
    }

}
