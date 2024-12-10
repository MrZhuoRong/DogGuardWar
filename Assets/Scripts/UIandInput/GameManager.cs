using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource BGM;

    public List<GameObject> BagItems = new List<GameObject>();

    public List<GameObject> Wuqi=new List<GameObject>();

    private void Start()
    {
        BGM=GetComponent<AudioSource>();
    }
    public void SetAudio()
    {
        if(BGM.isPlaying)
        {
            BGM.Pause();
        }
        else
        {
            BGM.Play();
        }
    }

    private void Update()
    {
        if(AttackPlayer.KillNumber%10==0)
        {
            int num=(AttackPlayer.KillNumber/10)+1;
            for(int i=0; i<num; i++)
            {
                BagItems[i].SetActive(true);
            }
            
        }
    }

    public void SetWuqi(GameObject gameObject)
    {
        foreach(GameObject wuqi in Wuqi)
        {
            wuqi.SetActive(false);
        }

        gameObject.SetActive(true);
    }
}
