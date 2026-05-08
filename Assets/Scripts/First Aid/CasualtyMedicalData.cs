using UnityEngine;

public class CasualtyMedicalData : MonoBehaviour
{
    public IncidentType incidentType;

    [HideInInspector] public string injury;
    [HideInInspector] public string consciousness;
    [HideInInspector] public string breathing;
    [HideInInspector] public string bleeding;
    [HideInInspector] public string pain;
    [HideInInspector] public string extraState;

    public enum IncidentType
    {
        RTC,
        HouseFire,
        CardiacArrest,
        FallHeight
    }

    void Start()
    {
        GenerateCasualty();
    }

    void GenerateCasualty()
    {
        injury = GetRandomInjury();

        consciousness = RandomState(
            "Alert",
            "Confused",
            "Unresponsive"
        );

        breathing = RandomState(
            "Normal breathing",
            "Rapid breathing",
            "Slow breathing",
            "Not breathing"
        );

        bleeding = RandomState(
            "No bleeding",
            "Minor bleeding",
            "Moderate bleeding",
            "Severe bleeding"
        );

        pain = RandomState(
            "Mild pain",
            "Moderate pain",
            "Severe pain",
            "Agonising pain"
        );

        extraState = RandomState(
            "Panicking",
            "Unable to walk",
            "Dizzy",
            "No additional concerns"
        );
    }

    string GetRandomInjury()
    {
        switch (incidentType)
        {
            case IncidentType.RTC:
                return RandomState(
                    "Broken arm",
                    "Head injury",
                    "Glass cuts",
                    "Crushed leg"
                );

            case IncidentType.HouseFire:
                return RandomState(
                    "Burns",
                    "Smoke inhalation",
                    "Severe burns"
                );

            case IncidentType.CardiacArrest:
                return "Cardiac arrest symptoms";

            case IncidentType.FallHeight:
                return RandomState(
                    "Broken pelvis",
                    "Spinal injury",
                    "Head trauma"
                );
        }

        return "Unknown injury";
    }

    string RandomState(params string[] options)
    {
        return options[Random.Range(0, options.Length)];
    }
}