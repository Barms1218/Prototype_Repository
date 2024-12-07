using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    SwapEvent swapEvent = new SwapEvent();
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        //swapEvent.AddListener(SwapWithPlayer);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwapWithPlayer()
    {
         transform.position = player.transform.position;
    }

}
