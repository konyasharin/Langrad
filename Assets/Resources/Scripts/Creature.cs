using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [Min(0.1f)] public float speed = 1f;
    protected Animator AnimatorController;
    protected SpriteRenderer SpriteRenderer;

    private void Start()
    {
        AnimatorController = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
