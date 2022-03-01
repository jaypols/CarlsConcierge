using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour {

  private float moveInput;
  private Rigidbody2D rb;
  private bool faceRight = true;
  private bool isGrounded;
  private Animator animator;
  private List <string> collisions = new List<string>();

  public float speed = 5;
  public float jumpFactor = 5.0F;
  public Transform grounded;
  public float checkRadius = 0.5F;
  public LayerMask theGround;
  public GameObject explosion;
  public Text roomText;
  public int jumpsAvailable = 2;
  public GameOverScreen GameOverScreen;
  
  // Start is called before the first frame update
  void Start() {
    animator = GetComponent <Animator>();
    rb = GetComponent <Rigidbody2D>();
    roomText.text = "4";

  }

  // Update is called once per frame
  void Update() {
    if (isGrounded) {
      jumpsAvailable = 2;

    }
    if (Input.GetKeyDown(KeyCode.UpArrow) && jumpsAvailable > 0) {

      rb.velocity = Vector2.up * jumpFactor;
      animator.SetBool("isRun", false);
      animator.SetBool("isIdle", false);
      animator.SetBool("isJump", true);
      jumpsAvailable--;
    } else if (Input.GetKeyDown(KeyCode.UpArrow) && jumpsAvailable == 0 && isGrounded == true) {
      rb.velocity = Vector2.up * jumpFactor;
      animator.SetBool("isRun", false);
      animator.SetBool("isIdle", false);
      animator.SetBool("isJump", true);

    }

  }

  void FixedUpdate() {
    isGrounded = Physics2D.OverlapCircle(grounded.position, checkRadius, theGround);

    moveInput = Input.GetAxisRaw("Horizontal");

    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    if (rb.velocity == Vector2.zero) {
      animator.SetBool("isRun", false);
      animator.SetBool("isIdle", true);
      animator.SetBool("isJump", false);
    } else if (moveInput != 0 && isGrounded == true) {

      animator.SetBool("isRun", true);
      animator.SetBool("isIdle", false);
      animator.SetBool("isJump", false);
    }

    if (!faceRight && moveInput > 0) {
      Flip();
    } else if (faceRight && moveInput < 0) {
      Flip();
    }

  }

  void Flip() {
    if (!faceRight)
      faceRight = true;
    else
      faceRight = false;

    Vector3 Scaler = transform.localScale;
    Scaler.x = Scaler.x * -1;
    transform.localScale = Scaler;
  }

  void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.name == "Platform Special") {
      if (!collisions.Contains(col.gameObject.tag)) {
        collisions.Add(col.gameObject.tag);

        int roomTemp = int.Parse(roomText.text) - 1;
        roomText.text = roomTemp.ToString();
        if (roomTemp == 0) {
          gameObject.SetActive(false);
          GameOverScreen.Setup(4);
        }
        GameObject e = Instantiate(explosion) as GameObject;
        e.transform.position = transform.position;
        Destroy(col.transform.GetChild(0).gameObject);
      }
    }
  }
}