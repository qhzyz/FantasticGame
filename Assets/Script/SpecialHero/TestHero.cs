using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHero : BasicHero {
    // Use this for initialization
    void Start () {
        health = 10;
        proActPoint = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    override public void UseSkill() {
    }
    override public void MoveTo(Vector3 pos) {
        transform.position = new Vector3(pos.x,
                       pos.y + FightAssign.FLOAT_PROPORTION * MapData.UNIT_SIZE * MapData.Y_SCALE, pos.z);
    }
}
