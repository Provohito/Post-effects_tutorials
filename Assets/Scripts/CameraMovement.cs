using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(requiredComponent: typeof(Rigidbody))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 10f;
    [SerializeField]
    float _rotationSpeed = 3.0f;

    private Vector2 _rotation = Vector2.zero;
    private Vector3 _moveVector = Vector3.zero;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _rotation += new Vector2(x:-Input.GetAxis("Mouse Y"), y:Input.GetAxis("Mouse X"));
        transform.eulerAngles = _rotation * _rotationSpeed;

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        _moveVector = new Vector3(x, y:0, z) * _moveSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.TransformDirection(_moveVector);
    }
}
