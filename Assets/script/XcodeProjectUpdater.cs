using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class XcodeProjectUpdater
{
	[PostProcessBuild]
	static void OnPostprocessBuild(BuildTarget buildTarget, string path)
	{
		if (buildTarget != BuildTarget.iOS) return;

		var plistPath = Path.Combine(path, "Info.plist");

		var plist = new PlistDocument();
		plist.ReadFromFile(plistPath);

		plist.root.SetString("Privacy - Bluetooth Peripheral Usage Description", "広告に使用します。");
		plist.root.SetString ("Privacy - Calendars Usage Description", "広告に使用します。");
		plist.root.SetString ("Privacy - Camera Usage Description", "広告に使用します。");
			

		plist.WriteToFile(plistPath);
	}
}