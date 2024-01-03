using System.Collections;
using System.Collections.Generic;
using TwitchIntegration;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TwitchCharger : MonoBehaviour
{
    [SerializeField]
    private GameObject _chatText;
    [SerializeField]
    private RectTransform _spawnPoint;

    private Slider _addingAMessage;

    private void Start()
    {
        _addingAMessage = GetComponent<Slider>();
        TwitchManager.OnTwitchMessageReceived += OnTwitchMessageReceived;
    }

    private void OnTwitchMessageReceived(TwitchUser user, string message)
    {
        var playerName = user.displayname;
        if (!string.IsNullOrEmpty(user.color))
            playerName = "<color=" + user.color + ">" + playerName + "</color>";

        _addingAMessage.value += 1;

        SpawnMessage($"{playerName}: {message}\n");
    }

    private void SpawnMessage(string text)
    {
        GameObject msg = Instantiate(_chatText);
        msg.GetComponent<RectTransform>().position = _spawnPoint.position;
        TextMeshProUGUI tmp = msg.GetComponent<TextMeshProUGUI>();

        tmp.text = text;
        msg.GetComponent<MovingToTarget>().SetTarget(this.GetComponent<RectTransform>());
    }

}
