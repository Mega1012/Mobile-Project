using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataformer.Core.Singleton;
using TMPro;

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
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckFinishLine)
        {
            EndGame();
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
        //uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
    #endregion
}
