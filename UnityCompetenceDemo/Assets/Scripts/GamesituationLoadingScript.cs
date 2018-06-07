using AssetManagerPackage;
using CompetenceComponentNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesituationLoadingScript : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Text evaluationState;

    private static bool isAssetpackCreated = false;
    string gsName;

    private void Awake()
    {
        if (!isAssetpackCreated)
        {
            // in here: create Component, determine settings
            AssetManager.Instance.Bridge = new Bridge();

            //CompetenceComponent
            CompetenceComponent competenceComponent = CompetenceComponent.Instance;
            CompetenceComponentSettings settings = new CompetenceComponentSettings();
            settings.NumberOfLevels = 3;
            settings.SourceFile = "dataModel.xml";
            settings.LinearDecreasionOfCompetenceValuePerDay = 0.1f;
            competenceComponent.Settings = settings;
            

            competenceComponent.Initialize();

            isAssetpackCreated = true;
        }

    }

    // Use this for initialization
    void Start ()
    {
        //in here: we fill the screen with content based on the current competence to learn/test

        //load current game situation
        gsName = CompetenceComponent.Instance.GetCompetenceRecommendation();



        string message = "Now the game presents a competence which needs to be trained/tested next. ";
        message += "In our case it is the competence <b><i>'" + gsName + "'</i></b>. Based on the player's performance there is evidence for (success) or against (failure) ";
        message += "the possession of this competence. Since this is a simple simulation the gameplay is simulated as a button click.";
        

        text.text = message;


        //display evaluation state
        CompetenceAssessmentObject cao =  CompetenceComponent.Instance.getAssessmentObject();
        string txt = "";
        foreach (AssessmentCompetence ac in cao.competences)
        {
            txt += ac.id + "_" + (Mathf.Round(100.0f*ac.value)/100.0f).ToString() + "_" +ac.timestamp+"\n";
        }
        evaluationState.text = txt;
    }
    

    private void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void UpdateCompetenceState(bool success)
    {
        //in here: the update of the current gamesituation is done based on the user input
        CompetenceComponent.Instance.Update(gsName,success);
        LoadNextGamesituation();
    }

    public void ResetEvaluationState()
    {
        CompetenceComponent.Instance.ResetCompetenceState();
        LoadNextGamesituation();
    }

    private void LoadNextGamesituation()
    {
        //Here the same Scene is loaded again and again
        //of course one could load different scenes - one for each gamesituation
        SceneManager.LoadScene("GamesituationLoadingScene");
    }
}
