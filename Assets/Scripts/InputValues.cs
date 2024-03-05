using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class InputValues : MonoBehaviour
{
    public InputActionReference attackInput;
    public InputActionReference defanceInput;
    public InputActionReference combatTypeInput;
    public InputActionReference spellInput;
    public InputActionReference dashInput;
    public InputActionReference jumpInput;
    public float delay = 2f;

    CharacterController characterController;
    EquipmentController equipment;
    public int attackCount = 0;
    bool startCombo;
    bool dash;
    float dashTimer = 0.25f;
    float jumptTimer = 0.25f;
    float nextTimeToJump = 1f;
    bool canJump;
    float nexTimeToPress = 1f;
    public float comboTime = 3f;
    public float comboTimeConst;
    public int totalComboAttackCount = 4;
    private void Start() {
        characterController = GetComponent<CharacterController>();
        equipment = GetComponent<EquipmentController>();
        comboTimeConst = comboTime;
    }
    private void Update() {
        Debug.Log(characterController.characterController.isGrounded);
        if(attackInput.action.WasPressedThisFrame()){
            characterController.Combat(attackCount);
            startCombo = true;
            if(attackCount == totalComboAttackCount){
                attackCount = 0;
            }
        }
        if(defanceInput.action.IsPressed()){
            characterController.Defance(true);
        }else{
            characterController.Defance(false);
        }

        if(combatTypeInput.action.WasPressedThisFrame()){
            characterController.CombatType();
        }

        if(jumpInput.action.IsPressed() && canJump){
            jumptTimer -= Time.deltaTime;
            if(jumptTimer >= 0){
                characterController.Jump(true);
            }else{
                canJump = false;
                characterController.Jump(false);
            }
        }else{
            nextTimeToJump -= Time.deltaTime;
            jumptTimer = 0.25f;
        }
        if(jumpInput.action.WasReleasedThisFrame()){
            characterController.Jump(false);
            canJump = false;
            
            if(nextTimeToJump < 0){
                canJump = true;
                nextTimeToJump = 1f;
            }
        }
        

        if(startCombo){
            comboTimeConst -= Time.deltaTime;
            if(comboTimeConst <= 0){
                attackCount = 1;
                startCombo = false;
                comboTimeConst = comboTime;
            }else if(attackInput.action.WasPressedThisFrame() && comboTimeConst > 0){
                attackCount++;
                comboTimeConst = comboTime;
            }
        }

        if(spellInput.action.IsPressed()){
            delay -= Time.deltaTime;
            characterController.canMove = false;
            equipment.SpellCharge();
        }

        if(spellInput.action.WasReleasedThisFrame()){
            if(delay <= 0){

                characterController.SpellAttack();
                characterController.delay = 1.8f;
            }
            characterController.canMove = true;
            delay = 2f;
        }

        if(dashInput.action.WasPressedThisFrame() && nexTimeToPress <= 0){
            characterController.Dash(true);
            dash = true;
            nexTimeToPress = 1f;
        }
        if(dash){
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0){
                characterController.Dash(false);
                dash = false;
                dashTimer = 0.25f;
            }
        }

        if(!characterController.anim.GetBool("IsArmed")){totalComboAttackCount = 2;}
        else{totalComboAttackCount = 5;}
        
        if(nexTimeToPress > 0){nexTimeToPress -= Time.deltaTime;}
        
    }
}
