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
    [SerializeField]
    private Hookshot _hookshot = null;
    [SerializeField]
    private Vector3 _hookshotMoveVector = Vector3.zero;
    [SerializeField]
    private SpriteRenderer _spriteRenderer1 = null, _spriteRenderer2 = null;

    [SerializeField]
    private Animator _auraAnimator = null;
    
    private float _speedY = 0f;
    private float _spriteMaskSize;

    private bool _isMaskOpened = false;

    private Coroutine _shootingCoroutine = null;

    private void Start()
    {
        _hookshot.HitTree += OnHitTree;
        _hookshot.HitNothing += OnHitNothing;
        _shootingCoroutine = null;
        _playerState = PlayerState.Falling;
    }

    private void Update()
    {
        if (!_spriteRenderer1.isVisible && !_spriteRenderer2.isVisible)
            KillPlayer();
        ScalePlayerMask((InputManager.GetRequestedPlayerInput(TapType.Left, true)));
        if ((InputManager.GetRequestedPlayerInput(TapType.Left, false)))
        {
            _auraAnimator.SetTrigger("AuraPopup");
            //_auraAnimator.ResetTrigger("AuraPopup");
        }
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
        InterfaceRun.AuraKeyDetection(opening);
        Vector3 scaling = new Vector3(1, 1, 0) * ((opening) ? 1f : -1f) * _maskScalingSpeed;
        _spriteMask.transform.localScale += scaling * Time.deltaTime;
        if (opening && _spriteMask.transform.localScale.x >= _maxMaskSize)
            _spriteMask.transform.localScale = new Vector3(_maxMaskSize, _maxMaskSize, 1f);
        else if (_spriteMask.transform.localScale.x <= 0f)
            _spriteMask.transform.localScale = Vector3.forward;
    }

    private void Jump()
    {
        switch (_playerState)
        {
            case PlayerState.Running:
                _speedY = _jumpAcceleration;
                _playerState = PlayerState.Jumping;
                break;
            case PlayerState.Jumping:
            case PlayerState.Falling:
                if (_shootingCoroutine == null)
                    _shootingCoroutine = StartCoroutine(ShootingHook());
                break;
        }
        
    }

    private IEnumerator ShootingHook()
    {
        InterfaceRun.ThrowLine();
        while(InputManager.GetRequestedPlayerInput(TapType.Right, true))
        {
            if (_playerState == PlayerState.Sliding)
            {
                Debug.DrawLine(_hookshot.transform.position, _hookshot.transform.position + _hookshot.GetNormalizedDirectionVector(transform.position) * _hookshot.HookshotDistance, Color.red);
                transform.position = new Vector3(transform.position.x, _hookshot.transform.position.y + _hookshot.GetNormalizedDirectionVector(transform.position).y * _hookshot.HookshotDistance, transform.position.z);
                if (Mathf.Acos(_hookshot.GetNormalizedDirectionVector(transform.position).y / _hookshot.GetNormalizedDirectionVector(transform.position).x) > Mathf.PI/4f)
                    break;
            }
            else
                _hookshot.MoveHook(_hookshotMoveVector);
            yield return null;
        }
        if(_playerState == PlayerState.Sliding)
            ShootOutOfHook();
        ResetHookshot();
        _shootingCoroutine = null;
        yield return null;
    }

    private void ShootOutOfHook()
    {
        _speedY = -_hookshot.GetNormalizedDirectionVector(transform.position).y * DifficultyManager.GetGameSpeed() * 0.5f;
        _playerState = PlayerState.Jumping;
    }

    private void ResetHookshot()
    {
        _hookshot.ResetPosition(transform.position);
    }

    private void KillPlayer()
    {
        _playerState = PlayerState.Dead;
        if (PlayerDied != null)
            PlayerDied();
    }

    private void OnHitNothing()
    {
        if (_shootingCoroutine != null)
        {
            _shootingCoroutine = null;
            ResetHookshot();
        }
    }

    private void OnHitTree()
    {
        if (_shootingCoroutine != null)
        {
            _playerState = PlayerState.Sliding;
            _hookshot.HookshotDistance = Vector3.Distance(transform.position, _hookshot.transform.position);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isMaskOpened && collision.collider.tag != "GROUND")
            return;
        Vector2 hitDirection = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
        if (transform.position.y < collision.transform.position.y + (collision.transform.localScale.y * 0.5f))
            KillPlayer();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isMaskOpened && collision.collider.tag != "GROUND")
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
