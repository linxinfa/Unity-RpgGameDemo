//PlayerCameraFollow.cs
using UnityEngine;

/// <summary>
/// 第三人称视角摄像机跟随
/// Author: linxinfa
/// </summary>
public class PlayerCameraFollow : MonoBehaviour
{
    public Transform targetPlayer;

    /// <summary>
    /// 原坐标
    /// </summary>
    public Vector3 originalPos = new Vector3(0, 4.27f, -7);
    /// <summary>
    /// 原角度
    /// </summary>
    public Vector3 originalRot = new Vector3(17.01f, 0, 0);

    /// <summary>
    /// 距离角色的距离
    /// </summary>
    public Vector3 dis = new Vector3(0, 4.81f, -7.59f);

    /// <summary>
    /// 平滑度
    /// </summary>
    public float smooth = 5f;

    private Transform m_cameraTransform;
    private float m_deltaTime;

    void Awake()
    {
        m_cameraTransform = transform;
    }

    void Update()
    {
        m_deltaTime = Time.deltaTime;
        if (null == targetPlayer)
        {
            m_cameraTransform.position = Vector3.Lerp(m_cameraTransform.position, originalPos, m_deltaTime * smooth);
            m_cameraTransform.eulerAngles = Vector3.Lerp(m_cameraTransform.eulerAngles, originalRot, m_deltaTime * smooth);
        }
        else
        {

            // 插值设置坐标，有个平滑跟随效果
            m_cameraTransform.position = Vector3.Slerp(m_cameraTransform.position, dis + targetPlayer.position, smooth * m_deltaTime);

            // 设置摄像机角度，对准跟随目标
            m_cameraTransform.LookAt(m_cameraTransform.position, Vector3.up);
        }
    }
}
