using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public float health;
    public bool isEnemy;
    EnemyBrain_Stupid enemyBrain;
    Material originalMaterial;
    MeshRenderer meshRenderer;
    Collider coll;
    public Material hitFlash;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
        enemyBrain = GetComponent<EnemyBrain_Stupid>();
        originalMaterial = meshRenderer.material; 
    }
    public void TakeDamage(float damage){
        if(isEnemy){
            enemyBrain.Stun();
            GetComponent<NavMeshAgent>().enabled = false;
        //    GetComponent<Rigidbody>().drag = 0;
            StartCoroutine(Hit()); 
            health -= damage;
            if(health <= 0)
                enemyBrain.Die();

                //Destroy(gameObject);
        }else{
            
        }
    }
    IEnumerator Hit(){
        
        coll.enabled = false;
        meshRenderer.material = hitFlash;
        yield return new WaitForSeconds(0.25f);
     //   GetComponent<Rigidbody>().drag = 1;
        coll.enabled = true;
        meshRenderer.material = originalMaterial;
        yield return new WaitForSeconds(1f);
        GetComponent<NavMeshAgent>().enabled = true;
        enemyBrain.UnStun();
    }
}
