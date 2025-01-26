using FMOD.Studio;
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

	public void SetBass(int level)
	{
		switch (level)
		{
			case 0:
				RuntimeManager.StudioSystem.setParameterByNameWithLabel("Bass_Speed", "Bass_High");
				break;
			case 1:
				RuntimeManager.StudioSystem.setParameterByNameWithLabel("Bass_Speed", "Bass_High");

				break;
			case 2:
				RuntimeManager.StudioSystem.setParameterByNameWithLabel("Bass_Speed", "Bass_High");

				break;
			default: 
				Debug.LogWarning("FMODManager::SetBass level out of range.");
				break;
		}
	}

	public void SetDrums(int level)
	{
		switch (level)
		{
			case 0:
				RuntimeManager.StudioSystem.setParameterByNameWithLabel("Drum_Speed", "Drum_High");
				break;
			case 1:
				RuntimeManager.StudioSystem.setParameterByNameWithLabel("Drum_Speed", "Drum_High");

				break;
			case 2:
				RuntimeManager.StudioSystem.setParameterByNameWithLabel("Drum_Speed", "Drum_High");

				break;
			default: 
				Debug.LogWarning("FMODManager::SetDrums level out of range.");
				break;
		}
	}
	public void SetGlobalParameterValue(string parameterName, int parameterValue)
	{
		RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
	}

    public void SetVCAVolume(string vcaName, float volume)
    {
        VCA vca = RuntimeManager.GetVCA("vca:/" + vcaName);
        vca.setVolume(volume);
    }

    public float GetVCAVolume(string vcaName)
    {
        VCA vca = RuntimeManager.GetVCA("vca:/" + vcaName);
        vca.getVolume(out float volume);
		return volume;
    }
}
