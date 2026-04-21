using UnityEngine;

public class LineRendererUpdate : MonoBehaviour
{
    LineRenderer lineRend;
    public Vector3 endPos;
    public Transform startPos;
    public MagneticTest magnetScript;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRend.SetPosition(0, startPos.localPosition);
        lineRend.SetPosition(1, endPos);

        if(magnetScript.isActivated == false)
        {
            magnetScript.beamOn = false;
            Destroy(gameObject);
        }
    }
}
