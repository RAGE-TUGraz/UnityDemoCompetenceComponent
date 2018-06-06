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
                msg+=  "Hello! in this demo we will show you how to use the RAGE competence asset pack. First, you have to download the newest version from our ";
                msg += "repository (https://github.com/RAGE-TUGraz/CompetenceBasedAssets) or the ones used in this demo game (Version 01.10.2017). Please note, that each RAGE asset might depend on other RAGE assets (base assets). This ";
                msg += "information is available from the developer (mmaurer@tugraz.at for TU Graz assets) directly. Later on this information will be presented uniformly in the RAGE ecosystem.";
                break;
            case 2:
                msg += "As a second step these assets (desired assets AND base assets DLLs) have to be included in Unity. Just create an folder (in this example in RAGE/DLLs) an drop the libraries there. Then an implementation ";
                msg += "of the Bridge class is needed. It is used to allow an platform independent use by implementing platform dependent functionality with the bridge pattern. For the competence asset pack ";
                msg += "we implement the two Interfaces IDataStorage and ILog. We furthermore create a folder (in this example in RAGE/IDataStorageFolder) for storing/loading our RAGE asset data.";
                break;
            case 3:
                msg += "The thrid and final step in preparing the development environment is to provide the domain model as data basis for the competence assets. Therefore, the file ";
                msg += "RAGE/IDataStorageFolder/DomainModel.xml is created. It includes an example competence/game situation structure. We now load repeatendly new scenes (always the same one with ";
                msg += "altered content, but could be totally different scenes) and update the competence values before leaving the scene.";
                break;
            case 4:
                msg += "Please note, that this approach is only one of three posible approaches how to use this asset. The updates are based on the game situations and their outcome (success/failure). ";
                msg += "Alternative one would be to update the competence state by supplying an competence id, information of success/failure, and an update strength. ";
                msg += "Alternative two uses an activity (unique string) for the update - the information which competence is updated in which direction (up/down) and how strong (low/medium/high) ";
                msg += "needs to be stored in the domain model. ";
                break;
            case 5:
                SceneManager.LoadScene(nextSceneName);
                break;
        }
        text.text = msg;
    }
}
