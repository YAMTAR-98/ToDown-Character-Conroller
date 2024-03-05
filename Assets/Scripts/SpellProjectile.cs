using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpellProjectile : MonoBehaviour
{
    public float damageValue;
    public float hitForce = 1f;
    public float speed;


    private void Start() {
        Invoke("DestroyObject", 1.2f);
    }
    private void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.GetComponent<Health>().TakeDamage(damageValue);
            Vector3 force = hitForce * transform.forward;
            Vector3 upForce = hitForce / 4 * transform.up;
            other.gameObject.GetComponent<Rigidbody>().AddForce(upForce, ForceMode.Impulse);
            other.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
    void DestroyObject(){
        Destroy(gameObject);
    }
}

