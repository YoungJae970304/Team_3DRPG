#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WARPSTARAvatarTools
{ 
    public class NormalConverter : EditorWindow
    {

        private Object obj;
        string SavePath() => $"Assets/{GetType()}.json";
        string OldObjName;

        [SerializeField] GameObject _ConvertFBX;
        [MenuItem("WARPSTAR AvatarTools/Normalg Converter")]
        public static void ShowWindow()
        {
            //既存のウィンドウのインスタンスを表示。ない場合は作成します。
            EditorWindow.GetWindow(typeof(NormalConverter));
        }

        //開くときにセーブデータを読み込み
        void OnEnable()
        {
            //Debug.Log(data);
            OldObjName = EditorUserSettings.GetConfigValue("OldObjName");

            string ObjPath = AssetDatabase.GUIDToAssetPath(OldObjName);
            if (ObjPath != null)
            {

                this.obj = (GameObject)AssetDatabase.LoadAssetAtPath(ObjPath, typeof(GameObject));

                if (this.obj == null)
                {
                    Debug.LogError("Not Setting FBX!");
                    return;
                }
                if (IsFBX(AssetDatabase.GetAssetPath(this.obj)))
                {
                    this._ConvertFBX = (GameObject)this.obj;
                    if (this._ConvertFBX == null)
                    {
                        Debug.LogError("Not Setting FBX!");
                        return;
                    }
                    createNormalsForOutline(this._ConvertFBX);
                    Debug.Log("Convert Finish");
                }
                else
                {
                    Debug.LogError("Not FBX!");
                }
            }
        }
        //閉じるときにデータを保存
        void OnDisable()
        {
            if (this.obj == null)
            {
                OldObjName = null;
            }
            EditorUserSettings.SetConfigValue("OldObjName", OldObjName);            
        }

        void OnGUI()
        {
            var so = new SerializedObject(this);
            so.Update();
            so.ApplyModifiedProperties();
            EditorGUILayout.BeginHorizontal();
            this.obj = EditorGUILayout.ObjectField(this.obj, typeof(Object), true);
            EditorGUILayout.EndHorizontal();
            if (this.obj != null) 
            {
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this.obj.GetInstanceID(), out var guid, out long localId))
                {
                    OldObjName = guid;
                }
            }
            if (GUILayout.Button("Convert"))
            {
                if (this.obj == null)
                {
                    Debug.LogError("Not Setting FBX!");
                    return;
                }
                if (IsFBX(AssetDatabase.GetAssetPath(this.obj)))
                {
                    this._ConvertFBX = (GameObject)this.obj;
                    if (this._ConvertFBX == null)
                    {
                        Debug.LogError("Not Setting FBX!");
                        return;
                    }
                    createNormalsForOutline(this._ConvertFBX);
                    Debug.Log("Convert Finish");
                }
                else
                {
                    Debug.LogError("Not FBX!");
                }
            }
        }
        bool IsFBX(string path)
        {
            return path.Contains(".fbx");
        }

        void createNormalsForOutline(GameObject g)
        {

            List<Mesh> meshes = new List<Mesh>();

            SkinnedMeshRenderer[] skinnedMeshRenderers = g.GetComponentsInChildren<SkinnedMeshRenderer>();
            MeshFilter[] meshFilters = g.GetComponentsInChildren<MeshFilter>();

            foreach (var r in skinnedMeshRenderers)
            {
                if (!meshes.Contains(r.sharedMesh))
                {
                    meshes.Add(r.sharedMesh);
                }
            }
            foreach (var r in meshFilters)
            {
                if (!meshes.Contains(r.sharedMesh))
                {
                    meshes.Add(r.sharedMesh);
                }
            }

            foreach (var mesh in meshes)
            {
                Vector3[] vertices = mesh.vertices;
                Vector3[] normals = mesh.normals;
                Vector4[] tangents = new Vector4[vertices.Length];

                Vector2[] uv2 = mesh.uv2;
                Vector2[] uv3 = mesh.uv3;


                for (int i = 0; i < normals.Length; i++)
                {
                    tangents[i] = new Vector4(uv2[i].x, uv2[i].y, uv3[i].x, uv3[i].y);
                }

                mesh.SetTangents(tangents);
            }
        }
    }
}
#endif