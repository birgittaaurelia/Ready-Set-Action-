using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CurtainEvent
{
    public float time;
    public CurtainAction action;
}

public enum CurtainAction
{
    Open,
    Close
}

public class CurtainController : MonoBehaviour
{
    public static CurtainController Instance;

    [Header("Curtain Objects")]
    public Transform leftCurtain;
    public Transform rightCurtain;

    [Header("Positions")]
    public float openPositionX = 10f;
    public float closedPositionX = 0f;

    [Header("Timing")]
    public float slideSpeed = 1f;

    [Header("Timeline")]
    public List<CurtainEvent> curtainTimeline = new List<CurtainEvent>();

    private float stageTimer = 0f;
    private int currentEventIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        curtainTimeline.Sort((a, b) => a.time.CompareTo(b.time));

        SetClosed();
    }

    void Update()
    {
        stageTimer += Time.deltaTime;

        if (currentEventIndex < curtainTimeline.Count &&
            stageTimer >= curtainTimeline[currentEventIndex].time)
        {
            TriggerEvent(curtainTimeline[currentEventIndex]);
            currentEventIndex++;
        }
    }

    void TriggerEvent(CurtainEvent curtainEvent)
    {
        if (curtainEvent.action == CurtainAction.Open)
            TriggerOpen();
        else
            TriggerClose();
    }

    void SetClosed()
    {
        leftCurtain.position = new Vector3(-closedPositionX, leftCurtain.position.y, leftCurtain.position.z);
        rightCurtain.position = new Vector3(closedPositionX, rightCurtain.position.y, rightCurtain.position.z);
    }

    IEnumerator SlideCurtains(Vector3 leftTarget, Vector3 rightTarget)
    {
        Vector3 leftStart = leftCurtain.position;
        Vector3 rightStart = rightCurtain.position;

        float elapsed = 0f;
        float duration = 1f / slideSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);

            leftCurtain.position = Vector3.Lerp(leftStart, leftTarget, t);
            rightCurtain.position = Vector3.Lerp(rightStart, rightTarget, t);

            yield return null;
        }

        leftCurtain.position = leftTarget;
        rightCurtain.position = rightTarget;
    }

    public void TriggerOpen()
    {
        StopAllCoroutines();
        StartCoroutine(SlideCurtains(
            new Vector3(-openPositionX, leftCurtain.position.y, leftCurtain.position.z),
            new Vector3(openPositionX, rightCurtain.position.y, rightCurtain.position.z)
        ));
    }

    public void TriggerClose()
    {
        StopAllCoroutines();
        StartCoroutine(SlideCurtains(
            new Vector3(-closedPositionX, leftCurtain.position.y, leftCurtain.position.z),
            new Vector3(closedPositionX, rightCurtain.position.y, rightCurtain.position.z)
        ));
    }

    
}