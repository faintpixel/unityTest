using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float Speed = 6f;
    public float JumpSpeed = 5f;
    public float Gravity = 20f;

    private Vector3 _moveDirection = Vector3.zero;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= Speed;
            if (Input.GetButton("Jump"))
                _moveDirection.y = JumpSpeed;
        }
        _moveDirection.y -= Gravity * Time.deltaTime;
        controller.Move(_moveDirection * Time.deltaTime);
	}
}
