using UnityEngine;

public class GraphBuilder : MonoBehaviour
{
    private enum Function
    {
        none = 0,
        x = 1,
        xx = 2,
        xxx = 3,
        sinx = 4,
        cosx = 5,
        tanx = 6
    }
    
    private Function chosenFunction = Function.none;
    
    [SerializeField] private Transform pointPrefab;

    [SerializeField, Range(10, 100)] private int resolution = 10;

    private Transform[] points;

    void Awake()
    {
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;
        points = new Transform[resolution];

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }
    
    void Update()
    {
        float time = Time.time;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;

            switch (chosenFunction)
            {
                case Function.x:
                    position.y = position.x;
                    break;
                case Function.xx:
                    position.y = position.x * position.x;
                    break;
                case Function.xxx:
                    position.y = position.x * position.x * position.x;
                    break;
                case Function.sinx:
                    position.y = Mathf.Sin(Mathf.PI * (position.x + time));
                    break;
                case Function.cosx:
                    position.y = Mathf.Cos(Mathf.PI * (position.x + time));
                    break;
                case Function.tanx:
                    position.y = Mathf.Tan(position.x);
                    break;
                default:
                    position.y = 0;
                    break;
            }
            
            point.localPosition = position;
        }
    }

    public void SelectGraphFunction(int funcNo)
    {
        chosenFunction = (Function)funcNo;
    }
}