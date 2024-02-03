using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class Kretanje : MonoBehaviour
{

    public float brzina = 5f;
    private Rigidbody2D igrac;
    private Vector2 inputVector = Vector2.zero;
    private Vector2 lastPointed = new Vector2(1,0);
    private Vector3 levo = new Vector3(0,180,0);
    private Vector3 desno = Vector3.zero;
    private int dashingCooldown;
    private int dashingDuration;
    public GameObject luk;
    public GameObject strela;
    private int holdDuration = 0;
    private bool holding = false;
    private SpriteResolver spriteResolver;
    bool ziv = true;
    public logika logikaSkripta;

    public void Fire(InputAction.CallbackContext context)
    {
        if (ziv)
        {
            if (context.performed)
            {
                spriteResolver.SetCategoryAndLabel("Aiming", "Aiming1");
                holding = true;
            }
            if (context.canceled && holdDuration > 50)
            {
                spawnArrow();
                holding = false;
                holdDuration = 0;
            }
            if (context.canceled)
            {
                holding = false;
                holdDuration = 0;
                spriteResolver.SetCategoryAndLabel("Idle", "Idle1");
            }
        }
    }

    private void spawnArrow()
    {
        GameObject ispaljena = Instantiate(strela);
        ispaljena.name = strela.name + igrac.name;
        ispaljena.transform.position = luk.transform.position;
        Rigidbody2D ispaljenaStrela = ispaljena.GetComponent<Rigidbody2D>();
        int x=0;
        if (lastPointed.y < 0)
        {
            x = 180;
        }
        ispaljenaStrela.transform.eulerAngles = new Vector3(x, 180-lastPointed.y * 180, lastPointed.x*90);
        if(lastPointed.x != 0 && lastPointed.y !=0)
        {
            if (lastPointed.x < 0)
            {
                lastPointed.x = -1;
            }
            if (lastPointed.y < 0)
            {
                lastPointed.y = -1;
            }
            ispaljenaStrela.transform.eulerAngles = new(x, 180 - MathF.Ceiling(lastPointed.y) * 180, -MathF.Ceiling(lastPointed.x) * 45);
        }
        ispaljenaStrela.velocity = lastPointed * 20f;

    }
    private void Awake()
    {
        igrac = GetComponent<Rigidbody2D>(); 
        spriteResolver = GetComponent<SpriteResolver>();
        logikaSkripta = GameObject.FindGameObjectWithTag("log").GetComponent<logika>();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && dashingCooldown==0 && ziv)
        {
            dashingDuration = 10;
            dashingCooldown = 100;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed && ziv)
        {
            lastPointed = context.ReadValue<Vector2>();
            if(lastPointed.x > 0)
            {
                igrac.transform.eulerAngles = desno;
            }
            else if (lastPointed.x < 0)
            {
                igrac.transform.eulerAngles = levo;
            }
            inputVector = lastPointed * brzina;

        }
        else if (context.canceled && ziv) 
        {
            inputVector = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (dashingDuration>0){
            dashingDuration--;
            igrac.velocity = inputVector * brzina;
        }
        else
        {
            igrac.velocity = inputVector;
            if (dashingCooldown > 0)
            {
                dashingCooldown--;
            }
        }
        if (holding)
        {
            holdDuration++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name.StartsWith(strela.name) && !(collision.name.EndsWith(strela.name + igrac.name))))
        {
            ziv = false;
            igrac.velocity = Vector2.zero;
            igrac.transform.eulerAngles = new Vector3(0, 0, 90);
            logikaSkripta.gameOver();

        }
    }
}
