using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

class MilestoneNotification : MonoBehaviour
{
    [Serializable]
    struct Message
    {
        public int RequiredCount;
        public string MessageText;
    }

    [SerializeField]
    private List<Message> MessageData;

    //[SerializeField] //dear god why can't Unity handle this by now?
    private Dictionary<int, string> Messages = new Dictionary<int, string>();

    [SerializeField]
    private List<string> RandomMessages = new List<string>();

    [SerializeField]
    private TMP_Text MessageBox;

    [SerializeField]
    private Animator MilestoneAnimator;

    [SerializeField]
    private string ShowAnimationTriggerName;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float MessageProbability = 0.5f;

    private void Start()
    {
        ShrimpCounter.CountChanged += OnCountChanged;

        //process our data into a dictionary so we can live like civilized people
        foreach (var Message in MessageData)
        {
            if (Messages.ContainsKey(Message.RequiredCount))
            {
                Debug.LogWarning($"Duplicate RequiredCount: {Message.RequiredCount} found while processing Messages", this);
                continue;
            }

            Messages.Add(Message.RequiredCount, Message.MessageText);
        }
    }

    private void OnDestroy()
    {
        ShrimpCounter.CountChanged -= OnCountChanged;
    }

    public void OnCountChanged(int Count)
    {
        // if we have a curated message, show that
        if (Messages.TryGetValue(Count, out string MessageText))
        {
            ShowMessage(MessageText);
            return;
        }

        if (RandomMessages.Count == 0)
            return;

        // otherwise, roll try to show a randomized message
        var Roll = UnityEngine.Random.Range(0.0f, 1.0f);
        if (Roll <= MessageProbability)
        {
            int Index = UnityEngine.Random.Range(0, RandomMessages.Count);
            ShowMessage(RandomMessages[Index]);
        }
    }

    private void ShowMessage(string Message)
    {
        MessageBox?.SetText(Message);
        MilestoneAnimator?.SetTrigger(ShowAnimationTriggerName);
    }
}
