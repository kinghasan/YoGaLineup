using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public Animator animator;
    private float TargetValue;
    private float nowValue;
    private AnimatorOverrideController anim;

    private void Start()
    {
        //animator.Play("Yoga8");
        animator.speed = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TargetValue = 0f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TargetValue = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TargetValue = 1f;
        }

        var value = Mathf.Lerp(nowValue, TargetValue, 0.05f);
        nowValue = value;
        animator.Play("Yoga8", 0, value);
    }
}
