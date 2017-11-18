using System.Collections;
using UnityEngine;
using GameClasses;
using System;

public class Player : MonoBehaviour
{

    public event Action PlayerDied;
    public event Action PlayerLanded;
    public event Action PlayerExitGround;

    public PlayerState CurrentState { get { return _playerState; } }

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
    [SerializeField]
    private Animator _playerAnimator = null;
    [SerializeField]
    private Animator _playerSpiritAnimator = null;

    [SerializeField]
    private AudioSource _hookshotWhoosh = null;

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
        ScalePlayerMask((InputManager.GetRequestedPlayerInput(TapType.Left, true)));
        if ((InputManager.GetRequestedPlayerInput(TapType.Left, false)))
            _auraAnimator.SetTrigger("AuraPopup");
        if (InputManager.GetRequestedPlayerInput(TapType.Right))
            Jump();
    }

    private void FixedUpdate()
    {
        if (_playerState != PlayerState.Sliding)
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
        if (InterfaceRun.ThisScript != null)
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
                _playerAnimator.ResetTrigger("Land");
                _playerAnimator.SetTrigger("Jump");
                _playerSpiritAnimator.ResetTrigger("Land");
                _playerSpiritAnimator.SetTrigger("Jump");
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
        if (InterfaceRun.ThisScript != null)
            InterfaceRun.ThrowLine();
        _hookshotWhoosh.Play();
        while (InputManager.GetRequestedPlayerInput(TapType.Right, true))
        {
            if (_playerState == PlayerState.Sliding)
            {
                Debug.DrawLine(_hookshot.transform.position, _hookshot.transform.position + _hookshot.GetNormalizedDirectionVector(transform.position) * _hookshot.HookshotDistance, Color.red);
                transform.position = new Vector3(transform.position.x, _hookshot.transform.position.y + _hookshot.GetNormalizedDirectionVector(transform.position).y * _hookshot.HookshotDistance, transform.position.z);
                if (Mathf.Acos(_hookshot.GetNormalizedDirectionVector(transform.position).y / _hookshot.GetNormalizedDirectionVector(transform.position).x) > Mathf.PI / 4f)
                    break;
            }
            else
                _hookshot.MoveHook(_hookshotMoveVector);
            yield return null;
        }
        if (_playerState == PlayerState.Sliding)
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
        Debug.Log("PLAYER IS DEAD");
        Time.timeScale = 0f;
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
        switch (collision.collider.tag)
        {
            case "GROUND":
                if (PlayerLanded != null)
                {
                    _playerAnimator.ResetTrigger("Jump");
                    _playerAnimator.SetTrigger("Land");
                    _playerSpiritAnimator.ResetTrigger("Jump");
                    _playerSpiritAnimator.SetTrigger("Land");
                    PlayerLanded();
                }
                break;
            case "OBSTACLE":
                if (_isMaskOpened)
                    return;
                KillPlayer();
                break;
            case "KILLZONE":
                KillPlayer();
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "GROUND")
            transform.position = new Vector3(transform.position.x, collision.contacts[0].point.y + transform.localScale.y * 0.5f);
        _playerState = PlayerState.Running;
        _speedY = 0f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_playerState != PlayerState.Jumping)
            _playerState = PlayerState.Falling;
        if (PlayerExitGround != null && collision.collider.tag == "GROUND")
            PlayerExitGround();
    }
}
