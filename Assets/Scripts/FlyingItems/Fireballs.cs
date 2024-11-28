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
}
