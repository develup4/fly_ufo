using UnityEngine;
using System.Collections;

public class FlightReady : MonoBehaviour
{
    public void OnFlightReadyButton()
    {
        Application.LoadLevel("FlightReady");
    }

	public void Labo()
	{
		Application.LoadLevel("Store_");
	}
}
