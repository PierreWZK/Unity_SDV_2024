using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moovement : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;
    public bool faceingRight = true;
    public LayerMask groundLayer;
    private Rigidbody2D _rb;
    private bool _isGrounded;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public Animator animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (_rb != null)
        {
            //! =========== !\\
            //!  Moovement  !\\
            //! =========== !\\
            
            //?  left x right  ?\\
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
                SetupDirectionByComponant("right");
            }
            else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
                SetupDirectionByComponant("left");
            } 
            else
            {
                transform.Translate(Vector3.zero);
                animator.SetFloat("Speed", 0);
            }
            //?  left x right  ?\\

            //?  Jump  ?\\
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rb.AddForce(Vector2.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode2D.Impulse);
                // animator.SetBool("Jump", true);
            }
            //?  Jump  ?\\

            //? =========== ?\\
            //?  Controles  ?\\
            //? =========== ?\\
            // Stop player if he press left and right at the same time
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.zero);
                animator.SetFloat("Speed", 0);
            }
            //? =========== ?\\
            //?  Controles  ?\\
            //? =========== ?\\

            //! =========== !\\
            //!  Moovement  !\\
            //! =========== !\\

            //*  Animation  *\\
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
            if (_isGrounded) {
                animator.SetBool("Jump", false);
            } else {
                animator.SetBool("Jump", true);
            }
            //*  Animation  *\\
        }
        else
        {
            Debug.LogError("Rigidbody2D is not attached to the GameObject!");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }

    private void SetupDirectionByComponant(string direction)
    {
        if (direction == "right" && faceingRight == false)
        {
            faceingRight = true;
            _spriteRenderer.flipX = false;
        }
        else if (direction == "left" && faceingRight == true)
        {
            faceingRight = false;
            _spriteRenderer.flipX = true;
        }
    }
}
