using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public const int health = 30;
    public int CurrentHp = health;
    public Camera Main;
    public GameObject enemy;
    public Slider Hpslider;
    public Animator anim;

    public bool dead = false;
    float Speed;

    public void Start()
    {
        Hpslider.maxValue = CurrentHp;
    }

    public void TakeDamage(int amount)
    {
        CurrentHp -= amount;
        healthChange();

    }
    void healthChange()
    {
        Hpslider.value = CurrentHp;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("공격받음");
            CurrentHp -= 1;
        }
        Hpslider.value = CurrentHp;
        if (CurrentHp <= 0 && !dead)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        print("Enemy Died");
        Destroy(enemy, 0.5f);
    }
}
