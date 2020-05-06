using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform finish;
    
    public float upperLegTorque;
    public float lowerLegTorque;

    public Game game;

    public Rigidbody2D qMotor;
    public Rigidbody2D wMotor;
    public Rigidbody2D oMotor;
    public Rigidbody2D pMotor;

    public Image qImage;
    public Image wImage;
    public Image oImage;
    public Image pImage;

    bool qPressed;
    bool wPressed;
    bool oPressed;
    bool pPressed;

    public Sprite buttonUp;
    public Sprite buttonDown;
    
    private Color pressedColor = new Color(0.18f,0.70f,0.93f);
    
    public bool[] buttonsDown = {false, false, false, false};
    private bool canMove;

    void Awake()
    {
        canMove = false;
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Go()
    {
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        canMove = true;
        qMotor.MoveRotation(90f);
        wMotor.MoveRotation(-90f);
        oMotor.MoveRotation(10f);
        pMotor.MoveRotation(10f);
    }
    
    void Update()
    {
        // If the finish line is crossed, we win.
        if (transform.position.x >= finish.transform.position.x - finish.transform.localScale.x/2f)
        {
            game.Win();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }

        if (!canMove)
        {
            return;
        }

        if (game.isOver)
        {
            ClearColor(qImage);
            ClearColor(wImage);
            ClearColor(oImage);
            ClearColor(pImage);
            return;
        }

        // Ugly workaround to allow buttons on screen as well as keyboard to work.
        qPressed = Input.GetKey(KeyCode.Q) || buttonsDown[0];
        wPressed = Input.GetKey(KeyCode.W) || buttonsDown[1];
        oPressed = Input.GetKey(KeyCode.O) || buttonsDown[2];
        pPressed = Input.GetKey(KeyCode.P) || buttonsDown[3];


        if (qPressed)
        {
            SetColor(qImage);

            qMotor.MoveRotation(qMotor.rotation + upperLegTorque * Time.deltaTime);
            wMotor.MoveRotation(wMotor.rotation - 0.8f * upperLegTorque * Time.deltaTime);
        }
        else
        {
            ClearColor(qImage);
        }


        UpdateMotorMovement();
    }

    private void UpdateMotorMovement()
    {
        if (wPressed)
        {
            SetColor(wImage);
            wMotor.MoveRotation(wMotor.rotation + upperLegTorque * Time.deltaTime);
            qMotor.MoveRotation(qMotor.rotation - 0.8f * upperLegTorque * Time.deltaTime);
        }
        else
        {
            ClearColor(wImage);
        }

        if (oPressed)
        {
            SetColor(oImage);
            oMotor.MoveRotation(oMotor.rotation - lowerLegTorque * Time.deltaTime);
            pMotor.MoveRotation(pMotor.rotation + 0.8f * lowerLegTorque * Time.deltaTime);
        }
        else
        {
            ClearColor(oImage);
        }

        if (pPressed)
        {
            SetColor(pImage);
            pMotor.MoveRotation(pMotor.rotation - lowerLegTorque * Time.deltaTime);
            oMotor.MoveRotation(oMotor.rotation + 0.8f * lowerLegTorque * Time.deltaTime);
        }
        else
        {
            ClearColor(pImage);
        }
    }
    
    private void SetColor(Image image)
    {
        image.color = pressedColor;
        image.sprite = buttonDown;
        image.SetNativeSize();
    }

    private void ClearColor(Image image)
    {
        image.color = Color.white;
        image.sprite = buttonUp;
        image.SetNativeSize();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            game.Lose();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            game.Win();
        }
    }
}