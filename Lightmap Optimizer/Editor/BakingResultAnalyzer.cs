using UnityEngine;

public class BakingResultAnalyzer : MonoBehaviour
{
    public void AnalyzeBakingResult()
    {
        // 这里只是简单示例，实际需要根据光照贴图数据进行分析
        Debug.DrawLine(Vector3.zero, Vector3.one, Color.red, 10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector3.zero, 1f);
    }
}    