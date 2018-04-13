using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigid;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);

        Animating(h, v);

        Turning();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);

        movement = movement.normalized * speed * Time.fixedDeltaTime;

        playerRigid.MovePosition(transform.position + movement);
    }

    void Animating(float h, float v)
    {
        bool walking = (h != 0 || v != 0);

        anim.SetBool("IsWalking", walking);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigid.MoveRotation(newRotation);
        }
    }

}
