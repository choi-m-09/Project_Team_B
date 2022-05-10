using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 몬스터 데이터 구조체 </summary>
[Serializable]
struct MonsterData
{
    public float AttRange; // 공격 범위
    public float AttDelay; // 공격 딜레이
    public float AttSpeed; // 공격 애니메이션 속도
    public float UnChaseTime; // 어그로 해제 간격
}

/// <summary> 캐릭터 데이터 구조체</summary>
[Serializable]
public struct CharacterStat
{
    public float HP; //체력 Health Point
    public float AP; //방어력 Armor Point
    public float DP; //공격력 Damage Point
    public float MoveSpeed;
    public float TurnSpeed;
    public float Hunger; // 허기
    public float Thirsty; // 갈증
}

/// <summary> 회전 연산 변수 구조체 </summary>
public struct ROTATEDATA
{
    public float Angle;
    public float Dir;
}

public class Character : MonoBehaviour
{
    /// <summary> Animator 컴포넌트 반환 </summary>
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if(_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    /// <summary> 회전 연산 함수 </summary>
    /// <param name="src">회전 시작 지점</param>
    /// <param name="des">회전 끝 지점</param>
    /// <param name="right">해당 오브젝트의 Vector.Right값</param>
    /// <param name="data">연산 후 리턴 값</param>
    public static void CalcAngle(Vector3 src, Vector3 des, Vector3 right, out ROTATEDATA data)
    {
        data = new ROTATEDATA();
        float Radian = Mathf.Acos(Vector3.Dot(src, des));
        //로테이션 값
        data.Angle = 180.0f * (Radian / Mathf.PI);
        //회전의 좌, 우방향 값
        data.Dir = 1.0f;
        if (Vector3.Dot(right, des) < 0.0f)
        {
            data.Dir = -1.0f;
        }
    }

}
