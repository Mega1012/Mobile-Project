using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPieceBasedSetup : ScriptableObject
{
    [Header("Pieces")]
    public List<LevelPieceBase> LevelPiecesStart;
    public List<LevelPieceBase> LevelPieces;
    public List<LevelPieceBase> LevelPiecesEnd;

    public int piecesStartNumber = 3;
    public int piecesNumber = 5;
    public int piecesEndNumber = 1;
}
