using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestMap1 : Map
{
    public void Awake()
    {
        SetMapInfo("Stage_1", "Stage1", "테스트용", new Vector2(-11, -2), new Vector2(-9, -2), new Vector2(-7, -2), new Vector2(-5, -2));
    }

    // Start is called before the first frame update




    public virtual void Update()
    {
        base.Update();

    }
}
