using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
// 이 게임 오브젝트에서 Animator 컴퍼넌트가 없으면 안된다.
// 개발자가 실수로 Animator 컴퍼넌트를 삭제 하면 경고를 띄운다.
public class Player : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Rigidbody rb;
    float h=0f,v=0f;
    float lastSkillTime = 0f;
    float lastAttacktime = 0f;
    float lastDashAttacktime = 0f;
    bool isAttacking = false; //공격중 아니냐 판단
    bool isDashAttack = false;
    bool isSkill = false;
    IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        yield return null;
    }
    public void OnStickPos(Vector3 Stickpos)
    {
        h = Stickpos.x;
        v = Stickpos.y;

    }
    void Update()
    {
        if(animator != null)
        {
            animator.SetFloat("Speed",(h*h +v*v));
            if(rb != null)
            {            //리지디바디의 속도
                Vector3 speed = rb.velocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
                rb.velocity = speed;
                if(h!=0f&&v!=0f)
                {
                    transform.rotation =
                        Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }

        }
        
    }
    public void OnAttackDown()
    {
        isAttacking = true;
        animator.SetBool("ComboAttack",isAttacking);
        StartCoroutine(ComoboAttackTiming());
        // 개발자 원하는 프레임 장면을 따로 만들고자 할 때
    }
    public void OnAttackUp()
    {
        isAttacking = false;
        animator.SetBool("ComboAttack", isAttacking);
    }
    IEnumerator ComoboAttackTiming()
    {
        if (Time.time - lastAttacktime > 1f)
        {
            lastAttacktime = Time.time;
            while (isAttacking)
            {
                animator.SetBool("ComboAttack", true);
                yield return new WaitForSeconds(1f);
                //콤보어택 까지의 시간 간격
            }

        }
    }
    public void OnSkillDown()
    {
        isSkill = true;
        if (Time.time -lastSkillTime >1f) 
        {   

            animator.SetTrigger("SkillTrigger");
            lastSkillTime = Time.time;
        }
    }
    public void OnSkillUp()
    {
        isSkill=false;
    }
    public void DashAttackDown()
    {
        if(Time.time - lastDashAttacktime > 1f)
        {
            isDashAttack = true;
            lastDashAttacktime = Time.time;
            animator.SetTrigger("DashAttackTrigger");
        }
        
    }
    public void DashAttackup()
    {
        isDashAttack = false;
    }
}
