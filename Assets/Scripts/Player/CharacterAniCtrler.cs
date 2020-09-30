// CharacterAniLogic.cs

using UnityEngine;
using System.Collections.Generic;

// 角色动画逻辑
public class CharacterAniCtrler
{
    private const string STR_ACTION = "Action";
    private const string STR_SPEED = "Speed";
    private const string STR_DEATH = "death";
    private Animator m_animator;

    /// <summary>
    /// 准备播放的动作
    /// </summary>
    private Queue<int> m_animQueue = new Queue<int>();
    private AnimatorClipInfo[] mClips = null;

    /// <summary>
    /// 是否播放跑步动作
    /// </summary>
    public bool IsPlayRunAction = false;

    public void Init(Animator ani)
    {
        m_animator = ani;
    }

    public void LateUpdate()
    {
        if (m_animator == null)
        {
            return;
        }
        if (!m_animator.isInitialized || m_animator.IsInTransition(0))
        {
            return;
        }
        if (null == mClips)
            mClips = m_animator.GetCurrentAnimatorClipInfo(0);
        if (null == mClips || mClips.Length == 0)
            return;

        int actionID = m_animator.GetInteger(STR_ACTION);
        if (actionID > 0)
        {
            //将Action复位
            m_animator.SetInteger(STR_ACTION, 0);
        }
        //将剩余队列的动作重新拿出来播放
        PlayRemainAction();

        if (IsPlayRunAction)
        {
            IsPlayRunAction = false;
            PlayRun();
        }
    }

    /// <summary>
    /// 将剩余队列的动作重新拿出来播放
    /// </summary>
    void PlayRemainAction()
    {
        if (m_animQueue.Count > 0)
        {
            PlayAnimation(m_animQueue.Dequeue());
        }
    }

    public void PlayDieImmediately()
    {
        PlayAniImmediately(STR_DEATH);
    }

    public void PlayAniImmediately(string name)
    {
        m_animator.Play(name, 0, 0.95f);
    }

    /// <summary>
    /// 播放不同动作ID
    /// </summary>
    /// <param name="isJump"></param>
    /// 
    public void PlayAnimation(int actionID)
    {
        if (m_animator == null)
            return;
        if (m_animator != null && (!m_animator.isInitialized || m_animator.IsInTransition(0) && actionID != 20))
        {
            m_animQueue.Enqueue(actionID);
            return;
        }
        m_animator.SetInteger(STR_ACTION, actionID);
    }


    /// <summary>
    /// 播放站立状态
    /// </summary>
    public void PlayIdle()
    {
        if (null == m_animator || !m_animator.isInitialized)
            return;
        m_animator.SetInteger(STR_SPEED, (int)CharacterAniId.Idle);
    }

    public bool IsPlayingRunAni()
    {
        AnimatorStateInfo stateinfo = m_animator.GetCurrentAnimatorStateInfo(0);
        return stateinfo.IsName("run");

    }

    /// <summary>
    /// 播放跑步状态
    /// </summary>
    public void PlayRun()
    {
        if (null == m_animator || !m_animator.isInitialized)
            return;
        m_animator.SetInteger(STR_SPEED, (int)CharacterAniId.Run);
    }

    public void ClearAnimQueue()
    {
        if (m_animQueue.Count > 0)
        {
            m_animQueue.Clear();
        }
    }
}

public enum CharacterAniId
{
    #region 使用Speed参数
    Run = 1,
    Idle = 2,
    #endregion


    #region 使用Action参数
    Attack = 1000,

    Hit = 2000,
    Hit2 = 2001,

    Death = 3000,
    #endregion
}
