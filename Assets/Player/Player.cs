using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody _rigidbody;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _camera;

    [SerializeField] private float _powerupDuration;
    private Coroutine _powerupCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;

        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidbody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }

    public void PickPowerUp() {
        if (_powerupCoroutine != null) {
            StopCoroutine(_powerupCoroutine);
        }

        _powerupCoroutine = StartCoroutine(StartPowerUp());
        
    }

    private IEnumerator StartPowerUp() {

        if (OnPowerUpStart != null) {
            Debug.Log("Start Powerup");
            OnPowerUpStart();
        }

        yield return new WaitForSeconds(_powerupDuration);

        if (OnPowerUpStop != null) {
            Debug.Log("Stop Powerup");
            OnPowerUpStop();
        }
    }
}
