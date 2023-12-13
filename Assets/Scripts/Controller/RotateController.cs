using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateController : ParentObstacleController
{
    public enum ObType
    {
        Swing
    }
    [SerializeField] private ObType obType;

    public override IEnumerator Activate()
    {
        switch (obType)
        {
            case ObType.Swing:
                break;

        }
        yield return base.Activate(); // 부모 클래스의 Activate 메서드 실행 
                                        //사실 ismoving과 별개로 움직이기 때문에 이걸 굳이 부모 activate를 실행하지 않아도 되지만 후에 test할때를 위해 그냥 실행하겠음. 
    }

    private void Awake()
    {
        base.Awake();
    }
}
