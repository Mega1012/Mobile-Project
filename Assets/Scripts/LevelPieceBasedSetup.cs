using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBasedSetup : ScriptableObject
{
    public ArtManager.ArtType ArtType;

    [Header("Pieces")]
    public List<LevelPieceBase> LevelPiecesStart;
    public List<LevelPieceBase> LevelPieces;
    public List<LevelPieceBase> LevelPiecesEnd;

    public int piecesStartNumber = 3;
    public int piecesNumber = 5;
    public int piecesEndNumber = 1;
}
