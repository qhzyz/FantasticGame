using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    const int MAX_NUM=10;
    public int playerNum;
    public GameObject[] players;
	void Start () {
        GameObject temp = (GameObject)Resources.Load("Player");
        players = new GameObject[MAX_NUM];
        for (int i = 0; i < playerNum; i++) {
            players[i] = Instantiate(temp, transform);
            players[i].GetComponent<Player>().InitPlayer();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
