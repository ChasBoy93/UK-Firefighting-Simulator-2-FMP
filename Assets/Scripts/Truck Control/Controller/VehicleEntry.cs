using System.Collections;
using UnityEngine;

public class VehicleEntry : MonoBehaviour
{
    public GameObject vehicleCam;
    public GameObject freeLookCamControl;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public GameObject exitTrig;
    public GameObject firefighter;

    public MDTController mdt;

    public bool canEnter = false;

    void Update()
    {
        if (canEnter == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<BoxCollider>().enabled = false;

                vehicleCam.SetActive(true);
                thePlayer.SetActive(false);
                freeLookCamControl.SetActive(true);

                liveVehicle.GetComponent<TruckController>().canDrive = true;

                liveVehicle.GetComponent<TruckSounds>().enabled = true;
                liveVehicle.GetComponent<TruckLights>().enabled = true;
                liveVehicle.GetComponent<BlueLightController>().enabled = true;
                firefighter.SetActive(true);
                canEnter = false;
                thePlayer.transform.parent = this.gameObject.transform;

                if (mdt != null)
                {
                    mdt.SetPlayerInVehicle(true);
                }

                StartCoroutine(ExitTrigger());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canEnter = false;
    }

    IEnumerator ExitTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        exitTrig.SetActive(true);
    }
}