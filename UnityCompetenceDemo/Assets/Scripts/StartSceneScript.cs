using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour {

    private int pageNr=0;
    [SerializeField] Text text;
    [SerializeField] string nextSceneName;

	// Use this for initialization
	void Start () {
        Next();
    }

    public void Next()
    {
        pageNr++;
        DisplayNextPage();
    }

    private void DisplayNextPage()
    {
        string msg = "";
        switch (pageNr)
        {
            case 1:
                //Bridge implementation
                msg+=  "Hello! in this demo we will show you how to use the RAGE competence acomponent. First, you have to download the newest version from our ";
                msg += "repository (https://github.com/RAGE-TUGraz/CompetenceComponent) or the ones used in this demo game (Version 07.106.2018). Please note, that each RAGE component might depend on other RAGE components (base components). This ";
                msg += "information is available from the developer (mmaurer@tugraz.at for TU Graz components) directly. Later on, this information will be presented uniformly in the RAGE ecosystem.";
                break;
            case 2:
                msg += "As a second step these components (desired components AND base components DLLs) have to be included in Unity. Just create an folder (in this example in RAGE/DLLs) an drop the libraries there. Then an implementation ";
                msg += "of the Bridge class is needed. It is used to allow an platform independent use by implementing platform dependent functionality with the bridge pattern. For the competence component ";
                msg += "we implement the two Interfaces IDataStorage and ILog. We furthermore create a folder (in this example in RAGE/IDataStorageFolder) for storing/loading our RAGE component data.";
                break;
            case 3:
                msg += "The thrid and final step in preparing the development environment is to provide the data model as data basis for the competence component. Therefore, the file ";
                msg += "RAGE/IDataStorageFolder/dataModel.xml is created. It includes an example competence structure. We now load repeatendly new scenes (always the same one with ";
                msg += "altered content, but could be totally different scenes) and update the competence values before leaving the scene.";
                break;
            case 4:
                msg += "Please note, that this is a simplified demonstration of this prototype. This should help to make the basic idea better understandable and to continue the discussion for further improvements.";
                msg += " The updates are based on the player performance (success/failure). You need to adapt the variable 'IDataStoragePath' in the Bridge.cs script.";

                break;
            case 5:
                SceneManager.LoadScene(nextSceneName);
                break;
        }
        text.text = msg;
    }
}
