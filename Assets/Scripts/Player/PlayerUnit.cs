using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit
{
    public PlayerUnit(int playerId, GameObject rootGo, GameObject modelGo, float moveSpeed)
    {
        m_playerId = playerId;
        m_rootGo = rootGo;
        m_rootTrans = rootGo.transform;

        m_modelGo = modelGo;
        m_modelTrans = modelGo.transform;
        m_moveSpeed = moveSpeed;

        //动画控制器
        var animator = modelGo.GetComponent<Animator>();
        m_aniCtrler = new CharacterAniCtrler();
        m_aniCtrler.Init(animator);

        //寻路控制器
        m_navAgent = rootGo.AddComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 播放跑动作
    /// </summary>
    public void PlayRun()
    {
        m_aniCtrler.PlayRun();
        m_isRunning = true;
    }

    /// <summary>
    /// 播放站立动作
    /// </summary>
    public void PlayIdle()
    {
        m_aniCtrler.PlayIdle();
        runDir = Vector3.zero;
        m_isRunning = false;
    }

    public void PlayAction(int actioinId)
    {
        m_aniCtrler.PlayAnimation(actioinId);
    }

    public void Update()
    {
        if (m_isRunning && m_aniCtrler.IsPlayingRunAni())
            Move();
    }

    public void LateUpdate()
    {
        m_aniCtrler.LateUpdate();
    }

    private void Move()
    {
        var dir = new Vector3(runDir.x, 0, runDir.y).normalized;
        m_rootTrans.position += dir * m_moveSpeed;
        m_modelTrans.forward = Vector3.Lerp(m_modelTrans.forward, dir, 50 * Time.deltaTime);
    }

    public Transform rootTrans { get { return m_rootTrans; } }

    private int m_playerId;
    private float m_moveSpeed;

    private GameObject m_rootGo;
    private Transform m_rootTrans;

    private GameObject m_modelGo;
    private Transform m_modelTrans;

    private CharacterAniCtrler m_aniCtrler;
    private NavMeshAgent m_navAgent;
    private bool m_isRunning;
    private bool m_attacking;

    /// <summary>
    /// 跑的方向
    /// </summary>
    public Vector2 runDir { get; set; }
}
