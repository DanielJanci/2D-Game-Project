using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

[Serializable]
public class RecordScoreData
{
    public Dictionary<string, PlayerLeaderboardEntry> recordScores;
    public Dictionary<string, bool> isDataLoaded;
    
    public RecordScoreData()
    {
        isDataLoaded = new Dictionary<string, bool>()
        {
            {"ScoreLevel01", false},
            {"ScoreLevel02", false},
            {"ScoreLevel03", false}
        };
        
        recordScores = new Dictionary<string, PlayerLeaderboardEntry>()
        {
            {"ScoreLevel01", new PlayerLeaderboardEntry()},
            {"ScoreLevel02", new PlayerLeaderboardEntry()},
            {"ScoreLevel03", new PlayerLeaderboardEntry()}
        };
    }
}
