using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Sword")]
    public GameObject swordInShell;
    public GameObject swordInHand;
    public BoxCollider swordHitCollider;

    [Header("Spell")]
    public ParticleSystem spellCharge;
    public GameObject spellProjectile;
    public Transform spellAttackPoint;

    [Header("Dash")]
    public ParticleSystem dashParticle;
    public BoxCollider dashCollider;
    
    CharacterController characterController;
    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate() {
        if(characterController.anim.GetBool("IsArmed")){
            swordInShell.SetActive(false);
            swordInHand.SetActive(true);
        }else{
            swordInShell.SetActive(true);
            swordInHand.SetActive(false);
            swordHitCollider.GetComponent<Damage>().isArmed = characterController.isArmed;
        }
    }
    public void SwordHit(){
        swordHitCollider.GetComponent<Damage>().isArmed = characterController.isArmed;
        swordHitCollider.enabled = true;
    }
    public void SwordHitClose(){
        swordHitCollider.enabled = false;
    }
    public void SpellCharge(){
        spellCharge.Play();
    }
    public void SpellAttack(){
        Instantiate(spellProjectile, spellAttackPoint.position, transform.rotation);
        characterController.anim.SetInteger("AttackType", 0);
    }
}
