using System;
using System.Collections.Generic;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;

public class LeaderboardRepo
{
    public static LeaderboardRepo Instance { get; private set; }

    private static string _userIDKey = "userID";

    public static void CreateInstance()
    {
        Instance = new LeaderboardRepo();
    }

    public string UserID { get; private set; }
    public string UserName { get; private set; }
    public float Score { get; private set; }
    public List<LeaderboardEntry> Leaderboard { get; private set; } = new List<LeaderboardEntry>();
    public bool UserCreated => !string.IsNullOrEmpty(UserID);

    public event Action<List<LeaderboardEntry>> OnLeaderboardUpdated;

    private DatabaseReference _database;

    private LeaderboardRepo()
    {
        UserID = PlayerPrefs.GetString(_userIDKey, null);

        _database = FirebaseDatabase.DefaultInstance.RootReference.Child("leaderboard");

        _database.ValueChanged += OnValueChanged;
    }

    public void CreateUser(string name)
    {
        UserID = _database.Push().Key;
        UserName = name;
        PlayerPrefs.SetString(_userIDKey, UserID);
        PlayerPrefs.Save();
        UpdateScore(0f);
    }

    public void GetLeaderboard()
    {
        _database.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                NotifyLeaderboardChanged(task.Result);
            }
        });
    }

    public void UpdateScore(float score)
    {
        if (!UserCreated)
        {
            return;
        }

        var entry = new LeaderboardEntry(score, (long)TimeUtil.UtcNowMillis(), UserName);

        _database.Child(UserID).SetValueAsync(entry.ToDictionary());
    }

    private void OnValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        NotifyLeaderboardChanged(args.Snapshot);
    }

    private void NotifyLeaderboardChanged(DataSnapshot dataSnapshot)
    {
        if (dataSnapshot == null)
        {
            return;
        }

        var json = dataSnapshot.GetRawJsonValue();
        if (string.IsNullOrEmpty(json))
        {
            return;
        }

        var dict = JsonConvert.DeserializeObject<Dictionary<string, LeaderboardEntry>>(json);
        Leaderboard.Clear();
        foreach (var kv in dict)
        {
            if (kv.Key == UserID)
            {
                UserName = kv.Value.Name;
                Score = kv.Value.Score;
            }

            Leaderboard.Add(kv.Value);
        }

        Leaderboard.Sort((a, b) =>
        {
            var result = b.Score.CompareTo(a.Score);
            if (result == 0)
            {
                return a.TimeStamp.CompareTo(b.TimeStamp);
            }
            return result;
        });

        OnLeaderboardUpdated?.Invoke(Leaderboard);
    }
}
