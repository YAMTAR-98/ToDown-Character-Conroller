using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageValue;
    public float damageValueTemp;
    public float hitForce = 1f;
    internal bool isArmed;

    private void Awake() {
        damageValueTemp = damageValue;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            if(!isArmed)
                damageValue = damageValue / 2f;
            other.gameObject.GetComponent<Health>().TakeDamage(damageValue);
            Vector3 force = hitForce * transform.forward;
            Vector3 upForce = hitForce / 4 * transform.up;
            other.gameObject.GetComponent<Rigidbody>().AddForce(upForce, ForceMode.Impulse);
            other.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            damageValue = damageValueTemp;
        }
    }
}
