using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Skill : MonoBehaviour
{
    public Animator animator;
    public string skillOn = "SkillTrigger";
    public string ComboAttack = "ComboAttack";
    public string DashAttack = "DashAttackTrigger";
    public AudioSource audioSource;
    public AudioClip audioClip;

    float lastAttacktime = 0f;
    float lastSkilltime = 0f;
    float lastDashAttacktime = 0f;
    bool isAttacking = false;//공격중인지 아닌지 판단
    bool isDashAttack = false;//대쉬 판단
    bool isSkill = false;

    void Start()
    {
        animator = GetComponent<Animator>();
         
    }
    public void OnAttackDown()
    {
        isAttacking = true;
        animator.SetBool("ComboAttack", true);
        StartCoroutine(ComboAttackTiming());
    }   //개발자가 원하는 프레임 장면을 따로 만들고자 할 때
    public void OnAttackUP()
    {
        isAttacking = false;
        animator.SetBool("ComboAttack", false);
    }
    IEnumerator ComboAttackTiming()
    {
        if (Time.time - lastAttacktime > 1f)
        {
            lastAttacktime = Time.time;
            while (isAttacking)
            {
                animator.SetBool("ComboAttack", true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    void Update()
    {
        


    }
    public void SkillPlayDown()
    {
        isSkill = true;
        
        
        StartCoroutine(SkillAttackTiming());
    }
    public void SkillPlayUp()
    {
        isSkill = false;
        
    }
    IEnumerator SkillAttackTiming()
    {
        if (Time.time - lastSkilltime > 1f)
        {
            lastSkilltime = Time.time;
            while (isSkill)
            {
                animator.SetTrigger(skillOn);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    public void DashAttackPlayDown()
    {
        isDashAttack = true;
       // animator.applyRootMotion = true;
        StartCoroutine(DashAttackTiming());
    }
    public void DashAttackPlayUp()
    {
        isDashAttack = false;
        //animator.applyRootMotion =false;
    }
    IEnumerator DashAttackTiming()
    {
        if (Time.time - lastDashAttacktime > 1f)
        {
            lastDashAttacktime = Time.time;
            while (isDashAttack)
            {
                animator.SetTrigger(DashAttack);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    public void SkillSound()
    { 
        audioSource.PlayOneShot(audioClip,1.0f);
    }
}
