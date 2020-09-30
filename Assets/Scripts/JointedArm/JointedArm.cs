using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JointedArm : ScrollRect, IPointerDownHandler
{
    protected float mRadius = 0f;

    private Transform m_trans;
    private Transform m_bgTrans;
    private Vector3 m_originalPos;

    protected override void Awake()
    {
        base.Awake();
        m_trans = transform;
        m_bgTrans = m_trans.Find("bg");
        m_originalPos = m_trans.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_trans.localPosition = m_originalPos;
            this.content.localPosition = Vector3.zero;
        }
    }

    protected override void Start()
    {
        base.Start();
        //计算摇杆块的半径
        mRadius = (m_bgTrans as RectTransform).sizeDelta.x * 0.5f;
    }



    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnDrag(eventData);
        var contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > mRadius)
        {
            contentPostion = contentPostion.normalized * mRadius;
            SetContentAnchoredPosition(contentPostion);
        }
        //Debug.Log(contentPostion);
        EventDispatcher.instance.DispatchEvent(EventNameDef.JOINTED_ARM_MOVE, contentPostion);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        //Debug.Log("OnEndDrag");
        EventDispatcher.instance.DispatchEvent(EventNameDef.JOINTED_ARM_MOVE_END);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_trans.position = GlobalObjs.mainCam2D.ScreenToWorldPoint(eventData.position);
        m_trans.localPosition = new Vector3(m_trans.localPosition.x, m_trans.localPosition.y, 0);
    }
}
