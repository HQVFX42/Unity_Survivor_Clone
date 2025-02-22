using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    public override bool Init()
    {
        if (base.Init())
        {
            return false;
        }

        ObjectType = Define.EObjectType.Monster;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
