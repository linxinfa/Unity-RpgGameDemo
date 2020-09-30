using UnityEngine;
using System.Collections.Generic;


public class PlayerMgr
{
    public void Init()
    {
        EventDispatcher.instance.Regist(EventNameDef.JOINTED_ARM_MOVE, OnJointedArmMove);
        EventDispatcher.instance.Regist(EventNameDef.JOINTED_ARM_MOVE_END, OnJointedArmMoveEnd);
    }

    public PlayerUnit CreatePlayer(int playerId, Vector3 pos)
    {
        if(m_playerDic.ContainsKey(playerId))
        {
            return m_playerDic[playerId];
        }
        var rootGo = new GameObject("player_" + playerId);
        var modelPrefab = ResourceMgr.instance.LoadRes<GameObject>("hero");
        var modelGo = Object.Instantiate(modelPrefab);
        modelGo.transform.SetParent(rootGo.transform);
        rootGo.transform.position = pos;
        PlayerUnit unit = new PlayerUnit(playerId, rootGo, modelGo, 0.04f);

        m_playerDic[playerId] = unit;
        return unit;
    }

    public PlayerUnit GetMyPlayer()
    {
        return m_playerDic[myPlayerId];
    }

    public void Update()
    {
        foreach (var player in m_playerDic.Values)
        {
            player.Update();
        }
    }

    public void LateUpdate()
    {
        foreach (var player in m_playerDic.Values)
        {
            player.LateUpdate();
        }
    }

    private void OnJointedArmMove(params object[] args)
    {
        Vector2 dir = (Vector2)args[0];
        var playerUnit = GetMyPlayer();
        playerUnit.PlayRun();
        playerUnit.runDir = dir;
    }

    private void OnJointedArmMoveEnd(params object[] args)
    {
        var playerUnit = GetMyPlayer();
        playerUnit.PlayIdle();
    }



    private Dictionary<int, PlayerUnit> m_playerDic = new Dictionary<int, PlayerUnit>();
    public int myPlayerId;

    private static PlayerMgr s_instance;
    public static PlayerMgr instance
    {
        get
        {
            if (null == s_instance)
                s_instance = new PlayerMgr();
            return s_instance;
        }
    }

} 