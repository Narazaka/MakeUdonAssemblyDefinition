using System;

namespace Narazaka.VRChat.MakeUdonAssemblyDefinition
{
    [Serializable]
    internal class AssemblyDefinitionJson
    {
        public string name = "NewAssembly";
        public string rootNamespace = "";
        public string[] references = new string[]
        {
            "VRC.SDKBase",
            "VRC.Udon",
            "VRC.Udon.Serialization.OdinSerializer",
            "UdonSharp.Runtime",
            "Unity.TextMeshPro",
        };
        public string[] includePlatforms = new string[0];
        public string[] excludePlatforms = new string[0];
        public bool allowUnsafeCode = false;
        public bool overrideReferences = false;
        public string[] precompiledReferences = new string[0];
        public bool autoReferenced = false;
        public string[] defineConstraints = new string[0];
        public string[] versionDefines = new string[0];
        public bool noEngineReferences = false;
    }
}
