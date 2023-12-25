using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchIntegration;
using TMPro;

public class TwitchManager : TwitchMonoBehaviour
{
    public TextMeshProUGUI checkMessage;

    [TwitchCommand(name: "message", defaultCooldown: 1, aliases: new string[] {"msg", "m"})]
    public void SendMessage(string msg)
    {
        checkMessage.text = msg;
    }
}
