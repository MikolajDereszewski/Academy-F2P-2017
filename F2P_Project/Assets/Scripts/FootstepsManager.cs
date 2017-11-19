using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameClasses;

public class FootstepsManager : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> _footsteps;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private Player _player;

    private void Start()
    {
        _player.PlayerLanded += OnPlayerLanded;
        _player.PlayerExitGround += OnPlayerExitGround;
        _player.PlayerDied += OnPlayerDied;
    }

    private void PlayNextSound()
    {
        if (_player.CurrentState == PlayerState.Dead)
            return;
        int next = Random.Range(0, _footsteps.Count);
        _audio.clip = _footsteps[next];
        _audio.Play();
        Invoke("PlayNextSound", _footsteps[next].length / 2f + Random.Range(-0.1f, 0f));
    }

    private void OnPlayerLanded()
    {
        if (_player.CurrentState == PlayerState.Running)
            return;
        PlayNextSound();
    }

    private void OnPlayerExitGround()
    {
        _audio.Pause();
        CancelInvoke();
    }

    private void OnPlayerDied()
    {
        OnPlayerExitGround();
        this.enabled = false;
    }
}
