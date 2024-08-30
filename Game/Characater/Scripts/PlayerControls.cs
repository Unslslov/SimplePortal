using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Rigidbody _controller;

    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _speed = 15f;

    private void OnValidate() 
    {
        _controller ??= GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _moveDirection = transform.forward * inputAxis.y + transform.right * inputAxis.x;
    }

    private void FixedUpdate() 
    {
        // Optional
        if(_moveDirection.magnitude == 0)
            return;

        _controller.velocity = new Vector3(_moveDirection.x * _speed, _moveDirection.y, _moveDirection.z * _speed);
    }
}