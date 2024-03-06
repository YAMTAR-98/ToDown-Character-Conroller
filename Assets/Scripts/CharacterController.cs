using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction movementAction;
    internal Animator anim;
    internal UnityEngine.CharacterController characterController;
    EquipmentController equipment;
    Vector3 move;
    bool inCombat;
    bool stun;
    internal bool isArmed;
    internal float movementSpeedTemp;
    internal float delay = 2f;
    float defaultAnimSpeed;
    internal bool canMove = true;
    [SerializeField] internal float movementSpeed;
    [SerializeField] internal float dashSpeed;
    [SerializeField] internal float jumpForce;
    [SerializeField] internal float smoothRotation = 0.05f;
    float _currentVelocity;
    

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<UnityEngine.CharacterController>();
        equipment = GetComponent<EquipmentController>();
        anim = GetComponent<Animator>();
        defaultAnimSpeed = anim.speed;
        movementAction = playerInput.actions.FindAction("Movement");
        dashSpeed = movementSpeed * 10f;
        movementSpeedTemp = movementSpeed;
    }

    private void Update() {
        //if(stun)
            //return;
        if(canMove && delay >= 1.9f){
            PlayerMovement();
        } 
        else{
            anim.SetFloat("Speed", 0);
            delay -= Time.deltaTime;
            if(delay <= 0){
                delay = 2;
            }
        } 
    }

    void PlayerMovement(){
        if(stun)
            return;

        Vector2 direction = movementAction.ReadValue<Vector2>();
       
        
        move = new Vector3(direction.x, 0, direction.y);
        float animSpeed = move.magnitude;
        
        
        characterController.Move(move * movementSpeed * Time.deltaTime);
        anim.SetFloat("Speed", animSpeed);
        
        if(direction.sqrMagnitude == 0)
            return;

        var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothRotation);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        //move.y = 0;
        //transform.rotation = Quaternion.LookRotation(move);
        //if(move.magnitude >= 0)
            
        //else
            Debug.Log(animSpeed);

        
        if(animSpeed <= 0.1f){
            //TODO: Mouse LookAt 
        }
    }
    internal void Jump(bool animate){
        if(stun)
            return;

        anim.SetBool("Jump", animate);
        float var;
        if(isArmed)
            var = movementSpeed * 2;
        else
            var = movementSpeed;
            Vector3 jump = new Vector3(move.x * var, jumpForce, move.z * var);
        characterController.Move(jump * Time.deltaTime);

    }
    
    internal void Combat(int combatType){
        if(stun)
            return;

        anim.SetInteger("AttackType", combatType);
        anim.SetTrigger("Attack");
    }
    internal void Defance(bool block){
        if(stun)
            return;

        anim.SetBool("Defance", block);
    }
    internal void CombatType(){
        if(stun)
            return;

        inCombat = !inCombat;
        isArmed = inCombat;
        anim.SetBool("Combat", inCombat);
        anim.SetBool("IsArmed", isArmed);
    }
    internal void SpellAttack(){
        if(stun)
            return;

        anim.SetTrigger("Attack");
        anim.SetInteger("AttackType", 5);
    }
    
    internal void Dash(bool dash){
        if(stun)
            return;

            
        if(dash){
            movementSpeed = dashSpeed;
            anim.speed = 0;
            equipment.dashParticle.Play();
            equipment.dashCollider.enabled = true;
        } 
        else{
            movementSpeed = movementSpeedTemp;
            anim.speed = defaultAnimSpeed;
            equipment.dashParticle.Stop();
            equipment.dashCollider.enabled = false;
        }
    }
    internal void Stunned(){
        stun = true;
        anim.SetBool("Stunned", stun);
        
    }
    public void StunComplate(){
        stun = false;
        anim.SetBool("Stunned", stun);
    }
    
}
