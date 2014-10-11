using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditorInternal;



public class EventsEditorWindow : EditorWindow 
{

	class Styles
	{
		//public GUIContent m_WarningContent = new GUIContent (string.Empty, EditorGUIUtility.LoadRequired("Builtin Skins/Icons/console.warnicon.sml.png") as Texture2D);
		public GUIContent m_WarningContent = new GUIContent("Warning!");
		public GUIStyle m_PreviewBox = new GUIStyle ("OL Box");
		public GUIStyle m_PreviewTitle = new GUIStyle ("OL Title");
		public GUIStyle m_LoweredBox = new GUIStyle ("TextField");
		public GUIStyle m_HelpBox = new GUIStyle ("helpbox");
		public Styles ()
		{
			m_LoweredBox.padding = new RectOffset (1, 1, 1, 1);
		}
	}

	//-----------------------------------------------------------------

	private ScriptPrescription m_ScriptPrescription;
	private const int kButtonWidth = 120;
	private const int kLabelWidth = 85;
	private Vector2 m_OptionsScroll;
	private static Styles m_Styles;
	private const string kFunctionsPath = "Visual Actions/Misc";
	private const string kEventsPath = "Visual Actions/Events";
	private const string kCustomEventsPath = "Visual Actions/Events/Custom";
	private string m_BaseClass = "MonoBehaviour";
	[SerializeField]
	private static GameObject m_GameObjectToAddTo;
	private static VisualActions m_TargetComponent;
	bool _refreshTargetComponents=false;
	int _refreshTimerCount = 0; 

	private static void Init ()
	{
		GetWindow<EventsEditorWindow> (true, "Add Events");

	}

	public static void Init(VisualActions targetComponent)
	{
		m_TargetComponent = targetComponent;
		m_GameObjectToAddTo =  targetComponent.gameObject;
		EditorPrefs.SetInt("VA_EventsEditorWindowTargetObject", m_GameObjectToAddTo.GetInstanceID());
		EditorPrefs.SetInt("VA_EventsEditorWindowTargetComponent", m_TargetComponent.GetInstanceID());

		Init ();
	}

	private void OnEnable ()
	{
		m_ScriptPrescription = new ScriptPrescription ();
		m_ScriptPrescription.m_Lang = Language.CSharp;
		m_ScriptPrescription.m_Template = GetTemplate("MonoBehaviour");

		//LOAD FILE
		string eventsDataFilePath = GetFunctionsPath() + "/" + m_BaseClass + ".functions.txt";
		LoadEventsFromFile(eventsDataFilePath);

		m_GameObjectToAddTo = EditorUtility.InstanceIDToObject( EditorPrefs.GetInt("VA_EventsEditorWindowTargetObject") ) as GameObject;
		m_TargetComponent = EditorUtility.InstanceIDToObject( EditorPrefs.GetInt("VA_EventsEditorWindowTargetComponent") ) as VisualActions;
	}

	private void OnGUI()
	{
		FunctionsGUI();
	}

	
	private void FunctionsGUI ()
	{
		if (m_Styles == null)
			m_Styles = new Styles ();

		if (m_ScriptPrescription.m_Functions == null)
		{
			GUILayout.FlexibleSpace ();
			return;
		}
		
		EditorGUILayout.BeginHorizontal ();
		{
			//GUILayout.Label ("Functions", GUILayout.Width (kLabelWidth - 4));
			
			EditorGUILayout.BeginVertical (m_Styles.m_LoweredBox);
			m_OptionsScroll = EditorGUILayout.BeginScrollView (m_OptionsScroll);
			{
				bool expanded = FunctionHeader ("General", true);
				
				for (int i = 0; i < m_ScriptPrescription.m_Functions.Length; i++)
				{
					FunctionData func = m_ScriptPrescription.m_Functions[i];
					
					if (func.name == null)
					{
						expanded = FunctionHeader (func.comment, false);
					}
					else if (expanded)
					{
						Rect toggleRect = GUILayoutUtility.GetRect (GUIContent.none, EditorStyles.toggle);
						toggleRect.x += 15;
						toggleRect.width -= 15;
						bool include = GUI.Toggle (toggleRect, func.include, new GUIContent (func.name, func.comment));
						if (include != func.include)
						{
							m_ScriptPrescription.m_Functions[i].include = include;
							SetFunctionIsIncluded(m_BaseClass, func.name, include);
						}
					}
				}
			} EditorGUILayout.EndScrollView ();
			EditorGUILayout.EndVertical ();

			if(GUILayout.Button("OK"))
			{
				AddEvents();
			}
		} EditorGUILayout.EndHorizontal ();
	}

	private bool FunctionHeader (string header, bool expandedByDefault)
	{
		GUILayout.Space (5);
		bool expanded = GetFunctionIsIncluded (m_BaseClass, header, expandedByDefault);
		bool expandedNew = GUILayout.Toggle (expanded, header, EditorStyles.foldout);
		if (expandedNew != expanded)
			SetFunctionIsIncluded (m_BaseClass, header, expandedNew);
		return expandedNew;
	}


	private void AddEvents()
	{
		//CREATE files
		//------------
		//For each checked event,
		//Check if script exists. If it doesn't, create one.
		//Add script to selected object, and set target to targetComponent

		for(int i=0; i< m_ScriptPrescription.m_Functions.Length; i++)
		{
			if(m_ScriptPrescription.m_Functions[i].include == true)
			{
				m_ScriptPrescription.m_ClassName = m_ScriptPrescription.m_Functions[i].name + "Event";
				string eventsPath = GetEventsPath() + "/" + m_ScriptPrescription.m_ClassName + ".cs";
				string customEventsPath = GetPath(kCustomEventsPath) + "/" + m_ScriptPrescription.m_ClassName + ".cs";
				//if file doesn't exist, create it
				if(File.Exists(eventsPath) == false && File.Exists(customEventsPath) == false )
				{
					//Should use MonoScripts.CreateMonoScript instead?
					//MonoScripts.CreateMonoScript(new NewScriptGenerator(m_ScriptPrescription).ToString (i), m_ScriptPrescription.m_ClassName, typeof(EventClass).ToString() , null , false);

					var writer = new StreamWriter (eventsPath);
					writer.Write (new NewScriptGenerator(m_ScriptPrescription).ToString (i));
					writer.Close ();
					writer.Dispose ();	
				}

				//Attach the component
				//InternalEditorUtility.AddScriptComponentUnchecked (m_GameObjectToAddTo,
				//                                                   AssetDatabase.LoadAssetAtPath (filePath, 
				//                               						typeof (MonoScript)) as MonoScript);
			}
			
		}
		
		AssetDatabase.Refresh();

		//ATTACH the components @ close window
		//---------------------
		_refreshTargetComponents = true;
		ShowNotification(new GUIContent("Adding events. Please wait..."));
		//AttachComponents();
	}

	void AttachComponents()
	{

		//ATTACH the components
		//---------------------
		for(int i=0; i< m_ScriptPrescription.m_Functions.Length; i++)
		{
			if(m_ScriptPrescription.m_Functions[i].include == true)
			{
				EventClass anEvent = m_GameObjectToAddTo.AddComponent(m_ScriptPrescription.m_Functions[i].name + "Event") as EventClass;
				anEvent.Target = m_TargetComponent;
				anEvent.hideFlags = HideFlags.HideInInspector;
			}
		}

		Close ();
		//GUIUtility.ExitGUI ();
	}

	//------------------------------------------
	//CHECK: Is compiling? Else, attach components
	//------------------------------------------
	void Update()
	{
		if(_refreshTargetComponents == true)	//The signal that tells update we want to attach components
		{
			_refreshTimerCount++;

			//Wait for a second before trying to attach components
			if(_refreshTimerCount > 100)
			{
				//If still compiling, wait another second
				if(EditorApplication.isCompiling == true)
				{
					_refreshTimerCount = 0;
				}
				else
				{
				_refreshTargetComponents = false;
				_refreshTimerCount = 0;

				AttachComponents();
				}
			}
		}
	}
//------------------------------------------------------------------------
	
	public void LoadEventsFromFile(string functionDataFilePath)
	{

		if (!File.Exists (functionDataFilePath))
		{
			m_ScriptPrescription.m_Functions = null;
		}
		else
		{
			StreamReader reader = new StreamReader (functionDataFilePath);
			List<FunctionData> functionList = new List<FunctionData> ();
			int lineNr = 1;
			while (!reader.EndOfStream)
			{
				string functionLine = reader.ReadLine ();
				string functionLineWhole = functionLine;
				try
				{
					if (functionLine.Substring (0, 7).ToLower () == "header ")
					{
						functionList.Add (new FunctionData (functionLine.Substring (7)));
						continue;
					}
					
					FunctionData function = new FunctionData ();
					
					bool defaultInclude = false;
					if (functionLine.Substring (0, 8) == "DEFAULT ")
					{
						defaultInclude = true;
						functionLine = functionLine.Substring (8);
					}
					
					if (functionLine.Substring (0, 9) == "override ")
					{
						function.isVirtual = true;
						functionLine = functionLine.Substring (9);
					}
					
					string returnTypeString = GetStringUntilSeperator (ref functionLine, " ");
					function.returnType = (returnTypeString == "void" ? null : returnTypeString);
					function.name = GetStringUntilSeperator (ref functionLine, "(");
					string parameterString = GetStringUntilSeperator (ref functionLine, ")");
					if (function.returnType != null)
						function.returnDefault = GetStringUntilSeperator (ref functionLine, ";");
					function.comment = functionLine;
					
					string[] parameterStrings = parameterString.Split (new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
					List<ParameterData> parameterList = new List<ParameterData> ();
					for (int i=0; i<parameterStrings.Length; i++)
					{
						string[] paramSplit = parameterStrings[i].Trim().Split (' ');
						parameterList.Add (new ParameterData (paramSplit[1], paramSplit[0]));
					}
					function.parameters = parameterList.ToArray ();
					
					function.include = GetFunctionIsIncluded (m_BaseClass, function.name, defaultInclude);
					
					functionList.Add (function);
				}
				catch (Exception e)
				{
					Debug.LogWarning ("Malformed function line: \""+functionLineWhole+"\"\n  at "+functionDataFilePath+":"+lineNr+"\n"+e);
				}
				lineNr++;
			}
			m_ScriptPrescription.m_Functions = functionList.ToArray ();
		}
	}
	
	
	private static string GetStringUntilSeperator (ref string source, string sep)
	{
		int index = source.IndexOf (sep);
		string result = source.Substring (0, index).Trim ();
		source = source.Substring (index + sep.Length).Trim (' ');
		return result;
	}

	private string GetFunctionsPath ()
	{
		//return Path.Combine (Application.dataPath, kFunctionsPath);
		return GetPath(kFunctionsPath);

	}

	private string GetEventsPath ()
	{
		//return Path.Combine (Application.dataPath, kEventsPath);
		return GetPath(kEventsPath);
	}

	private string GetPath(string kPath)
	{
		//return Path.Combine (Application.dataPath, kPath);
		return Path.Combine ("Assets/", kPath);
	}

	private bool GetFunctionIsIncluded (string baseClassName, string functionName, bool includeByDefault)
	{
		string prefName = "FunctionData_" + (baseClassName != null ? baseClassName + "_" : string.Empty) + functionName;
		return EditorPrefs.GetBool (prefName, includeByDefault);
	}
	
	private void SetFunctionIsIncluded (string baseClassName, string functionName, bool include)
	{
		string prefName = "FunctionData_" + (baseClassName != null ? baseClassName + "_" : string.Empty) + functionName;
		EditorPrefs.SetBool (prefName, include);
	}
	

	private string GetTemplate (string nameWithoutExtension)
	{
		string path = Path.Combine (GetFunctionsPath (), nameWithoutExtension + "." + extension + ".txt");
		if (File.Exists (path))
			return File.ReadAllText (path);
		
		//return kNoTemplateString;
		return "NoTemplate";
	}

	private string extension
	{
		get
		{
			switch (m_ScriptPrescription.m_Lang)
			{
			case Language.CSharp:
				return "cs";
			case Language.JavaScript:
				return "js";
			case Language.Boo:
				return "boo";
			default:
				throw new ArgumentOutOfRangeException ();
			}
		}
	}
	
}
