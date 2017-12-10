using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Services.Conversation.v1;
using UnityEngine.UI;
using UnityEngine;

	public class conversationscript : MonoBehaviour 
{
	private Conversation m_Conversation = new Conversation();
	private string m_WorkspaceID = "0dd40ff5-1a8a-43ac-9e83-93d0e463450d";
	private string m_Input = "Can you unlock the door?";
	public float coolDownTime;
	public InputField myTextField;

	void Start () 
	{
		Debug.Log("User: " + m_Input);
		m_Conversation.Message(OnMessage, m_WorkspaceID, m_Input);
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.Return) && Time.time > coolDownTime) 
		{
			coolDownTime += Time.time + 1;
			Debug.Log ("Enter");
			m_Conversation.Message(OnMessage, m_WorkspaceID, myTextField.text);
		}

	}

	void OnMessage (MessageResponse resp, string CustomData)
	{	
		
//		if (resp != null && (resp.intents.Length > 0 || resp.entities.Length > 0)) 
//		{
//			string intent = resp.intents[0].intent;
//			Debug.Log ("Intent: " + intent);
//			//Debug.Log ("Resp is null.");
//			//Debug.Log("Response: " + resp.output.text[0]);
//			return; 
//		}

//		void OnMessage(MessageResponse resp, string customData)
//		{
			foreach (Intent mi in resp.intents)
				Debug.Log("intent: " + mi.intent + ", confidence: " + mi.confidence);

			if (resp.output != null && resp.output.text.Length > 0)
			foreach (string txt in resp.output.text)
					Debug.Log("Output: " + txt);
		
		

	}

//	if (resp.output != null && resp.output.text.Length > 0)
//		foreach (string txt in resp.output.text)
//			Debug.Log("Output: " + txt);

	//foreach(Intent mi in resp.intents)
		//Debug.Log("intent: " + mi.intent + ", confidence: " + mi.confidence);

		//Debug.Log("response: " + resp.output.text);
		// Update is called once per frame


//	void processResponse(MessageResponse resp, string customData)
//	{
//		if (resp.output != null && resp.output.text.Length > 0) 
//		foreach (string txt in resp.output.text)
//				Debug.Log("Output: " + txt);
//
//	}

}