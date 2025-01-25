using FMODUnity;
using UnityEngine;

public class FMODManager
{
	public static FMODManager Instance
	{
		get
		{
			_instance ??= new FMODManager();
			return _instance;
		}
	}

	private static FMODManager _instance;

	public void PlaySound(string eventName)
	{
		RuntimeManager.PlayOneShot(eventName);
	}

	public void SetGlobalParameterValue(string parameterName, int parameterValue)
	{
		RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
	}
}
