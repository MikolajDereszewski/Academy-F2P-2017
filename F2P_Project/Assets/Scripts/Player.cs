using System.Collections;
using UnityEngine;
using GameClasses;
using System;

public class Player : MonoBehaviour {

    public event Action PlayerDied;

    [SerializeField]
    private PlayerState _playerState;
    private TapType _currentInput;

    [SerializeField]
    private float _jumpAcceleration = 3f;
    [SerializeField]
    private float _gravityAcceleration = 9.81f;
    [SerializeField]
    private float _playerWeight;
    [SerializeField]
    private SpriteMask _spriteMask;
    [SerializeField]
    private float _maxMaskSize = 25f;
    [SerializeField]
    private float _maskScalingSpeed = 50f;
    
    private float _speedY = 0f;
    private float _spriteMaskSize;

    private bool _isMaskOpened = false;

    private void Start()
    {
        _playerState = PlayerState.Falling;
    }

    private void Update()
    {
        ScalePlayerMask((InputManager.GetRequestedPlayerInput(TapType.Left)));
        _currentInput = InputManager.GetPlayerInput();
        if (_currentInput == TapType.None)
            ScalePlayerMask(false);
        if (InputManager.GetRequestedPlayerInput(TapType.Right))
            Jump();
    }

    private void FixedUpdate()
    {
        if(_playerState != PlayerState.Sliding)
        {
            _speedY -= _gravityAcceleration * Time.deltaTime;
            transform.position += Vector3.up * _speedY * _playerWeight * Time.deltaTime;
            if (_playerState == PlayerState.Jumping && _speedY < 0)
                _playerState = PlayerState.Falling;
        }
    }

    private void ScalePlayerMask(bool opening)
    {
        _isMaskOpened = opening;
        if(opening)
            Debug.Log("Scaling");
        Vector3 scaling = new Vector3(1, 1, 0) * ((opening) ? 1f : -1f) * _maskScalingSpeed;
        _spriteMask.transform.localScale += scaling * Time.deltaTime;
        if (opening && _spriteMask.transform.localScale.x >= _maxMaskSize)
            _spriteMask.transform.localScale = new Vector3(_maxMaskSize, _maxMaskSize, 1f);
        else if (_spriteMask.transform.localScale.x <= 0f)
            _spriteMask.transform.localScale = Vector3.forward;
    }

    private void Jump()
    {
        Debug.Log("JUMP");
        switch (_playerState)
        {
            case PlayerState.Running:
                _speedY = _jumpAcceleration;
                _playerState = PlayerState.Jumping;
                break;
            case PlayerState.Jumping:
            case PlayerState.Falling:
                //code for slide start;
                break;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isMaskOpened)
            return;
        Vector2 hitDirection = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
        if (transform.position.y < collision.transform.position.y + (collision.transform.localScale.y*0.5f))
        {
            if (PlayerDied != null)
                PlayerDied();
            _playerState = PlayerState.Dead;
            Debug.Log("MARTWY!");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isMaskOpened)
            return;
        _playerState = PlayerState.Running;
        transform.position = new Vector3(transform.position.x, collision.contacts[0].point.y + transform.localScale.y*0.5f);
        _speedY = 0f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_playerState != PlayerState.Jumping)
            _playerState = PlayerState.Falling;
    }
}
