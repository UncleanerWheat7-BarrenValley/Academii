using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]
    FlameGun flameGun;

    public void FireFlameProjectile() 
    {
        flameGun.FireFlame();
    }
}
