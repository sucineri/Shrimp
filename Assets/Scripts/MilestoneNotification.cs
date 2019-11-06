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
    private TMP_Text MessageBox;

    [SerializeField]
    private Animator MilestoneAnimator;

    [SerializeField]
    private string ShowAnimationTriggerName;

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
        if (Messages.TryGetValue(Count, out string MessageText))
        {
            ShowMessage(MessageText);
        }
    }

    private void ShowMessage(string Message)
    {
        if (MessageBox == null)
            return;

        MessageBox.SetText(Message);

        if (MilestoneAnimator != null)
        {
            MilestoneAnimator.SetTrigger(ShowAnimationTriggerName);
        }
    }
}
