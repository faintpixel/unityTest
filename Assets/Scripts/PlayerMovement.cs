using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float Speed = 6f;
    public float JumpSpeed = 5f;
    public float Gravity = 20f;

    public Text countText;

    private Vector3 _moveDirection = Vector3.zero;
    private bool _onLadder = false;
    private Vector3 _startPosition;
    private CharacterController _controller;

    private int _treasureCount;

	void Start () {
        _startPosition = transform.position;
        _treasureCount = 0;
        _controller = GetComponent<CharacterController>();
        SetCountText();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump") && _onLadder)
            HandleLadderInput(_controller);
        else
            HandleRegularInput(_controller);        
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

    private void Die()
    {
        Debug.Log("dead.");

        // play a sound
        AudioSource deathSound = GetComponent<AudioSource>();
        deathSound.Play();

        // remove life

        // give temporary invulnerability

        // respawn
        transform.position = _startPosition;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadder = true;
            Debug.Log("On ladder");
        }
        else if(other.gameObject.CompareTag("Hazard"))
        {
            Die();
        }
        else if(other.gameObject.CompareTag("Treasure"))
        {           
            AudioSource pickupSound = other.GetComponent<AudioSource>();         
            AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
            Destroy(other.gameObject);
            _treasureCount++;
            SetCountText();           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadder = false;
            Debug.Log("Off ladder");
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + _treasureCount.ToString();       
    }
}
