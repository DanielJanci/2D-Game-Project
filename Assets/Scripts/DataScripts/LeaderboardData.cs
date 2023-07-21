using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayFab.ClientModels;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class LeaderboardData
{
    public Dictionary<string, List<PlayerLeaderboardEntry>> leaderboard;
    public Dictionary<string, bool> isDataLoaded;

    public LeaderboardData()
    {
        isDataLoaded = new Dictionary<string, bool>()
        {
            {"ScoreLevel01", false},
            {"ScoreLevel02", false},
            {"ScoreLevel03", false}
        };
            
        leaderboard = new Dictionary<string, List<PlayerLeaderboardEntry>>()
        {
            {"ScoreLevel01", new List<PlayerLeaderboardEntry>()},
            {"ScoreLevel02", new List<PlayerLeaderboardEntry>()},
            {"ScoreLevel03", new List<PlayerLeaderboardEntry>()}
        };
    }
}
