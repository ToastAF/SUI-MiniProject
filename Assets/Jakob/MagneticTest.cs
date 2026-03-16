using UnityEngine;

public class MagneticTest : MonoBehaviour
{
    LayerMask layerMask;

    bool magnetizeable = false;

    [SerializeField]
    GameObject lightOnObject, lightOffObject;

    void Start()
    {
        layerMask = LayerMask.GetMask("Magnetic");
        lightOnObject.SetActive(false);
        lightOffObject.SetActive(true);
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            magnetizeable = true;

            lightOnObject.SetActive(true);
            lightOffObject.SetActive(false);

            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            magnetizeable = false;

            lightOnObject.SetActive(false);
            lightOffObject.SetActive(true);

            //Debug.Log("Did not Hit");
        }
    }
}
