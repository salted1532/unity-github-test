using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy SAO", menuName = "Scriptable Object/Enemy SAO", order = int.MaxValue)]

public class EnemySAO : ScriptableObject
{
    [SerializeField]
    private string e_name;
    [SerializeField]
    public string E_name { get { return e_name;  } }
    [SerializeField]
    private int what_e;
    [SerializeField]
    public int What_E { get { return what_e; } }
    [SerializeField]
    private int hp;
    [SerializeField]
    public int Hp { get { return hp; } }
    [SerializeField]
    private int shiled;
    [SerializeField]
    public int Shiled { get { return shiled; } }
    [SerializeField]
    private int damage;
    [SerializeField]
    public int Damage { get { return Damage; } }

}
