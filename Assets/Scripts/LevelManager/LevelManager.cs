using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    public float timeBetweenPieces = .3f;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;

    [Header("Animation")]
    public float ScaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;


    private void Awake()
    {
        //SpawnNextLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region
    private void CreateLevelPieces()
    {
        CleanSpawnedPieces();

        if (_currSetup != null)
        {
            _index++;
            if (_index >= levelPieceBasedSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        _currSetup = levelPieceBasedSetups[_index];

        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.LevelPiecesStart);
        }

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.LevelPieces);
        }

        for (int i = 0; i < _currSetup.piecesEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.LevelPiecesEnd);
        }

        ColorManager.instance.ChangeColorByType(_currSetup.ArtType);

        StartCoroutine(ScalePiecesByTime());
        

    }
    
    IEnumerator ScalePiecesByTime()
    {
        foreach( var p in _spawnedPieces )
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for(int i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, ScaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);

        }
        CoinsAnimatorManager.instance.StartAnimations();
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[(new System.Random()).Next(0, list.Count)];
        var SpawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            SpawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            SpawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach (var p in SpawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.instance.GetSetupByType(_currSetup.ArtType).gameObject);
        }

        _spawnedPieces.Add(SpawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
           Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCorroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.LevelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }

}