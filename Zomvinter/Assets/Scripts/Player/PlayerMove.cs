using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Character
{
    public Transform RotatePoint;
    public float CharMoveSpeed = 5.0f;
    public float CharJumpForce = 200.0f;
    new Rigidbody rigidbody;
    public CharacterStat myStat;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Rotate(RotatePoint);
    }
    //대각선이동(애니메이션) , rotating lookAt()
    void Moving()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Input.GetAxis("Horizontal");
        pos.z = Input.GetAxis("Vertical");
        //pos.Normalize();
        myAnim.SetFloat("pos.x", pos.x);
        myAnim.SetFloat("pos.z", pos.z);
        //Debug.Log(pos.normalized);;
        this.transform.Translate(pos.normalized * CharMoveSpeed * Time.deltaTime);
    }
    //void Rotating()
    //{
    //    Vector3 mPosition = Input.mousePosition; //마우스좌표
    //    Vector3 oPosition = this.transform.position; //오브젝트좌표

    //    mPosition.y = oPosition.y - Camera.main.transform.position.y; 

    //    Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
    //    float dirz = target.z - oPosition.z;
    //    float dirx = target.x - oPosition.x;
    //    float rotateDegree = Mathf.Atan2(dirz, dirx) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0.0f, rotateDegree, 0.0f);
    //}
    void Rotate(Transform RotatePoint)
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //마우스 위치를 카메라레이를 이용해 카메라에서 스크린의 점을 통해 반환
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        //월드좌표로 하늘 방향에 크기가 1인 백터와 원점을 갖음
        float rayLength;
        if (GroupPlane.Raycast(cameraRay, out rayLength)) //레이가 평면과 교차했는지 파악
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength); //레이렝스거리에 위치값 반환
            /*transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
            //위에서 구한 pointTolook 위치값을 캐릭터가 보게함*/
            Vector3 dir = pointTolook - this.transform.position; //뱡향 구하기, 방향 벡터값 = 목표벡터 - 시작벡터
            dir.y = 0f;
            Quaternion rot = Quaternion.LookRotation(dir.normalized); //방향의 쿼터니언 값 구하기, 쿼너티언 값 = 쿼너티언 방향 값(방향 벡터)
            RotatePoint.transform.rotation = rot; //방향 돌리기
        }
    }

}
