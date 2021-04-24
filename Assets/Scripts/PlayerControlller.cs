using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    [SerializeField] Transform view_pos;
    [SerializeField] float mv_speed = 10f;
    [SerializeField] float jmp_force = 20f;
    [SerializeField] float mouse_sense = 2f;
    [SerializeField] bool mouse_inv = false;

    [SerializeField] int[] min_max_view_angle = new int[2]{-60, 60};

    public delegate void DeathHandler();
    public event DeathHandler Death;
    Camera main_cam;
    RaycastHit hit_obj;

    Rigidbody rb;

    float v_rot = 0f;//verical rotation

    private void Awake() 
    {
        main_cam = Camera.main;
        main_cam.transform.position = view_pos.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    void Update()
    {
        InputHandler();
    }

    void InputHandler()
    {
        var v_mv = Input.GetAxis("Vertical");
        var h_mv = Input.GetAxis("Horizontal"); 

        var jmp = Input.GetButtonDown("Jump");

        MovePlayer(v_mv, h_mv, jmp);

        var v_view = Input.GetAxis("Mouse Y");
        var h_view = Input.GetAxis("Mouse X");

        PlayerView(v_view,  h_view);

        if(Input.GetButtonDown("Fire1"))
            PlayerTouch();
    }

    void MovePlayer(float v, float h, bool j = false)
    {
        if(j)
            rb.AddRelativeForce(transform.up * jmp_force * Mathf.Abs(Physics.gravity.y));

        var new_pos = ((transform.forward * v) + (transform.right * h)) * Time.deltaTime * mv_speed;
        rb.MovePosition(transform.position + new_pos);
    }

    void PlayerView(float v, float h)
    {
        transform.Rotate(Vector3.up, h * mouse_sense, Space.Self);

        v_rot += v * mouse_sense * (mouse_inv ? 1 : -1);
        v_rot = Mathf.Clamp(v_rot, min_max_view_angle[0], min_max_view_angle[1]);

        view_pos.localRotation = Quaternion.Euler(v_rot, 0, 0);

        main_cam.transform.position = view_pos.position;
        main_cam.transform.rotation = view_pos.rotation;
    }

    bool PlayerTouch()
    {
      return Physics.Raycast(view_pos.position,view_pos.forward, out hit_obj, 2f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (hit_obj.collider != null)
        {
            Gizmos.DrawRay(view_pos.position, view_pos.forward * hit_obj.distance);
            Gizmos.DrawWireSphere(view_pos.position + view_pos.forward * hit_obj.distance, 0.1f);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Respawn")
        {
            Death();
        }
    }
}
