using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    float speed {  get; set; }
    int Health { get; set; }
    void damage(int damage);
}
