using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewMovement : MonoBehaviour
{
    [SerializeField] float fMaxAngle;
    [SerializeField] Camera cam;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject goEscapeMenu;
    [SerializeField] GameObject GameScreen;
    
    private float fSense;

    private float fRotationX;
    private float fRotationY;
    
    private Rigidbody rig;

    public bool bEscapeMenu;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Rotate()
    {
        float fMouseX = Input.GetAxisRaw("Mouse X") * fSense * Time.deltaTime;
        float fMouseY = Input.GetAxisRaw("Mouse Y") * fSense * Time.deltaTime;

        fRotationY += fMouseX;

        fRotationX -= fMouseY;
        fRotationX = Mathf.Clamp(fRotationX, -fMaxAngle, fMaxAngle);

        cam.transform.rotation = Quaternion.Euler(new Vector3(fRotationX, fRotationY, 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, fRotationY, 0));
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

    private void SetVelText()
    {
        text.text = Mathf.RoundToInt(rig.velocity.magnitude).ToString();
    }
}
