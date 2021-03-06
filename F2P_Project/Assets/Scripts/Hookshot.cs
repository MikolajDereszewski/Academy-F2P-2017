﻿using UnityEngine;
using System;
using GameClasses;

public class Hookshot : MonoBehaviour
{

    public event Action HitTree;
    public event Action HitNothing;

    [SerializeField]
    private SpriteRenderer _renderer = null;
    [SerializeField]
    private LineRenderer _lineRenderer = null;

    public float HookshotDistance = 0f;

    private float _lockedYPosition = 0f;

    private void Update()
    {
        _renderer.enabled = (transform.localPosition != Vector3.zero);
        if (transform.position.y >= Camera.main.ViewportToWorldPoint(Vector3.up).y && HitNothing != null)
            HitNothing();
        if (HookshotDistance != 0f)
            transform.position = Vector3.left * DifficultyManager.GetGameSpeed() * Time.deltaTime + new Vector3(transform.position.x, _lockedYPosition, transform.position.z);
        if(transform.position != transform.parent.position)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.parent.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HookshotDistance != 0f)
            return;
        if (other.tag == "LEAVES" && HitTree != null)
        {
            _lockedYPosition = transform.position.y;
            HitTree();
        }
    }

    public void ResetPosition(Vector3 position)
    {
        Vector3[] resetVectors = { Vector3.zero, Vector3.zero };
        _lineRenderer.SetPositions(resetVectors);
        transform.position = position;
        HookshotDistance = 0f;
        _lockedYPosition = 0f;
    }

    public void MoveHook(Vector3 deltaMove)
    {
        transform.position += deltaMove * Time.deltaTime;
    }

    public Vector3 GetNormalizedDirectionVector(Vector3 objectPosition)
    {
        return (objectPosition - transform.position).normalized;
    }
}
