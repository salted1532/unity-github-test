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
    tunnelAi TAi;
    public EnemySAO E_data;

    public void Start()
    {
        EAi = GetComponent<EnemyAi>();
        TAi = GetComponent<tunnelAi>();
        Hpslider.maxValue = CurrentHp;
        Hpslider.value = CurrentHp;
    }
    //데미지 받아오기 
    public void TakeDamage(int amount)
    {
        int TDamage = 0;
        TDamage = amount - E_data.Shiled;
        if(TDamage > 0)
        {
            CurrentHp -= TDamage;
        }
        Hpslider.value = CurrentHp;

    }

    private void Update()
    {
        //죽었는지 확인
        if (CurrentHp <= 0 && !dead)
        {
            TAi.Die();
            EAi.Die();
        }
    }
}
