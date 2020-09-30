using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button createPlayerBtn;
    public Toggle cameraFollowTgl;
    public Button attackBtn;
    public Button hitBtn;
    public Button hit2Btn;
    public Button deathBtn;

    private PlayerCameraFollow m_cameraFollowBhv;

    private int m_playerCnt = 1;

    void Awake()
    {
        GlobalObjs.Init();
        PlayerMgr.instance.Init();
    }

    void Start()
    {
        PlayerMgr.instance.myPlayerId = 1;
        PlayerMgr.instance.CreatePlayer(PlayerMgr.instance.myPlayerId, Vector3.zero);

        //创建角色
        createPlayerBtn.onClick.AddListener(() =>
        {
            ++m_playerCnt;
            PlayerMgr.instance.CreatePlayer(m_playerCnt, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
        });


        //摄像机跟随
        m_cameraFollowBhv = GlobalObjs.mainCam3D.gameObject.AddComponent<PlayerCameraFollow>();
        var myPlayerUnit = PlayerMgr.instance.GetMyPlayer();
        m_cameraFollowBhv.targetPlayer = myPlayerUnit.rootTrans;

        cameraFollowTgl.onValueChanged.AddListener((v) =>
        {
            if (v)
            {
                var playerUnit = PlayerMgr.instance.GetMyPlayer();
                m_cameraFollowBhv.targetPlayer = playerUnit.rootTrans;
            }
            else
            {
                m_cameraFollowBhv.targetPlayer = null;
            }
        });

        //注：摇杆按钮见脚本JointedArm.cs

        //攻击按钮
        attackBtn.onClick.AddListener(() =>
        {
            var playerUnit = PlayerMgr.instance.GetMyPlayer();
            playerUnit.PlayAction((int)CharacterAniId.Attack);
        });

        //受击按钮
        hitBtn.onClick.AddListener(() =>
        {
            var playerUnit = PlayerMgr.instance.GetMyPlayer();
            playerUnit.PlayAction((int)CharacterAniId.Hit);
        });

        //受击按钮2
        hit2Btn.onClick.AddListener(() =>
        {
            var playerUnit = PlayerMgr.instance.GetMyPlayer();
            playerUnit.PlayAction((int)CharacterAniId.Hit2);
        });

        //阵亡按钮
        deathBtn.onClick.AddListener(() =>
        {
            var playerUnit = PlayerMgr.instance.GetMyPlayer();
            playerUnit.PlayAction((int)CharacterAniId.Death);
        });
    }


    void Update()
    {
        PlayerMgr.instance.Update();
    }

    void LateUpdate()
    {
        PlayerMgr.instance.LateUpdate();
    }
}
