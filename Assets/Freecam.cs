using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freecam : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 10;
    [SerializeField]
    private float MoveSpeedStep = 1;
    [Tooltip("Use keycode based command, not unity input")]
    [SerializeField]
    private bool NoRelience = false;

    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    private Transform Child;

    private Vector3 MoveDirection;
    private Vector3 Rotation;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (NoRelience)
            IndipendentCheckInput();
        else
            CheckInput();

        MouseInput();

        Move();
    }

    private void Move()
    {
        transform.Translate(MoveDirection*MoveSpeed, Space.Self);
    }

    private void IndipendentCheckInput()
    {
        MoveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            MoveDirection.z = 1;
        else if (Input.GetKey(KeyCode.S))
            MoveDirection.z = -1;

        if (Input.GetKey(KeyCode.A))
            MoveDirection.x = -1;
        else if (Input.GetKey(KeyCode.D))
            MoveDirection.x = 1;

        if (Input.GetKey(KeyCode.Space))
            MoveDirection.y = 1;
        else if (Input.GetKey(KeyCode.C))
            MoveDirection.y = -1;

        MoveDirection.Normalize();

    }

    private void CheckInput()
    {
        MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetButton("Jump"))
        {
            MoveDirection.y = 1;
        }
        else if (Input.GetButton("Down"))
        {
            MoveDirection.y = -1;
        }
        MoveDirection.Normalize();
    }

    private void MouseInput()
    {
        MoveSpeed = Mathf.Clamp(MoveSpeed + (Input.mouseScrollDelta.y * MoveSpeedStep), 0, Mathf.Infinity);

        Rotation = new Vector3(Input.GetAxis("Mouse Y")*-1, Input.GetAxis("Mouse X"), 0)*RotationSpeed;

        //transform.Rotate(Rotation);
        Child.Rotate(Vector3.right, Rotation.x);
        transform.Rotate(Vector3.up, Rotation.y);


    }
}
