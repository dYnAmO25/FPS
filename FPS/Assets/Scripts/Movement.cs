using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject goEscapeMenu;
    [SerializeField] float fSpeed;
    [SerializeField] float fSense;
    [SerializeField] float fJumpForce;
    [SerializeField] float fBunnyHopBoost;
    [SerializeField] float fMaxAngle;
    [SerializeField] float fGroundDrag;
    [SerializeField] float fAirDrag;
    [SerializeField] float fNerfedSpeedMultiplyer;
    [SerializeField] GameObject GameScreen;

    private float fRotationY;
    private float fRotationX;

    private float fCurrentSpeed;

    private Rigidbody rig;

    private bool bGrounded;
    private bool bJumpAllowed = true;

    private float fMultiplyerSpeed;

    private bool bAllowMovement;

    public bool bEscapeMenu = false;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateSpeed();
        GroundCheck();
        if (bEscapeMenu == false)
        {
            Jump();
            Rotation();
            Move();
        }
        SetDrag();
        Strafe();
        SetVelText();
        GetSense();
        ResetScene();
        ManageEscape();
    }

    private void Rotation()
    {
        float fMouseX = Input.GetAxisRaw("Mouse X") * fSense * Time.deltaTime;
        float fMouseY = Input.GetAxisRaw("Mouse Y") * fSense * Time.deltaTime;

        fRotationY += fMouseX;

        fRotationX -= fMouseY;
        fRotationX = Mathf.Clamp(fRotationX, -fMaxAngle, fMaxAngle);

        cam.transform.rotation = Quaternion.Euler(new Vector3(fRotationX, fRotationY, 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, fRotationY, 0));
    }

    private void Move()
    {
        if (bAllowMovement)
        {
            fCurrentSpeed = fSpeed + (fMultiplyerSpeed * fBunnyHopBoost);
        }
        else
        {
            fCurrentSpeed = (fSpeed * fNerfedSpeedMultiplyer) + (fMultiplyerSpeed * fBunnyHopBoost);
        }
        


        float fHorizontalInput = Input.GetAxisRaw("Horizontal");
        float fVerticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.forward * fVerticalInput + transform.right * fHorizontalInput;

        rig.AddForce(move.normalized * fCurrentSpeed * Time.deltaTime * 10f, ForceMode.Force);
    }

    private void Jump()
    {        
        if (bGrounded && bJumpAllowed)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rig.AddForce(Vector3.up * fJumpForce * Time.fixedDeltaTime * 10f, ForceMode.Impulse);

                bJumpAllowed = false;

                Invoke("ResetJump", 0.1f);
            }
        }
    }
    
    private void GroundCheck()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f);

        if (hit.collider != null && hit.collider.tag != "Player")
        {
            bGrounded = true;
        }
        else
        {
            bGrounded = false;
        }
    }

    private void Strafe()
    {
        if (!bAllowMovement)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Mouse X") > 0)
            {
                //Strafe Right
                Vector3 vel = rig.velocity;
                vel.y = 0;
                Vector3 speed = transform.forward * vel.magnitude;
                speed.y = rig.velocity.y;
                rig.velocity = speed;
            }

            if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Mouse X") < 0)
            {
                //Strafe Left
                Vector3 vel = rig.velocity;
                vel.y = 0;
                Vector3 speed = transform.forward * vel.magnitude;
                speed.y = rig.velocity.y;
                rig.velocity = speed;
            }
        }
    }

    private void SetDrag()
    {
        if (!bGrounded || !bJumpAllowed)
        {
            rig.drag = fAirDrag;
            bAllowMovement = false;
        }
        else
        {
            rig.drag = fGroundDrag;
            bAllowMovement = true;
        }
    }

    private void CalculateSpeed()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fMultiplyerSpeed = 0;
        }
    }

    private void ResetJump()
    {
        bJumpAllowed = true;
        fMultiplyerSpeed += 1;
    }

    private void SetVelText()
    {
        text.text = Mathf.RoundToInt(rig.velocity.magnitude).ToString();
    }

    private void ManageEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bEscapeMenu = !bEscapeMenu;
        }

        if (bEscapeMenu)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = bEscapeMenu;
        goEscapeMenu.SetActive(bEscapeMenu);
        GameScreen.SetActive(!bEscapeMenu);
    }

    private void GetSense()
    {
        if (PlayerPrefs.HasKey("Sense"))
        {
            fSense = PlayerPrefs.GetFloat("Sense");
        }
        else
        {
            fSense = 250f;
        }
    }

    private void ResetScene()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
