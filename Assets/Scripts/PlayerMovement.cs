using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float Speed = 6f;
    public float JumpSpeed = 5f;
    public float Gravity = 20f;

    private Vector3 _moveDirection = Vector3.zero;
    private bool _onLadder = false;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();

        if (Input.GetButton("Jump") && _onLadder)
            HandleLadderInput(controller);
        else
            HandleRegularInput(controller);        
	}

    private void HandleLadderInput(CharacterController controller)
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= Speed;

        controller.Move(_moveDirection * Time.deltaTime);
    }

    private void HandleRegularInput(CharacterController controller)
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            Gravity = 20f;
            _onLadder = true;
            Debug.Log("On ladder");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            Gravity = 20f;
            _onLadder = false;
            Debug.Log("Off ladder");
        }
    }
}
