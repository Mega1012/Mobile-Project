using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataformer.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;
    
    public float speed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckFinishLine = "FinishLine";

    public GameObject endScreen;

    public GameObject startScreen;

    public bool invencible = false;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;

        var _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;
        
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            if(!invencible) EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckFinishLine)
        {
            if (!invencible) EndGame();
        }
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
        startScreen.SetActive(false);
    }

    #region POWER UPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration). SetEase(ease);//.OnComplete(ResetHeight);a
        Invoke(nameof(ResetHeight), duration);
    }
    
    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }
    #endregion
}
