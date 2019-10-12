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

    //Calculates the new position and rotation of the player given a desired displacement
    public void MoveOnTheLevel(float displacement,ref float t,ref Vector3 newPosition,ref Quaternion rotation)
    {
        //Placeholder implementation
        Vector3 oldPosition = newPosition;
        t = Mathf.Clamp01(t+0.1f * Time.deltaTime);

        newPosition = EvaluateSegments(t);

        Vector3 dir = newPosition - oldPosition;
        if (dir != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }

    public Vector3 EvaluateSegments(float t)
    {
        Vector3 result = Vector3.zero;
        int segmentIndex = Mathf.FloorToInt(((float)segments.Length) * t);

        if (segmentIndex == segments.Length)
        {
            segmentIndex--;
        }

        //Transform composite T to segment's
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
