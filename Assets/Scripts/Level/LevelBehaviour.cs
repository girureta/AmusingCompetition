using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public LevelSegmentBehaviour[] segments = new LevelSegmentBehaviour[] { };

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
