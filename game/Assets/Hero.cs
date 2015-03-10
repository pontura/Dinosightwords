using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private Animator animator;
    public states state;
    private Collider2D collider;

    public enum states
    {
        IDLE,
        RUN,
        JUMP
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        Events.OnHeroJump += OnHeroJump;
        collider = GetComponent<Collider2D>();
    }
    void OnDestroy()
    {
        Events.OnHeroJump -= OnHeroJump;
    }
    void OnHeroJump()
    {
        Jump();
        collider.enabled = false;
    }
    public void Jump()
    {
        if (state == states.JUMP) return;
        state = states.JUMP;
        animator.SetBool("isJumping", true);
    }
    public void EndJump()
    {
        collider.enabled = true;
        state = states.RUN;
        animator.SetBool("isJumping", false);
    }
}
