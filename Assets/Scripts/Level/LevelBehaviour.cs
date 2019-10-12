using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public LevelSegmentBehaviour[] segments = new LevelSegmentBehaviour[] { };

    /// <summary>
    /// How much each segment represent in the T of the Composite bezier formed by 'segments'
    /// </summary>
    protected float sizeSegmentT = 0.1f;

    private void Awake()
    {
        sizeSegmentT = 1.0f / segments.Length;
    }
    public Vector3 EvaluateSegments(float t)
    {
        Vector3 result = Vector3.zero;

        float tt = ((float)segments.Length) * t;
        int segmentIndex = Mathf.FloorToInt(tt);

        //Transfor composite T to segment's
        float lowerT = sizeSegmentT * segmentIndex;
        float localT = (t - lowerT) / sizeSegmentT;

        result = segments[segmentIndex].EvaluateSegment(localT);

        return result;
    }

#if  UNITY_EDITOR
    [ContextMenu("Add new segment")]
    protected void AddSegment()
    {
        LevelSegmentBehaviour newSegment = null;
        if (segments.Length == 0)
        {
            newSegment = LevelSegmentBehaviour.CreateSegment(LevelSegmentBehaviour.SegmentType.start);
            newSegment.name = "Segment 0";
            segments = new LevelSegmentBehaviour[] { newSegment };
        }
        else
        {
            List<LevelSegmentBehaviour> segmentList = new List<LevelSegmentBehaviour>(segments);
            newSegment = LevelSegmentBehaviour.CreateSegment(LevelSegmentBehaviour.SegmentType.inner);
            newSegment.previousSegment = segmentList[segmentList.Count - 1];
            newSegment.transform.position = newSegment.GetStartPoint().position;
            newSegment.name = "Segment " + segmentList.Count;

            segmentList.Add(newSegment);
            segments = segmentList.ToArray();
        }
        newSegment.transform.SetParent(transform);
    }
#endif
}
