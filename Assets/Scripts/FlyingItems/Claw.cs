using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Claw : Ball
{
    public enum Direction { Left, Right }
    public float Length;
    public float strechOutTime;
    public float strechBackTime;
    Direction m_direction;
    bool alerting;
    
    public AudioClip appearSoundClip;
    public override void UpdateView()
    {
        //Do nothing
    }
    new private void Awake()
    {
        base.Awake();
        StartCoroutine(Func());
    }
    new private void Update()
    {
        base.Update();
        if (alerting)
        {
            //显示黄色叹号
            
            if (appearSoundClip != null)
            {
                if (BGMPlay.instance != null)
                    BGMPlay.instance.PlayMusic(appearSoundClip);
            }
        }
    }
    public void SetDirection(Direction direction)
    {
        m_direction = direction;
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HitPlayer(other.gameObject.GetComponent<PlayerController>());
        }
    }
    protected override void HitPlayer(PlayerController player)
    {
        if (isHostile)
            player.Hp--;
    }

    IEnumerator Func()
    {
        
        alerting = true;
        yield return new WaitForSeconds(3);
        alerting = false;
        int direction = 1;
        if (m_direction == Direction.Right)
        {
            direction = -1;
        }
        Tweener tweener = transform.DOMoveX(transform.position.x + direction * Length, strechOutTime);
        tweener.SetAutoKill(false);
        while (!tweener.IsComplete())
        {
            yield return null;
        }

        Friendly = true;

        tweener = transform.DOMoveX(transform.position.x - direction * Length, strechBackTime);
        tweener.SetAutoKill(false);
        while (!tweener.IsComplete())
        {
            yield return null;
        }
        Destroy(gameObject);

    }
    protected override void HandleRotation()
    {
        //Do nothing
    }
}
