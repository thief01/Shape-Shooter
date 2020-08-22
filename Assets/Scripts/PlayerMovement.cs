using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tr;
    public float speed=10;

    void Update()
    {
        playerRotation();
        Vector2 offset;
        offset.y = Input.GetAxis("Vertical");
        offset.x = Input.GetAxis("Horizontal");

        rb.velocity = offset*speed*Time.deltaTime;
    }
    void playerRotation()
    {
        Vector2 mouse = Input.mousePosition;
        mouse = new Vector2(mouse.x - Screen.width/2, mouse.y - Screen.height/2);
        float angle = Vector2.SignedAngle(new Vector2(1, 1), mouse);
        tr.localEulerAngles = new Vector3(0, 0, angle + 45);
    }
}
