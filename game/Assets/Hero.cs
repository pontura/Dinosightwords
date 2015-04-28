using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private Animator animator;
    public states state;
    private Collider2D collider2d;
    private Game game;

    public enum states
    {
        IDLE,
        RUN,
        JUMP,
        CRASH,
        SLIDE,
        CELEBRATE,
        UNHAPPY,
        WIN
    }
    void Start()
    {
        this.game = Game.Instance;
        Events.StartGame += StartGame;
        Events.OnHeroJump += OnHeroJump;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.OnHeroCelebrate += OnHeroCelebrate;
        Events.OnHeroUnhappy += OnHeroUnhappy;
        Events.OnLevelComplete += OnLevelComplete;
        

        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }
    void OnDestroy()
    {
        Events.StartGame -= StartGame;
        Events.OnHeroJump -= OnHeroJump;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.OnHeroCelebrate -= OnHeroCelebrate;
        Events.OnHeroUnhappy -= OnHeroUnhappy;
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void StartGame()
    {
        ResetAnimation();
    }
    void OnHeroJump()
    {
        Jump();
        collider2d.enabled = false;
    }
    void OnHeroSlide()
    {
        Slide();
      //  collider.enabled = false;
    }
    void OnHeroCrash()
    {
        Crash();
       // collider.enabled = false;
    }
    void OnHeroCelebrate()
    {
        Celebrate();
    }
    void OnHeroUnhappy()
    {
        Unhappy();
    }
    void Slide()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.SLIDE) return;
        state = states.SLIDE;
        animator.SetBool(state.ToString(), true);
    }
    void Crash()
    {
        if (state == states.CRASH) return;
        state = states.CRASH;
        animator.SetBool(state.ToString(), true);
    }
    void Jump()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.JUMP) return;
        Events.OnSoundFX("jump");
        state = states.JUMP;
        animator.SetBool(state.ToString(), true);
    }
    void Celebrate()
    {
        if (state == states.JUMP) return;
        state = states.CELEBRATE;
        animator.SetBool(state.ToString(), true);
    }
    void Unhappy()
    {
        if (state == states.JUMP) return;
        state = states.UNHAPPY;
        animator.SetBool(state.ToString(), true);
    }
    void OnLevelComplete()
    {
        if (state == states.WIN) return;
        state = states.WIN;
        animator.SetBool(state.ToString(), true);
    }
    public void ResetAnimation()
    {
        if (game.state == Game.states.PAUSED) return;
        collider2d.enabled = true;
        state = states.RUN;
        animator.SetBool("JUMP", false);
        animator.SetBool("CRASH", false);
        animator.SetBool("SLIDE", false);
        animator.SetBool("CELEBRATE", false);
        animator.SetBool("UNHAPPY", false);
        animator.SetBool("IDLE", false);
    }
}
