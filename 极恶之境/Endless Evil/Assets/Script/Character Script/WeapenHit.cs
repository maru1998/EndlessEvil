using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapenHit : MonoBehaviour
{
    public Collider2D weapenHitBox;
    public float damage;
    public Animator animator;
    public GameObject charactor;

    public bool Attacking = false;
    public bool Attacked = false;
    // Start is called before the first frame update
    void Start()
    {
        weapenHitBox = GetComponent<BoxCollider2D>();
        animator = charactor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        getHit();
        damage = 100;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") && Attacking && !Attacked)
        {
            Debug.Log("hitted");
            Attacked = true;
        }
    }
    private bool getHit()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SlashMelee1H") || animator.GetCurrentAnimatorStateInfo(0).IsName("JabMelee1H") && !Attacking && !Attacked)
        {
            Attacking = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SlashMelee1H") || !animator.GetCurrentAnimatorStateInfo(0).IsName("JabMelee1H") && Attacked)
        {
            Attacking = false;
            Attacked = false;
        }
        return false;
    }
}
