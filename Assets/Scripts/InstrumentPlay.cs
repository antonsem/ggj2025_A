using UnityEngine;

public class InstrumentPlay : MonoBehaviour
{
    
    [SerializeField]public enum Instrument{Bass, HH,Kick,Snare}
    public Instrument instrument;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            switch (instrument)
            {
                case Instrument.Bass:
                    
                    PlayRandomBassSound();
                    break;
                case Instrument.Kick:
                    FMODManager.Instance.PlaySound("SFX_DrumKitKick");

                    break;
                case Instrument.Snare:
                    FMODManager.Instance.PlaySound("SFX_DrumKitSnare");

                    break;
                case Instrument.HH :
                    FMODManager.Instance.PlaySound("SFX_DrumKitHH");

                    break;
                default:
                    break;
            }
           
        }
    }

    private void PlayRandomBassSound()
    {
        string[] bassSounds = { "SFX_BassE", "SFX_BassA", "SFX_BassD", "SFX_BassG" };
        int randomIndex = Random.Range(0, bassSounds.Length);
        FMODManager.Instance.PlaySound(bassSounds[randomIndex]);
    }
}
