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
    private RectTransform[] _spawnPoints;

    [SerializeField]
    private float _chargeTime;
    private float _timer;


    public Slider _addingAMessage;

    private void Start()
    {
        _addingAMessage = GetComponent<Slider>();
        TwitchManager.OnTwitchMessageReceived += OnTwitchMessageReceived;

        StartTimer();
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
        GameObject msg = Instantiate(_chatText,transform.parent);

        Vector2 randomPos = new Vector2(Random.Range(_spawnPoints[0].position.x, _spawnPoints[1].position.x), Random.Range(_spawnPoints[0].position.y, _spawnPoints[1].position.y));

        msg.GetComponent<RectTransform>().position = randomPos;
        TextMeshProUGUI tmp = msg.GetComponent<TextMeshProUGUI>();

        tmp.text = text;
        msg.GetComponent<MovingToTarget>().SetTarget(gameObject);
    }

    private void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (_timer < _chargeTime)
        {
            _timer += Time.deltaTime;

            float timerRatio = _timer / _chargeTime;
            float potentialSliderValue = timerRatio * _addingAMessage.maxValue;
            if (potentialSliderValue > _addingAMessage.value) 
            {
                _addingAMessage.value = potentialSliderValue;
            
            }
           

            yield return new WaitForEndOfFrame();
        }
    }

}
