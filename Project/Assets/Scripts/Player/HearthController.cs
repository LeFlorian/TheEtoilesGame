using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthController : MonoBehaviour
{

	public LifeController hps;
	public Sprite FullHearth;
	public Sprite EmptyHearth;

	public Image[] hearths;

    // Update is called once per frame
    void Update()
    {
    	if (hps == null) hps = GameObject.FindWithTag("Player").GetComponent<LifeController>();
        for (int i=0; i < hearths.Length; i++){
        	if(i <  hps.life){
        		hearths[i].sprite = FullHearth;
        	}
        	else{
        		hearths[i].sprite = EmptyHearth;
        	}		
        }
    }
}
