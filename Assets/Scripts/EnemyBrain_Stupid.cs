using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyBrain_Stupid : MonoBehaviour
{
    public GameObject target;
    float attackDistance;
    internal Animator anim; 
    private NavMeshAgent navMeshAgent;
    public GameObject hitPoint;
    bool isStunned;
    private void Awake() {
        
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        attackDistance = navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        if(target != null && !isStunned){
            bool inRange = Vector3.Distance(transform.position, target.transform.position) <= attackDistance;

            if(inRange)
                LookAtTarget();
            else
                UpdatePath();

            if(anim.GetBool("Die")){
                
                anim.SetBool("Attack", false);
            }else{
                int random = Random.Range(0, 2);
                anim.SetInteger("Random", random);
                anim.SetBool("Attack", inRange);
            }
        }
    }
    void UpdatePath(){
        if(navMeshAgent.enabled == true)
            navMeshAgent.SetDestination(target.transform.position);
    }
    void LookAtTarget(){
        Vector3 lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2.0f);
    } 
    internal void Die(){
        anim.SetBool("Die", true);
        target = null;
    }
    public void HitPlayer(){
        hitPoint.SetActive(true);
    }
    public void CloseTrigger(){
        hitPoint.SetActive(false);
    }
    public void Stun(){
        isStunned = true;
        anim.SetTrigger("Hit");
        
    }
    public void UnStun(){
        isStunned = false;
        anim.ResetTrigger("Hit");
    }
    
}