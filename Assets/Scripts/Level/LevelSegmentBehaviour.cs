using UnityEngine;

public class LevelSegmentBehaviour : MonoBehaviour
{
    public Transform startPoint;
    public Transform startTangent;

    public Transform endPoint;
    public Transform endTangent;

    public LevelSegmentBehaviour previousSegment;

    [System.Serializable]
    public enum SegmentType
    {
        //Uses startPoint/endPoint as start/end
        start,
        //Uses previousSegment as start/end
        inner
    }

    protected SegmentType segmentType = SegmentType.inner;

    public SegmentType GetSegmentType()
    {
        return segmentType;
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

        segment.endPoint = new GameObject("EndPoint").transform;
        segment.endPoint.SetParent(segment.transform);
        segment.endPoint.localPosition = Vector3.forward * 2;
        segment.endPoint.localRotation = Quaternion.identity;

        segment.endTangent = new GameObject("EndTanget").transform;
        segment.endTangent.SetParent(segment.endPoint);
        segment.endTangent.localPosition = Vector3.zero;
        segment.endTangent.localRotation = Quaternion.identity;

        if (segmentType == SegmentType.start)
        {
            segment.startPoint = new GameObject("StartPoint").transform;
            segment.startPoint.SetParent(segment.transform);
            segment.startPoint.localPosition = Vector3.zero;
            segment.startPoint.localRotation = Quaternion.identity;

            segment.startTangent = new GameObject("StartTangent").transform;
            segment.startTangent.SetParent(segment.startPoint);
            segment.startTangent.localPosition = Vector3.zero;
            segment.startTangent.localRotation = Quaternion.identity;
        }

        segmentGO.SetActive(true);
        return segment;
    }
}
