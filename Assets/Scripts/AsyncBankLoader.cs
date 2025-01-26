using System.Collections;
using UnityEngine;


    public class AsyncBankLoader : MonoBehaviour
    {
        private string mainThemeEventPath = "event:/MX_MainTheme"; 

        void Start()
        {
#if UNITY_WEBGL
            StartCoroutine(LoadMasterBankAsync());
#else
            Debug.Log("This script is intended for WebGL only."); 
            PlayMainTheme();
#endif
        }

        IEnumerator LoadMasterBankAsync()
        {
            FMODUnity.RuntimeManager.LoadBank("Master", true);

            // Wait until the Master Bank is loaded
            while (!FMODUnity.RuntimeManager.HasBankLoaded("Master"))
            {
                yield return null;
            }

            Debug.Log("Master Bank loaded successfully!");

            // Play the main theme
            PlayMainTheme();
        }

        void PlayMainTheme()
        {
            FMODManager.Instance.PlaySound("event:/MX_MainTheme");
        }
    }
