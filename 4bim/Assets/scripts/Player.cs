using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float focaPulo = 10f;

    public bool noChao = false;
    private bool podePular = true;  // Variável para controlar o pulo
    private bool puloDuplo = false;  // Variável para controlar o pulo duplo

    public bool andando = false;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "noChao")
        {
            noChao = true;
            podePular = true; // O jogador pode pular novamente quando estiver no chão
            puloDuplo = false; // Reseta o pulo duplo
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "noChao")
        {
            noChao = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        andando = false;

        // Movimentos para esquerda e direita
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = true;
            Debug.Log("LeftArrow");

            if (noChao)
            {
                andando = true;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = false;
            Debug.Log("RightArrow");

            if (noChao)
            {
                andando = true;
            }
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (noChao && podePular)
            {
                _rigidbody2D.AddForce(new Vector2(0, 1) * focaPulo, ForceMode2D.Impulse);
                podePular = false;  // Impede um segundo pulo no chão
                Debug.Log("Pulo");
            }
            else if (!noChao && !puloDuplo)
            {
                _rigidbody2D.AddForce(new Vector2(0, 1) * focaPulo, ForceMode2D.Impulse);
                puloDuplo = true;  // Marca que o pulo duplo foi realizado
                Debug.Log("Pulo Duplo");
            }
        }

        _animator.SetBool("Andando", andando);
    }
}