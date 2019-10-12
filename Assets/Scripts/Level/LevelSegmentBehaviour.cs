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
