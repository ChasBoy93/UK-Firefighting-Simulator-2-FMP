using UnityEngine;

public class CasualtyMedicalData : MonoBehaviour
{
    public IncidentType incidentType;

    [Header("Casualty State")]
    public string injury;
    public string consciousness;
    public string breathing;
    public string bleeding;
    public string pain;
    public string extraState;

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

        // CONSCIOUSNESS

        consciousness = RandomState(
            "Alert",
            "Confused",
            "Unresponsive"
        );

        // BREATHING

        if (consciousness == "Unresponsive")
        {
            breathing = RandomState(
                "Slow breathing",
                "Not breathing"
            );
        }
        else
        {
            breathing = RandomState(
                "Normal breathing",
                "Rapid breathing"
            );
        }

        // BLEEDING

        if (injury.Contains("Crushed") || injury.Contains("Head") || injury.Contains("Broken pelvis"))
        {
            bleeding = RandomState(
                "Moderate bleeding",
                "Severe bleeding"
            );
        }
        else
        {
            bleeding = RandomState(
                "No bleeding",
                "Minor bleeding"
            );
        }

        // PAIN

        if (consciousness == "Unresponsive")
        {
            pain = "No response";
        }
        else
        {
            pain = RandomState(
                "Mild pain",
                "Moderate pain",
                "Severe pain",
                "Agonising pain"
            );
        }

        // EXTRA STATE

        if (consciousness == "Unresponsive")
        {
            extraState = RandomState(
                "Cold to touch",
                "Weak pulse",
                "No movement"
            );
        }
        else
        {
            extraState = RandomState(
                "Panicking",
                "Unable to walk",
                "Dizzy",
                "No additional concerns"
            );
        }
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

    // TREATMENTS

    public bool ApplyCPR()
    {
        if (breathing == "Not breathing")
        {
            breathing = "Slow breathing";

            return true;
        }

        return false;
    }

    public bool ControlBleeding()
    {
        if (bleeding == "Severe bleeding")
        {
            bleeding = "Moderate bleeding";

            return true;
        }

        return false;
    }

    public bool GiveOxygen()
    {
        if (breathing == "Rapid breathing")
        {
            breathing = "Normal breathing";

            return true;
        }

        return false;
    }

    public bool ReassureCasualty()
    {
        if (extraState == "Panicking")
        {
            extraState = "Calm";

            return true;
        }

        return false;
    }

    public bool Immobilise()
    {
        if (injury.Contains("Broken") || injury.Contains("Spinal"))
        {
            extraState = "Immobilised";

            return true;
        }

        return false;
    }
}