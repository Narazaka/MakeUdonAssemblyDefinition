using UnityEngine;
using UnityEditor;

namespace Narazaka.VRChat.MakeUdonAssemblyDefinition
{
    public class MakeUdonAssemblyDefinition
    {
        [MenuItem("Assets/Create/U# + Unity Assembly Definition", priority = 98)]
        static void CreateUdonAssemblyDefinition()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets";
            }
            else if (!System.IO.Directory.Exists(path))
            {
                path = System.IO.Path.GetDirectoryName(path);
            }
            var asmdefDefaultName = System.IO.Path.GetFileName(path);
            var asmdefPath = EditorUtility.SaveFilePanelInProject("Save Assembly Definition", asmdefDefaultName, "asmdef", "", path);
            if (string.IsNullOrEmpty(asmdefPath))
            {
                return;
            }
            var asmdefName = System.IO.Path.GetFileNameWithoutExtension(asmdefPath);
            var asmdefDir = System.IO.Path.GetDirectoryName(asmdefPath);
            var assemblyDef = new AssemblyDefinitionJson()
            {
                name = asmdefName,
            };
            string json = JsonUtility.ToJson(assemblyDef, true);
            string filePath = System.IO.Path.Combine(asmdefDir, asmdefName + ".asmdef");
            System.IO.File.WriteAllText(filePath, json);
            AssetDatabase.Refresh();

            var udonAsmdef = ScriptableObject.CreateInstance<UdonSharpEditor.UdonSharpAssemblyDefinition>();
            udonAsmdef.sourceAssembly = AssetDatabase.LoadAssetAtPath<UnityEditorInternal.AssemblyDefinitionAsset>(filePath);
            var udonAsmdefPath = System.IO.Path.Combine(asmdefDir, asmdefName + ".asset");
            AssetDatabase.CreateAsset(udonAsmdef, udonAsmdefPath);
            AssetDatabase.Refresh();
        }
    }
}
