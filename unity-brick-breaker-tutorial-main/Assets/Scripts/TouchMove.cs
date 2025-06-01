using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 75f;
    private int auxDirecao;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetPaddle();
    }

     public void ResetPaddle()
    {
        rigidbody.linearVelocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    void Update()
    {
        if (auxDirecao != 0) 
        {
            transform.Translate(speed * Time.deltaTime + auxDirecao, 0, 0);
        }

        if (auxDirecao < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

     
        if (auxDirecao > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector2.left;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector2.right;
        } else {
            direction = Vector2.zero;
        }
    }

     private void FixedUpdate()
    {
        if (direction != Vector2.zero) {
            rigidbody.AddForce(direction * speed);
        }


    }

    public void TouchHorizontal(int direcao)
    {
        auxDirecao = direcao;
        speed = 30f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float maxOffset = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.linearVelocity);
            float bounceAngle = (offset / maxOffset) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.linearVelocity = rotation * Vector2.up * ball.rigidbody.linearVelocity.magnitude;
        }
    }

}
