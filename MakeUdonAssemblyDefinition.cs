using UnityEngine;
using UnityEditor;
using System.IO;

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
            else if (!Directory.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            var asmdefDefaultName = Path.GetFileName(path);
            var asmdefPath = Path.GetRelativePath(Directory.GetCurrentDirectory(), EditorUtility.SaveFilePanel("Save Assembly Definition", path, asmdefDefaultName, "asmdef"));
            if (string.IsNullOrEmpty(asmdefPath))
            {
                return;
            }
            var asmdefName = Path.GetFileNameWithoutExtension(asmdefPath);
            var asmdefDir = Path.GetDirectoryName(asmdefPath);
            var assemblyDef = new AssemblyDefinitionJson()
            {
                name = asmdefName,
            };
            string json = JsonUtility.ToJson(assemblyDef, true);
            string filePath = Path.Combine(asmdefDir, asmdefName + ".asmdef");
            File.WriteAllText(filePath, json);
            AssetDatabase.Refresh();

            var udonAsmdef = ScriptableObject.CreateInstance<UdonSharpEditor.UdonSharpAssemblyDefinition>();
            udonAsmdef.sourceAssembly = AssetDatabase.LoadAssetAtPath<UnityEditorInternal.AssemblyDefinitionAsset>(filePath);
            var udonAsmdefPath = Path.Combine(asmdefDir, asmdefName + ".asset");
            AssetDatabase.CreateAsset(udonAsmdef, udonAsmdefPath);
            AssetDatabase.Refresh();
        }
    }
}
