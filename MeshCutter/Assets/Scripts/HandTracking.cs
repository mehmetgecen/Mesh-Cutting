using UnityEngine;

public class HandTracking : MonoBehaviour
{
    private UDPReceive udpReceive;
    public Transform[] handPoints;

    void Start()
    {
        udpReceive = GetComponent<UDPReceive>();
    }

    void Update()
    {
        UpdateHandPoints();
    }

    private void UpdateHandPoints()
    {
        string data = udpReceive.data;

        if (data.Length.Equals(0)) return;

        data = data.Substring(1, data.Length - 2); // delete first and last c har -> [,]
        string[] points = data.Split(','); // split string and get x,y,z values

        for (int i = 0; i < 21; i++)
        {
            float x = 7f - float.Parse(points[i * 3]) / 100f; 
            float y = float.Parse(points[i * 3 + 1]) / 100f;
            float z = float.Parse(points[i * 3 + 2]) / 100f;

            handPoints[i].localPosition = new Vector3(x, y, -z);
        }
    }
}