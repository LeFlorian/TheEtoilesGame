using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.UI;

public class Music_LifeController : MonoBehaviour
{
	[SerializeField]	
    private int maxLife;
    public int life;
    public Slider slider;


    public void Awake() {
    	slider.maxValue = maxLife;
    	slider.value = maxLife;
    }

    public int InflictDamage(int damage)
    {
        life -= damage;
        life = Mathf.Clamp(life, 0, maxLife);

        if (life <= 0)
        {
            KillPlayer();
        }

        slider.value = life;

        return life;
    }

    public void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby");
    }
}