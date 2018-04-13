using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    public Animator GetAnimator
    {
        get;
        protected set;
    }

    [HideInInspector]
    public Rigidbody2D Rigid;

    public int Health;

    public virtual void OnHurt(int amount)
    {
        Health -= amount;
    }

}
