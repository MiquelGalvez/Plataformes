using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Animator m_Animator;
    private SpriteRenderer sprflip; 
    private bool m_Running;
    private bool m_Jumping;
    private Rigidbody2D player;
    [SerializeField]
    TextMeshProUGUI textpunt;
    public float speed;
    private int punt = 0;
    bool isJumping = false; //Para comprobar si ya está saltando
    [Range(1, 500)] public float potenciaSalto; //Potencia de salto del jugador
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        sprflip = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xdir = Input.GetAxisRaw("Horizontal");

        float movimientoH = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(movimientoH * speed, player.velocity.y);
        m_Running = false;
        m_Jumping = false;



        if (xdir < 0)
        {
            m_Running = true;
            player.AddForce(Vector2.left * speed);
            sprflip.flipX = true;
        }
        else if (xdir > 0)
        {
            m_Running = true;
            player.AddForce(Vector2.right * speed);
            sprflip.flipX = false;
        }

        if (Input.GetButton("Jump") && !isJumping)
{
            //Le aplico la fuerza de salto
            player.AddForce(Vector2.up * potenciaSalto);
            //Digo que está saltando (para que no pueda volver a saltar)
            isJumping = true;
            m_Jumping = true;
        }
        m_Animator.SetBool("corriendo", m_Running);
        m_Animator.SetBool("saltando", m_Jumping);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Si el jugador colisiona con un objeto con la etiqueta suelo
        if (other.gameObject.CompareTag("Suelo"))
        {
            //Digo que no está saltando (para que pueda volver a saltar)
            isJumping = false;
            //Le quito la fuerza de salto remanente que tuviera
            player.velocity = new Vector2(player.velocity.x, 0);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Suelo")
        {
            SceneManager.LoadScene(0);
        }
        if (collision.tag == "Puerta")
        {
            SceneManager.LoadScene(1);
        }
        if (collision.tag == "PickUp")
        {
            punt++;
            textpunt.text = (": " + punt.ToString());
            Destroy(collision.gameObject);
        }

    }
}
