using System;
using UnityEngine;

public class LevelSegmentBehaviour : MonoBehaviour
{
    [SerializeField]
    protected Transform startPoint;

    [SerializeField]
    protected Transform startTangent;

    [SerializeField]
    protected Transform endPoint;

    [SerializeField]
    protected Transform endTangent;

    public LevelSegmentBehaviour previousSegment;

    public Vector3 EvaluateSegment(float t)
    {
            return GetPoint(
            GetStartPoint().position,
            GetStartTangent().position,
            GetEndTangent().position,
            GetEndPoint().position,
            t);
    }

    //https://catlikecoding.com/unity/tutorials/curves-and-splines/
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    [System.Serializable]
    public enum SegmentType
    {
        //Uses startPoint/endPoint as start/end
        start,
        //Uses previousSegment as start/end
        inner
    }

    [SerializeField][HideInInspector]
    protected SegmentType segmentType = SegmentType.inner;

    public SegmentType GetSegmentType()
    {
        return segmentType;
    }

    public Transform GetStartPoint()
    {
        Transform point = null;
        switch (segmentType)
        {
            case SegmentType.start:
                point = startPoint;
                break;
            case SegmentType.inner:
                point = previousSegment.endPoint;
                break;
            default:
                break;
        }
        return point;
    }

    public Transform GetStartTangent()
    {
        Transform point = point = startTangent;
        return point;
    }

    public Transform GetEndPoint()
    {
        Transform point = endPoint;
        return point;
    }

    public Transform GetEndTangent()
    {
        Transform point = endTangent;
        return point;
    }

    /// <summary>
    /// Create a Segment with its helper transform taking into account the type of the segment
    /// </summary>
    /// <param name="segmentType"></param>
    /// <returns></returns>
    public static LevelSegmentBehaviour CreateSegment(SegmentType segmentType)
    {
        GameObject segmentGO = new GameObject("Segment");
        segmentGO.SetActive(false);

        LevelSegmentBehaviour segment = segmentGO.AddComponent<LevelSegmentBehaviour>();
        segment.segmentType = segmentType;
        segment.endPoint = new GameObject("EndPoint").transform;
        segment.endPoint.SetParent(segment.transform);
        segment.endPoint.localPosition = Vector3.forward * 2;
        segment.endPoint.localRotation = Quaternion.identity;

        segment.endTangent = new GameObject("EndTangent").transform;
        segment.endTangent.SetParent(segment.transform);
        segment.endTangent.position = segment.endPoint.position;
        segment.endTangent.rotation = segment.endPoint.rotation;

        if (segmentType == SegmentType.start)
        {
            segment.startPoint = new GameObject("StartPoint").transform;
            segment.startPoint.SetParent(segment.transform);
            segment.startPoint.localPosition = Vector3.zero;
            segment.startPoint.localRotation = Quaternion.identity;
        }

        segment.startTangent = new GameObject("StartTangent").transform;
        segment.startTangent.SetParent(segment.transform);
        segment.startTangent.localPosition = Vector3.zero;
        segment.startTangent.localRotation = Quaternion.identity;

        segmentGO.SetActive(true);
        return segment;
    }
}
