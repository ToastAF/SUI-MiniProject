using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

public class MagneticTest : MonoBehaviour
{
    [SerializeField] GameObject playerBody, locomotionObject, magnet, beamAttract, beamRepulse;
    Animator magnetAnim;
    public bool isPlus = true;

    Rigidbody rb;
    CharacterController controller;
    public float pullStrength = 1;

    LayerMask layerPlus, layerMinus;

    public bool beamOn = false;

    public bool isActivated;
    public Vector2 gripValue;

    [SerializeField]
    GameObject lightOnObject, lightOffObject;

    void Start()
    {
        //rb = playerBody.GetComponent<Rigidbody>();
        controller = playerBody.GetComponent<CharacterController>();
        layerPlus = LayerMask.GetMask("MagneticPlus");
        layerMinus = LayerMask.GetMask("MagneticMinus");
        lightOnObject.SetActive(false);
        lightOffObject.SetActive(true);

        magnetAnim = magnet.GetComponent<Animator>();
    }



    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMinus))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);


            if (isActivated) // MAGNETIZEMENT TIME !!!!!!!!!!!!
            {
                locomotionObject.SetActive(false);
                beamAttract.SetActive(true);

                /*if (!beamOn)
                {
                    GameObject temp = Instantiate(magneticBeam, transform.position, Quaternion.identity);
                    LineRendererUpdate tempScript = temp.GetComponent<LineRendererUpdate>();
                    tempScript.endPos = hit.point;
                    tempScript.startPos = magnet.transform;
                    tempScript.magnetScript = this;

                    beamOn = true;
                }*/

                Vector3 directionToWall = (hit.point - playerBody.transform.position).normalized;


                controller.Move(directionToWall * pullStrength * Time.fixedDeltaTime);
                //rb.AddForce(directionToWall * pullStrength, ForceMode.Acceleration);
            }
            else
            {
                locomotionObject.SetActive(true);
                beamAttract.SetActive(false);
            }

            lightOnObject.SetActive(true);
            lightOffObject.SetActive(false);

            //Debug.Log("Did Hit");
        }else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerPlus))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);


            if (isActivated) // MAGNETIZEMENT TIME !!!!!!!!!!!!
            {
                locomotionObject.SetActive(false);

                beamRepulse.SetActive(true);


                Vector3 directionToWall = (hit.point - playerBody.transform.position).normalized;


                controller.Move(-directionToWall * pullStrength * Time.fixedDeltaTime);
                //rb.AddForce(directionToWall * pullStrength, ForceMode.Acceleration);
            }
            else
            {
                locomotionObject.SetActive(true);
                beamRepulse.SetActive(false);
            }

            lightOnObject.SetActive(true);
            lightOffObject.SetActive(false);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            locomotionObject.SetActive(true);

            beamAttract.SetActive(false);
            beamRepulse.SetActive(false);

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

        //Debug.Log("I work!");
    }

    public void OnActivate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPlus)
            {
                magnetAnim.SetBool("Switch to -", true);
                isPlus = false;
            }
            else
            {
                magnetAnim.SetBool("Switch to +", true);
                isPlus = true;
            }
        }

        Debug.Log("Activated!");
    }
    /*
    public void Vibrate(InputDevice device, float amplitude, float duration)
    {
        // Get the haptic capability from the device
        var command = UnityEngine.InputSystem.XR.HapticCapabilities.GetCapability(device);

        if (command.supportsImpulse)
        {
            // Parameters: channel (usually 0), amplitude (0-1), duration (seconds)
            device.SendHapticImpulse(0, amplitude, duration);
        }
    }
    */
}
