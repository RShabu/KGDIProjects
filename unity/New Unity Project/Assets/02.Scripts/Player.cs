using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : Character {

    bool isGround;
    float horizontal;

    public float RunSpeed = 1;
    public float JumpForce = 1;

    [Header("Ground Check")] 
    [Space(10)] //칸띄우기
    public Color GizmosColor;
    public LayerMask GroundLayer;
    public Vector3 Offset;
    public float Radius = 1;

    public override void OnHurt(int amount)
    {
        base.OnHurt(amount); //base 부모 this 자기자신
    }

    private void OnDrawGizmos()
    {
        Handles.color = GizmosColor;

        Handles.DrawWireDisc(transform.position + Offset, Vector3.forward, Radius);
    }

    private void Awake()
    {
        GetAnimator = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();

    }

    private void FixedUpdate()
    {
        isGround = CheckGround();
        GetAnimator.SetBool("IsGround", isGround);

        Move(horizontal);

        float vY = Rigid.velocity.y;
        GetAnimator.SetFloat("VelocityY", vY);
    }

    void Move(float h)
    {
        GetAnimator.SetInteger("Run", (int)h);

        if (h == 0)
            return;

        transform.localScale = new Vector3(h*15,15,1);

        transform.Translate(Vector3.right * h *RunSpeed *Time.fixedDeltaTime);
    }

    bool CheckGround()
    {

        Collider2D collider = Physics2D.OverlapCircle(transform.position + Offset, Radius, GroundLayer);

        return collider != null ? true : false;

        /*
        if(collider !=null)
        {
            retrun true;
        }
        else
        {
            return false;
        }
        */
    }

    void Jump()
    {
        if (!isGround)
            return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rigid.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
        }
    }

}
