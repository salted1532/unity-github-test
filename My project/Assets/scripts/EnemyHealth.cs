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

    EnemyAi EAi;

    public void Start()
    {
        EAi = GameObject.Find("MovementAi").GetComponent<EnemyAi>();
        Hpslider.maxValue = CurrentHp;
        Hpslider.value = CurrentHp;
    }
    //������ �޾ƿ��� 
    public void TakeDamage(int amount)
    {
        CurrentHp -= amount;
        Hpslider.value = CurrentHp;

    }

    private void Update()
    {
        //�׾����� Ȯ��
        if (CurrentHp <= 0 && !dead)
        {
            EAi.Die();
        }
    }
}
