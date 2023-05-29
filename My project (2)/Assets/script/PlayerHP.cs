using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    
    public Text hptext;
    public GameObject player;

    private float playerHealth;

    void Update()
    {
        playerHealth = player.GetComponent<HealthPoints>().HP;
        hptext.text = playerHealth.ToString();
    }
}
