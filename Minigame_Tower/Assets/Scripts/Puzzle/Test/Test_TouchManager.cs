using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TouchManager : TouchManager
{

    public override void ChildChange()
    {

        if (touchedObject == null || targetObject == null || touchedObject == targetObject)
        {
            return;
        }
        if (touchedObject.transform.childCount == 1 ||
            targetObject.transform.childCount == 1)
        {
            return;
        }

        touchedObject.transform.GetChild(1).transform.parent = transform;
        targetObject.transform.GetChild(1).transform.parent = touchedObject.transform;
        transform.GetChild(0).transform.parent = targetObject.transform;
        targetObject.transform.GetChild(1).localPosition= Vector3.zero;
        touchedObject.transform.GetChild(1).localPosition = Vector3.zero;

        blockController.ThreeMatchAction(touchedIndexX, touchedIndexY);
            blockController.ThreeMatchAction(targetIndexX, targetIndexY);
    
    }

    
}
