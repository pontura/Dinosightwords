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
        JUMP,
        CRASH
    }
    void Start()
    {
        Events.OnHeroJump += OnHeroJump;
        Events.OnHeroCrash += OnHeroCrash;

        animator = GetComponent<Animator>();       
        collider = GetComponent<Collider2D>();
    }
    void OnDestroy()
    {
        Events.OnHeroJump -= OnHeroJump;
        Events.OnHeroCrash -= OnHeroCrash;
    }
    void OnHeroJump()
    {
        Jump();
        collider.enabled = false;
    }
    void OnHeroCrash()
    {
        Crash();
        collider.enabled = false;
    }
    void Crash()
    {
        if (state == states.CRASH) return;
        state = states.CRASH;
        animator.SetBool("isCrashing", true);
    }
    void Jump()
    {
        if (state == states.JUMP) return;
        state = states.JUMP;
        animator.SetBool("isJumping", true);
    }
    public void EndCrash()
    {
        collider.enabled = true;
        state = states.RUN;
        animator.SetBool("isCrashing", false);
    }
    public void EndJump()
    {
        collider.enabled = true;
        state = states.RUN;
        animator.SetBool("isJumping", false);
    }
}
