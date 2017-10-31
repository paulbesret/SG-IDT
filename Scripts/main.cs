using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;       //Allows us to use Lists. 
using static UnityEngine.Debug;
using static UnityEngine.Mathf;
using static UnityEngine.GameObject;
using static UnityEngine.Profiling.Profiler;
using UnityEngine.AI;
using TMPro;

public class main : MonoBehaviour {
	
	public static main m = null;   
	GestionGUI UI;
	Animator AC;
	static mailController mailControler;
	
	public string lastaction="";
	RaycastHit hit;
	float hitdist;
	Ray ray;
	Plane playerPlane = new Plane(Vector3.up, new Vector3(0,0,0));
	public Transform JoueurT;
	DeplacementPerso agentJoueur;
	
	public Camera cameraUI;
	public Camera cameraIso;
	public Camera cameraCutscene;
	
	public GameObject OrdinateurUI;
	public GameObject ArmoireUI;
	
	public static conditionsActionQuetes Caq; 
	
	Transform placeHolderCamera;
	Vector3 cameraPosition;
	
	//main m;
	void Awake () {
		
		m=this;
		Caq = GetComponent<conditionsActionQuetes>();
		JoueurT = Find("Joueur").transform;
		agentJoueur = JoueurT.GetComponent<DeplacementPerso>();
		AC = JoueurT.GetChild(0).GetChild(0).GetComponent<Animator>();
		UI = GetComponent<GestionGUI>();
		mailControler = new mailController();
		
	}
	void Start() {
		etatJeu="Jeu";
		
	}
	
	void Update () {
		gestionClics();
		GestionEtat();
	}
	
	
	public GameObject lastInteractable;
	public bool versionPC=false;
	
	public string etatJeu="";
	
	float dernierTempClic=0;
	public float TempsMinimalEntreClic=0.5f;
	
	//si versionPC : effets de mouseOver
	void gestionClics() {
		if (Input.GetMouseButtonDown(0) ) { // clic
			Ray rayIso = cameraIso.ScreenPointToRay(Input.mousePosition);
			Ray rayUI = cameraUI.ScreenPointToRay(Input.mousePosition);
			
			//Gestion du clique sur interface (dialogue ou UI)
			if ((etatJeu=="Jeu"||etatJeu=="Dialogue") && dernierTempClic<(Time.realtimeSinceStartup-TempsMinimalEntreClic) && Physics.Raycast(rayUI, out hit,1 << LayerMask.NameToLayer("UI"))) {
				dernierTempClic = Time.realtimeSinceStartup;
				lastInteractable=null;
				if(hit.collider.name=="fenetre") {
					Debug.Log("toucheBouton "+hit.collider.transform.parent.name);
					boutonAction(hit.collider.transform.parent.gameObject);
					
				} else if (hit.collider.name == "closeButton") {
					hit.transform.parent.gameObject.SetActive(false);
					cameraCutscene.enabled = false;
					ActiviteEnCours = "Jeu";
				} else if (hit.collider.name == "boutonReduire") {
					hit.transform.parent.gameObject.SetActive(false);
					cameraCutscene.enabled = false;
					ActiviteEnCours = "Jeu";
				} else {
					Debug.Log("toucheBouton "+hit.collider.name);
					boutonAction(hit.collider.gameObject);
				}
				
				
				
			} else if(etatJeu=="Jeu" && Physics.Raycast(rayIso, out hit/*,1 << LayerMask.NameToLayer("Interactable")*/)) {
			//check objet
				//Player hit an interactable
				if(hit.collider.gameObject.layer == 8){ //8 = ID du Layer (contenu dans le nom)
					Debug.Log("toucheInteractable "+hit.collider.name);
					lastInteractable=hit.collider.gameObject; //va permettre de déclencher les actions
				}
				agentJoueur.gotoo(rayIso.GetPoint(hit.distance));
				interactionAction(hit.collider.gameObject);
			//sinon check sol pour déplacement
			} else if (etatJeu=="Jeu" && playerPlane.Raycast(rayIso, out hitdist)) {
				Debug.Log("toucheSol");
				lastInteractable=null;
				agentJoueur.gotoo(rayIso.GetPoint(hitdist));
			}
		}
	}
	
	public string ActiviteEnCours="Jeu"; //ActiviteEnCours
	public void GestionEtat() {
		
		if(cameraCutscene.enabled == true){
			if(Input.GetKeyDown(KeyCode.Escape)){
				ActiviteEnCours = "Jeu";
				cameraCutscene.enabled = false;
			}
		}
		
		//gère quand dans menu/dialogue/miniJeu
		if(ActiviteEnCours == "Ordinateur"){
			if (main.m.lastInteractable != null){
				placeHolderCamera = main.m.lastInteractable.transform.GetChild(0);
				cameraPosition = placeHolderCamera.position;
				cameraCutscene.transform.position = cameraPosition;//Translate(cameraPosition);
				cameraCutscene.transform.rotation = placeHolderCamera.rotation;//Translate(cameraPosition);
				cameraCutscene.enabled = true;
				OrdinateurUI.SetActive(true);
			}
		} else if (ActiviteEnCours == "Armoire"){
			if(main.m.lastInteractable != null){
				placeHolderCamera = main.m.lastInteractable.transform.GetChild(0);
				cameraPosition = placeHolderCamera.position;
				cameraCutscene.transform.position = cameraPosition;//Translate(cameraPosition);
				cameraCutscene.transform.rotation = placeHolderCamera.rotation;//Translate(cameraPosition);
				cameraCutscene.enabled = true;
				ArmoireUI.SetActive(true);
			}
		}
	}
	
	public void startInteraction(string Action){
		
		switch (Action)
		{
			case "Ordinateur":
				//Player open the PC
				ActiviteEnCours = "Ordinateur";
				AC.CrossFade("dance", 0.5f);
				break;
				
			case "Armoire":
				//Player open the bookshelf
				AC.CrossFade("pickup", 0.5f);
				ActiviteEnCours = "Armoire";
				break;
		}
		
	}
	
	
	public void boutonAction(GameObject objetClique) {
		string nomBouton = objetClique.name;
		switch (nomBouton)
		{
		case "choix1": 
			UI.next(1);
			break;
		case "choix2": 
			UI.next(2);
				break;
		case "choix3": 
			UI.next(3);
				break;
		case "choix4": 
			UI.next(4);
				break;
		default:
			break;
		}
		
		
	}
	
	public void interactionAction(GameObject objetClique) {
		string nomAction = objetClique.name;
		switch (nomAction)
		{
			
		default:
			break;
		}
	}
	
	public void actionPNJ(string PNJnom) {
		Log("actionPNJ "+PNJnom);
		switch (PNJnom)
		{
		case "Directeur":
			UI.openDialogue(PNJnom);
			etatJeu = "Dialogue";
			
			
			
			break;
		default:
			break;
		}
	}
	/*	public void actionOutil(string toolName) {
		Log("actionOutil "+toolName);
		switch (toolName)
		{
		case "Chaise":
			Animator AC = agentJoueur.t.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
			AC.CrossFade("dance", 0.5f);
			
			
			
			break;
		default:
			break;
		}
	}
	*/
	
	public string queteEnCours; //"firstQuest"
	public Transform Mails;
	//static TextMeshPro mailContent = Mails.GetChild(0).GetComponent<TextMeshPro>();
	public int nbActiveMails = 0;
	public void showMail(int idMail){
		GameObject mailContainer;
		TextMeshPro mailContent ;
		ArrayList mailDirecteur ;
		switch (nbActiveMails)
		{
		case 0:
			mailContainer = Mails.transform.GetChild(nbActiveMails).gameObject;
			mailContainer.SetActive(true);
			 mailDirecteur = mailController.getMail(idMail);
			 mailContent = Mails.GetChild(nbActiveMails).GetChild(0).GetComponent<TextMeshPro>();
			mailContent.text = "De: "+mailDirecteur[0]+"\n Objet: "+mailDirecteur[1];
		break;
		case 1:
			 mailContainer = Mails.transform.GetChild(nbActiveMails).gameObject;
			mailContainer.SetActive(true);
			 mailDirecteur = mailController.getMail(idMail);
			 mailContent = Mails.GetChild(nbActiveMails).GetChild(0).GetComponent<TextMeshPro>();
			mailContent.text = "De: "+mailDirecteur[0]+"\n Objet: "+mailDirecteur[1];
		break;	
		default:
			break;
		}
		if(nbActiveMails == 0){
			
		}
	}
}
