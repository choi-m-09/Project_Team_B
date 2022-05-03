using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Player : PlayerController, BattleSystem
{
    /*-----------------------------------------------------------------------------------------------*/
    //지역 변수
    public CharacterStat myStat;
    public LayerMask BulletLayer;//총알 도착지
    private Vector3 dir; // 총알 각도 
    //인벤토리
    public List<GameObject> myItems = new List<GameObject>();
    public GameObject myInventory;
    private bool ActiveInv = false;

    //마우스 로테이트
    public Transform RotatePoint;
    //이동 벡터
    Vector3 pos = Vector3.zero;
    //총알프리팹, 총알발사위치, 총알각도
    public Transform bullet; public Transform bulletStart; public Transform bulletRotate;
    //총 획득시 생성하기 위한 boolcheck용 
    public bool GunCheck1 = false; public bool GunCheck2 = false;
    //총 획득시 생성되는 위치의 부모 설정
    public Transform UnArmed1; public Transform UnArmed2; //플레이어 등 부분에 스폰되는 위치
    public Transform UnArmedGun1; public Transform UnArmedGun2; // 플레이어가 획득한 무기의 prefab을 사용해야함 

    /*-----------------------------------------------------------------------------------------------*/
    //Unity
    void Start()
    {
        ChangeState(STATE.CREATE);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        
    }
    private void LateUpdate()
    {
        //base.BulletRotate(bulletRotate);
    }
    /*-----------------------------------------------------------------------------------------------*/
    //유한 상태 기계
    public enum STATE
    {
        NONE, CREATE, ALIVE, BATTLE, DEAD
    }

    public STATE myState = STATE.NONE;

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.NONE:
                break;
            case STATE.CREATE:
                myStat.MoveSpeed = 3.0f;
                ChangeState(STATE.ALIVE);
                break;
            case STATE.ALIVE:
                break;
            case STATE.BATTLE:
                break;
            case STATE.DEAD:
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NONE:
                break;
            case STATE.CREATE:
                break;
            case STATE.ALIVE:
                if(!myAnim.GetBool("IsAiming")) Move();

                //Rotation();
                if (Input.GetMouseButton(0) && myAnim.GetBool("IsAiming"))//&& GunCheck
                {
                    Fire();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1) && !myAnim.GetBool("IsGun"))//&&GunCheck
                {
                    myAnim.SetBool("IsGun", true);
                    myAnim.SetTrigger("GetGun");
                }
                if (Input.GetKeyDown(KeyCode.X))// && GunCheck
                {
                    myAnim.SetTrigger("PutGun");
                    myAnim.SetBool("IsGun", false);
                    myAnim.SetBool("IsAiming", false);
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    myAnim.SetTrigger("Melee");
                }
                if (Input.GetKeyDown(KeyCode.Tab) && myState != STATE.DEAD)
                {
                    ActiveInv = !ActiveInv;
                    myInventory.SetActive(ActiveInv);
                }
                if (myAnim.GetBool("IsGun") && Input.GetMouseButtonDown(1))
                {
                    StartAiming(); //로테이션 값 저장
                    myAnim.SetBool("IsAiming", true);
                    //BulletRotCtrl();
                }
                if (myAnim.GetBool("IsGun") && Input.GetMouseButton(1))
                {
                    Rotate(RotatePoint);
                    BulletRotate(bulletRotate);
                }
                if (myAnim.GetBool("IsGun") && Input.GetMouseButtonUp(1))
                {
                    myAnim.SetBool("IsAiming", false);
                    StopAiming(); Debug.Log("StopAiming");

                }
                //총 획득시 Guncheck true 만들기
                //if (GunCheck)
                break;
            case STATE.BATTLE:
                break;
            case STATE.DEAD:
                break;
        }
    }

    void Move()
    {
        pos.x = Input.GetAxis("Horizontal");
        pos.z = Input.GetAxis("Vertical");
        base.Moving(pos, myStat.MoveSpeed, RotatePoint);
    }
    void Rotation()
    {
        //base.Rotate(RotatePoint);
    }


    /*-----------------------------------------------------------------------------------------------*/
    // 배틀 시스템
    public void TakeHit(float damage, RaycastHit hit)
    {

    }
    void OnAttack()
    {
        //base.BulletRotate(bulletRotate);

    }
    public void OnDamage(float Damage)
    {
        if (myState == STATE.DEAD) return;
        myStat.HP -= Damage;
        if (myStat.HP <= 0) ChangeState(STATE.DEAD);
        else
        {

        }
    }
    public void OnCritDamage(float CritDamage)
    {

    }
    public bool IsLive()
    {
        return true;
    }
    void Fire()
    {
        Instantiate(bullet, bulletStart.position, bulletRotate.rotation);
    }
    void BulletRotCtrl()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit, 9999.0f))
        //{
        //    Vector3 pos = bulletStart.position - hit.point;
        //}
        //Quaternion rotation = Quaternion.LookRotation(pos);
        //transform.rotation = rotation;  
    }
    void GetWeapon()//주무기 획득시 등에 스폰
    {
        if (GunCheck1 == true)
            Instantiate(UnArmedGun1, UnArmed1);
        if (GunCheck2 == true)
            Instantiate(UnArmedGun2, UnArmed2);
    }
    void PutWeapon()//주무기 버릴때 없앰
    {
        if (GunCheck1 == false)
            if (UnArmedGun1 != null) Destroy(UnArmedGun1);
        if (GunCheck2 == false)
            if (UnArmedGun2 != null) Destroy(UnArmedGun2);
    }
}
