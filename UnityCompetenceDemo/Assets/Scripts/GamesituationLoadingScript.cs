using AssetManagerPackage;
using CompetenceAssessmentAssetNameSpace;
using CompetenceBasedAdaptionAssetNameSpace;
using DomainModelAssetNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesituationLoadingScript : MonoBehaviour {

    [SerializeField] Text text;

    private static bool isAssetpackCreated = false;

    private void Awake()
    {
        if (!isAssetpackCreated)
        {
            // in here: create Assets, determine settings
            AssetManager.Instance.Bridge = new Bridge();

            //Doamin Model Asset
            DomainModelAssetSettings dmas = new DomainModelAssetSettings();
            dmas.LocalSource = true;
            dmas.Source = "DomainModel.xml";
            DomainModelAsset.Instance.Settings = dmas;

            //Competence Assement Asset
            CompetenceAssessmentAssetSettings caas = new CompetenceAssessmentAssetSettings();
            caas.TransitionProbability = 0.7;
            CompetenceAssessmentAsset.Instance.Settings = caas;

            //Competence based Adaptation Asset
            //no settings needed
            isAssetpackCreated = true;
        }

    }

    // Use this for initialization
    void Start ()
    {
        //in here: we fill the screen with content based on the current gamesituation

        //load current game situation
        string gsName = CompetenceBasedAdaptionAsset.Instance.getNextGameSituationId();

        //check if all competences are mastered
        if (gsName == null)
            LoadEndScene();

        string message = "Now the game presents a game situation for training the competence associated with ";
        message += "gamesituation <b><i>'" + gsName + "'</i></b>. Based on the player's performance the gamesituation is completed  ";
        message += "either successfully or as a failure. Since this is a simple simulation the gameplay is simulated as a button click.";
        

        text.text = message;
    }
    

    private void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void UpdateCompetenceState(bool success)
    {
        //in here: the update of the current gamesituation is done based on the user input
        CompetenceBasedAdaptionAsset.Instance.setGameSituationUpdate(success);
        LoadNextGamesituation();
    }

    private void LoadNextGamesituation()
    {
        //Here the same Scene is loaded again and again
        //of course one could load different scenes - one for each gamesituation
        SceneManager.LoadScene("GamesituationLoadingScene");
    }
}
