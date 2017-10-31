
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;

	public class GestionGUI : MonoBehaviour {
		
		[System.Serializable]
		public class SubtitleDelays
		{
			public float characterDelay = 0.05f;
			public float sentenceDelay  = 0.5f;
			public float commaDelay     = 0.1f;
			public float finalDelay     = 1.2f;
		}
		
		public void openDialogue(string who) {
			
			GameObject.Find(who).GetComponent<DialogueTreeController>().StartDialogue();
			
		} 
		
		//Options...
		[Header("Input Options")]
		public bool skipOnInput;
		public bool waitForInput;
		
		//Group...
		[Header("Subtitles")]
		public RectTransform subtitlesGroup;
		public Text actorSpeech;
		public Text actorName;
		public Image actorPortrait;
		public RectTransform waitInputIndicator;
		public SubtitleDelays subtitleDelays = new SubtitleDelays();
		
		//Group...
		[Header("Multiple Choice")]
		public RectTransform optionsGroup;
		public Button optionButton;
		private Dictionary<Button, int> cachedButtons;
		private Vector2 originalSubsPosition;
		private bool isWaitingChoice;
		
		private AudioSource _localSource;
		private AudioSource localSource{
			get {return _localSource != null? _localSource : _localSource = gameObject.AddComponent<AudioSource>();}
		}
		
		
		void OnEnable(){
			DialogueTree.OnDialogueStarted       += OnDialogueStarted;
			DialogueTree.OnDialoguePaused        += OnDialoguePaused;
			DialogueTree.OnDialogueFinished      += OnDialogueFinished;
			DialogueTree.OnSubtitlesRequest      += OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;
		}
		
		void OnDisable(){
			DialogueTree.OnDialogueStarted       -= OnDialogueStarted;
			DialogueTree.OnDialoguePaused        -= OnDialoguePaused;
			DialogueTree.OnDialogueFinished      -= OnDialogueFinished;
			DialogueTree.OnSubtitlesRequest      -= OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
		}
		
		void Start(){
			/*subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
			optionButton.gameObject.SetActive(false);
			waitInputIndicator.gameObject.SetActive(false);
			originalSubsPosition = subtitlesGroup.transform.position;*/
		}
		
		void OnDialogueStarted(DialogueTree dlg){
			//nothing special...
			Debug.Log("Debug");
		}
		
		void OnDialoguePaused(DialogueTree dlg){
			subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
		}
		
		void OnDialogueFinished(DialogueTree dlg){
			dialogues[0].GetChild(0).GetComponent<Animator>().Play("fenetreDisparait");
			main.m.etatJeu="Jeu";
			/*subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
			if (cachedButtons != null){
				foreach (var tempBtn in cachedButtons.Keys){
					if (tempBtn != null){
						Destroy(tempBtn.gameObject);
					}
				}
				cachedButtons = null;
			}*/
		}
		
		
		void OnSubtitlesRequest(SubtitlesRequestInfo info){
			StartCoroutine(Internal_OnSubtitlesRequestInfo(info));
		}
		TMPro.TextMeshPro TMP;
		public Transform[] dialogues;
		IEnumerator Internal_OnSubtitlesRequestInfo(SubtitlesRequestInfo info){
			
			var text = info.statement.text;
			var audio = info.statement.audio;
			var actor = info.actor;
			
			/*subtitlesGroup.gameObject.SetActive(true);
			actorSpeech.text = "";
			
			actorName.text = actor.name;
			actorSpeech.color = actor.dialogueColor;
			
			actorPortrait.gameObject.SetActive( actor.portraitSprite != null );
			actorPortrait.sprite = actor.portraitSprite;
			*/
			var DS = dialogues[0];
			DS.gameObject.SetActive(true);
			TMP = DS.transform.GetComponentInChildren<TMPro.TextMeshPro>();
			//TMP.text = actor.name+"  "+text;
			TMP.text="";
			int charAt=0;
			while(charAt<text.Length){
				TMP.text=TMP.text+text[charAt];
				charAt++;
				
				yield return new WaitForSeconds(0.05f);
			}		
			waitForInput=true;
			
			if (waitForInput){
				//waitInputIndicator.gameObject.SetActive(true);
				while(!Input.GetMouseButtonDown(0)){
					yield return null;
				}
				//waitInputIndicator.gameObject.SetActive(false);
			}
			
			//yield return null;
			//subtitlesGroup.gameObject.SetActive(false);
			info.Continue();
		}
		
		IEnumerator CheckInput(System.Action Do){
			while(!Input.anyKeyDown){
				yield return null;
			}
			Do();
		}
		
		IEnumerator DelayPrint(float time){
			var timer = 0f;
			while (timer < time){
				timer += Time.deltaTime;
				yield return null;
			}
		}
		
		
		MultipleChoiceRequestInfo infos;
		int[] reponsestemps = new int[10];
		void OnMultipleChoiceRequest(MultipleChoiceRequestInfo info){
			infos=info;
			/*optionsGroup.gameObject.SetActive(true);
			var buttonHeight = optionButton.GetComponent<RectTransform>().rect.height;
			optionsGroup.sizeDelta = new Vector2(optionsGroup.sizeDelta.x, (info.options.Values.Count * buttonHeight) + 20);
			*/
			cachedButtons = new Dictionary<Button, int>();
			int i = 0;
			
			foreach (KeyValuePair<IStatement, int> pair in info.options){
				/*var btn = (Button)Instantiate(optionButton);
				btn.gameObject.SetActive(true);
				btn.transform.SetParent(optionsGroup.transform, false);
				btn.transform.localPosition = (Vector2)optionButton.transform.localPosition - new Vector2(0, buttonHeight * i);
				btn.GetComponentInChildren<Text>().text = pair.Key.text;
				cachedButtons.Add(btn, pair.Value);
				btn.onClick.AddListener( ()=> { Finalize(info, cachedButtons[btn]);	});
				*/
				var d =dialogues[i+1];
				d.gameObject.SetActive(true);
				d.GetComponentInChildren<Animator>().Play("fenetreApparait");
				reponsestemps[i+1] = pair.Value;
				TMP = d.GetComponentInChildren<TMPro.TextMeshPro>();
				TMP.text = pair.Key.text;
				
				i++;
			}
			
			/*if (info.showLastStatement){
				subtitlesGroup.gameObject.SetActive(true);
				var newY = optionsGroup.position.y + optionsGroup.sizeDelta.y + 1;
				subtitlesGroup.position = new Vector2(subtitlesGroup.position.x, newY);
			}
			
			if (info.availableTime > 0){
				StartCoroutine(CountDown(info));
			}*/
		}
		
		IEnumerator CountDown(MultipleChoiceRequestInfo info){
			isWaitingChoice = true;
			var timer = 0f;
			while (timer < info.availableTime){
				if (isWaitingChoice == false){
					yield break;
				}
				timer += Time.deltaTime;
				SetMassAlpha(optionsGroup, Mathf.Lerp(1, 0, timer/info.availableTime));
				yield return null;
			}
			
			if (isWaitingChoice){
				Finalize(info, info.options.Values.Last());
			}
		}
		
		void Finalize(MultipleChoiceRequestInfo info, int index){
			isWaitingChoice = false;
			SetMassAlpha(optionsGroup, 1f);
			optionsGroup.gameObject.SetActive(false);
			if (info.showLastStatement){
				subtitlesGroup.gameObject.SetActive(false);
				subtitlesGroup.transform.position = originalSubsPosition;
			}
			foreach (var tempBtn in cachedButtons.Keys){
				Destroy(tempBtn.gameObject);
			}
			info.SelectOption(index);
		}
		public void next(int ii) {
			main.m.etatJeu="Dialogue";
			dialogues[1].GetChild(0).GetComponent<Animator>().Play("fenetreDisparait");
			dialogues[2].GetChild(0).GetComponent<Animator>().Play("fenetreDisparait");
			dialogues[3].GetChild(0).GetComponent<Animator>().Play("fenetreDisparait");
			dialogues[4].GetChild(0).GetComponent<Animator>().Play("fenetreDisparait");
			dialogues[ii].GetComponentInChildren<Animator>().Play("fenetreChoisie");
			Debug.Log("next "+ii);
			infos.SelectOption(reponsestemps[ii]);
			
			
		}
		
		void SetMassAlpha(RectTransform root, float alpha){
			foreach(var graphic in root.GetComponentsInChildren<CanvasRenderer>()){
				graphic.SetAlpha(alpha);
			}
		}
	}
