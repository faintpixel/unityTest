using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
    public float MoveSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = MoveSpeed * Input.GetAxis("Mouse Y") * -1;
        float v = MoveSpeed * Input.GetAxis("Mouse X");

        transform.Rotate(h, v, 0);
    }
}
