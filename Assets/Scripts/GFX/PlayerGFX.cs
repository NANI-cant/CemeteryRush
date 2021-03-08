using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    public Sprite SpriteTop;
    public Sprite SpriteDown;
    public Sprite SpriteLeft;
    public Animator _animator;
    public bool isRunning;
    public int direction;

    private void Update(){
        _animator.SetBool("isRunning", isRunning);
        switch (direction)
        {
            case 0: SetTop();
            break;
            case 1: SetRight();
            break;
            case 2: SetDown();
            break;
            case 3: SetLeft();
            break;
        }
    }

    public void SetTop(){
        _animator.SetBool("Top", true);
        _animator.SetBool("Left", false);
    }

    public void SetDown(){
        _animator.SetBool("Top", false);
        _animator.SetBool("Left", false);
    }

    public void SetLeft(){
        _animator.SetBool("Top", false);
        _animator.SetBool("Left", true);
        transform.localScale = new Vector3(1f,1f,1f);
    }

    public void SetRight(){
        _animator.SetBool("Top", false);
        _animator.SetBool("Left", true);
        transform.localScale = new Vector3(-1f,1f,1f);
    }
}
