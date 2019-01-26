using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Delroda {
	public class OpenPersistentDataFolder : ScriptableWizard {

		[MenuItem("Delroda/Open Persistent Folder")]
		static void CreateWizard() {
			System.Diagnostics.Process.Start(Application.persistentDataPath);
		}
	}
	public class OpenProjectFolder : ScriptableWizard {

		[MenuItem("Delroda/Open Project Folder")]
		static void CreateWizard() {
			System.Diagnostics.Process.Start(Application.dataPath);
		}
	}
}
