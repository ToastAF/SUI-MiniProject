using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class MagneticTest : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    Rigidbody rb;
    CharacterController controller;
    public float pullStrength = 1;

    LayerMask layerMask;

    bool magnetizeable = false;

    public bool isActivated;
    public Vector2 gripValue;

    [SerializeField]
    GameObject lightOnObject, lightOffObject;

    void Start()
    {
        //rb = playerBody.GetComponent<Rigidbody>();
        controller = playerBody.GetComponent<CharacterController>();
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

            if (isActivated) // MAGNETIZEMENT TIME !!!!!!!!!!!!
            {
                Vector3 directionToWall = (hit.point - playerBody.transform.position).normalized;


                controller.Move(directionToWall * pullStrength * Time.fixedDeltaTime);
                //rb.AddForce(directionToWall * pullStrength, ForceMode.Acceleration);
            }

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

    public void OnSelect(/*InputValue input*/ InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isActivated = true;
        }        

        if (context.canceled)
        {
            isActivated = false;
        }

        //gripValue = input.Get<Vector2>();

        Debug.Log("I work!");
    }
}
