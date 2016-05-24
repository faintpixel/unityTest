using UnityEngine;
using System.Collections;

public class RatScript : MonoBehaviour {
    public float Speed = 5f; // TO DO - switch to min/max speed and randomly pick a speed each time it turns
    public float Gravity = 20f;
    public Vector3 MoveDirection;

    private CharacterController _controller;
    private bool _wasGrounded = false;
    private bool _switchingDirections = false;
    private Vector3 _previousPosition;

    void Start () {
        _controller = GetComponent<CharacterController>();
        //MoveDirection = new Vector3(1f, 0f, 0f);
	}
	
	void Update () {
	
	}

    public void FixedUpdate()
    {
        Vector3 moveDirection;

        _previousPosition = transform.position;

        if (_controller.isGrounded || _switchingDirections)
        {
            _switchingDirections = false;
            _wasGrounded = true;
            moveDirection = new Vector3(MoveDirection.x, MoveDirection.y, MoveDirection.z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= Speed;
            moveDirection.y -= Gravity * Time.deltaTime;
            _controller.Move(moveDirection * Time.deltaTime);
        }
        else if(_wasGrounded)
        {
            transform.position = _previousPosition;
            MoveDirection *= -1;
            _switchingDirections = true;
        }
        else
        {
            moveDirection = new Vector3(MoveDirection.x, MoveDirection.y, MoveDirection.z);
            moveDirection.y -= Gravity * Time.deltaTime;
            _controller.Move(moveDirection * Time.deltaTime);
        }

    }
}
