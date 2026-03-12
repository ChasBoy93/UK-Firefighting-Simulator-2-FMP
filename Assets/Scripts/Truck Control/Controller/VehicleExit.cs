using System.Collections;
using UnityEngine;

public class VehicleExit : MonoBehaviour
{
    public GameObject vehicleCam;
    public GameObject freeLookCamControl;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public GameObject entryTrig;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            thePlayer.SetActive(true);
            vehicleCam.SetActive(false);
            freeLookCamControl.SetActive(false);

            liveVehicle.GetComponent<TruckController>().canDrive = false;

            liveVehicle.GetComponent<TruckSounds>().enabled = false;
            liveVehicle.GetComponent<TruckLights>().enabled = false;
            thePlayer.transform.parent = null;

            StartCoroutine(EnterAgain());
        }
    }

    IEnumerator EnterAgain()
    {
        yield return new WaitForSeconds(0.5f);
        entryTrig.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
