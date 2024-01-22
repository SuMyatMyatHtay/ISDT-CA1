using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Ani : MonoBehaviour
{
    public Enemy3Main Enemy3Main;
    // Start is called before the first frame update
   public void hurtDone()
    {
        Enemy3Main.hurtDone();
    }

    public void Death()
    {
        Enemy3Main.Death();
    }

    public void startDeath()
    {
        Enemy3Main.startDeath();
    }

    public void disappear()
    {
        Enemy3Main.disappear();
    }

    public void Attack1()
    {
        Enemy3Main.Attack1();
    }

    public void Attack2()
    {
        Enemy3Main.Attack2();
    }

    public void Attack3()
    {
        Enemy3Main.Attack3();
    }

    public void Attack1Done()
    {
        Enemy3Main.Attack1Done();
    }
    public void Attack2Done()
    {
        Enemy3Main.Attack2Done();
    }
    public void Attack3Done()
    {
        Enemy3Main.Attack3Done();
    }
}
