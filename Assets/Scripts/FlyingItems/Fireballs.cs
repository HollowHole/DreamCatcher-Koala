using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fireballs : Ball
{
    [SerializeField] Sprite HostileImage;
    [SerializeField] Sprite FriendlyImage;
    [SerializeField] RuntimeAnimatorController HostileAnimCrl;
    [SerializeField] RuntimeAnimatorController FriendlyAnimCrl;
    [SerializeField] AudioClip explodeSoundClip;
    public override void UpdateView()
    {
        if (isHostile)
        {
            mImg.sprite = HostileImage;
            animator.runtimeAnimatorController = HostileAnimCrl;
        }
        else
        {
            mImg.sprite = FriendlyImage;
            animator.runtimeAnimatorController = FriendlyAnimCrl;
        }
    }
    //private void Update()
    //{
    //    base.Update();
    //}
    new private void OnDestroy()
    {
        base.OnDestroy();
        if (explodeSoundClip != null)
        {
            if(BGMPlay.instance != null)
            BGMPlay.instance.PlayMusic(explodeSoundClip);
        }
        
    }
}
