using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using Utilities;

public class EnemyAnimation : MonoBehaviour, IAIAttribute
{
    public Animator _anim;
    private EnemyAction enemyAction;
    public GameObject player;
    public GameObject enemy;
    public Collider enemyWeapon;
    public GameObject leftFoot;
    public GameObject rightFoot;
    FMODUnity.StudioEventEmitter leftFootStepEmitter;
    FMODUnity.StudioEventEmitter rightFootStepEmitter;
    public FMODUnity.StudioEventEmitter jump;
    private FMOD.Studio.EventInstance enemySound;

    public string textureName;

    void Start()
    {
        _anim = GetComponent<Animator>();
        enemyAction = GetComponent<EnemyAction>();
        leftFootStepEmitter = leftFoot.GetComponent<FMODUnity.StudioEventEmitter>();
        rightFootStepEmitter = rightFoot.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    void FixedUpdate()
    {
        initialiseAnimatorBool();
    }

    void initialiseAnimatorBool()
    {
        _anim.SetBool("isAttacking", enemyWeapon.isTrigger);
        _anim.SetBool("isKeepBlocking", enemyAction.isKeepBlocking);
        _anim.SetBool("isPerfectBlock", enemyAction.isPerfectBlock);
        _anim.SetBool("isInPerfectBlockOnly", enemyAction.isInPerfectBlockOnly);
    }

    #region Enemy Attack Logic
    public void OnAnimation_IsHeavyAttackActive()
    {
        enemyWeapon.isTrigger = false;
    }

    public void OnAnimation_IsHeavyAttackDeactive()
    {
        enemyWeapon.isTrigger = true;
    }

    public void OnAnimation_IsLightAttackActive()
    {
        enemyWeapon.isTrigger = false;
    }

    public void OnAnimation_IsLightAttackDeactive()
    {
        enemyWeapon.isTrigger = true;
    }

    public void OnAnimation_StopAttackCollision()
    {
        enemyWeapon.isTrigger = true;
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Block_Impact");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.enemyWeapon.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }
    #endregion

    #region Enemy Block Logic
    public void OnAnimation_isBlockStart()
    {
        enemyAction.isKeepBlocking = true;
    }

    public void OnAnimation_BlockStart()
    {
        enemyAction.isKeepBlocking = true;
    }

    public void OnAnimation_isPerfectBlock()
    {
        enemyAction.isPerfectBlock = true;
    }

    public void OnAnimation_isPerfectBlockEnd()
    {
        enemyAction.isPerfectBlock = false;
    }
    #endregion

    #region Enemy Get Hurt Logic
    public void OnAnimation_isGetCriticalHit()
    {
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/Injured");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/hitBody");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }
    #endregion

    public void OnAnimation_isBlockStun()
    {

    }

    public void OnAnimation_isStunFinished()
    {

    }

    public void OnAnimation_isBlockStunFinished()
    {

    }

    #region FMOD Sound
    #region FootStep
    public void OnAnimation_LeftWalkFootStep()
    {
        leftFootStepEmitter.Play();
    }

    public void OnAnimation_RightWalkFootStep()
    {
        rightFootStepEmitter.Play();
    }

    public void OnAnimation_Jump()
    {
        jump.Play();
    }
    #endregion

    #region Combat
    public void OnAnimation_SwingWeapon()
    {
        enemyWeapon.GetComponent<FMODUnity.StudioEventEmitter>().Play();
    }

    public void OnAnimation_RaiseSword()
    {
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Sword_Block");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.enemyWeapon.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }

    public void OnAnimation_isLightAttacking()
    {
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/Shout");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }
    #endregion
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grass"))
        {
            textureName = other.tag;
            leftFootStepEmitter.SetParameter("Terrain", 0.0f);
            rightFootStepEmitter.SetParameter("Terrain", 0.0f);
        }
        if (other.CompareTag("Stone"))
        {
            textureName = other.tag;
            leftFootStepEmitter.SetParameter("Terrain", 2.0f);
            rightFootStepEmitter.SetParameter("Terrain", 2.0f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Bridge"))
        {
            textureName = collision.collider.tag;
            leftFootStepEmitter.SetParameter("Terrain", 1.0f);
            rightFootStepEmitter.SetParameter("Terrain", 1.0f);
        }
    }

    public void playDeathSound()
    {
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/Death");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }

    public void playPerfectBlockSound()
    {
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Perfect_Block");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.enemyWeapon.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }

    public void playWarnSound()
    {
        enemySound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/Warn");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemySound, this.transform, this.GetComponent<Rigidbody>());
        enemySound.start();
        enemySound.release();
    }
}
