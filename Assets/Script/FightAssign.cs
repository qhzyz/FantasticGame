using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAssign : MonoBehaviour {
    public const float FLOAT_PROPORTION = 1.0f;
    PlayerManager playerManager;

    MapData mapData;
    MakeMap makeMap;
    int currentTurn;
    BasicSkill[] basicSkills;
	// Use this for initialization
	void Start () {
        mapData = transform.Find("map").GetComponent<MapData>();
        makeMap = transform.Find("map").GetComponent<MakeMap>();
        playerManager = transform.Find("PlayerManager").GetComponent<PlayerManager>();
        InitHero();
    }
    public bool InitHero() {
        int maxMark = mapData.length * mapData.width;//共有几个mapunit数据
        int k = 0;
        for (int i=0;i< playerManager.playerNum; i++) {
            for (int j=0;j<playerManager.players[i].GetComponent<Player>().heroNum;) {
                if (mapData.DataArr[k].isStart) {
                    Transform temp = makeMap.mapSurface[k].transform;
                    playerManager.players[i].GetComponent<Player>().hero[j].transform.localPosition = new Vector3(temp.localPosition.x, 
                        temp.localPosition.y+ FLOAT_PROPORTION*MapData.UNIT_SIZE*MapData.Y_SCALE, temp.localPosition.z);
                    playerManager.players[i].GetComponent<Player>().hero[j].posX = mapData.DataArr[k].x;
                    playerManager.players[i].GetComponent<Player>().hero[j].posY = mapData.DataArr[k].y;
                    playerManager.players[i].GetComponent<Player>().hero[j].pos = k;
                    j++;
                }
                k++;
                //如果所有地图块遍历完成，所有英雄未初始完返回错误
                if(k==maxMark &&(j!= playerManager.players[i].GetComponent<Player>().heroNum || i!= playerManager.playerNum - 1)) {
                    return false;
                }
            }
        }
        return true;
    }
    void Update () {
		
	}
}
