using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GetAnimationClip : Editor {
    [MenuItem("Assets/Develop Tools/Separate Anim", priority = 1)]
    public static void SeparateAnimByModel() {
        string[] strs = Selection.assetGUIDs;

        if(strs.Length > 0) {
            int gameNum = strs.Length;
            string animFolder = EditorUtility.OpenFolderPanel("select folder", "", "");
            animFolder = Path.GetRelativePath(Application.dataPath + "/..", animFolder);
            for(int i = 0; i < gameNum; i++) {
                string assetPath = AssetDatabase.GUIDToAssetPath(strs[i]);
                //Debug.Log(assetPath); //���嵽fbx��·��
                
                // ��ȡassetPath��������Դ
                Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                bool isCreate = false;
                List<Object> animation_clip_list = new List<Object>();
                foreach(Object item in assets) {
                    if(typeof(AnimationClip) == item?.GetType())//�ҵ�fbx����Ķ���
                    {
                        Debug.Log("�ҵ�����Ƭ�Σ�" + item);
                        if(!item.name.StartsWith("__preview")) {
                            animation_clip_list.Add(item);
                        }
                    }
                }
                foreach(AnimationClip animation_clip in animation_clip_list) {
                    Object new_animation_clip = new AnimationClip();
                    EditorUtility.CopySerialized(animation_clip, new_animation_clip);
                    string animation_path = Path.Combine(animFolder, new_animation_clip.name + ".anim").Replace('\\','/');
                    Debug.Log(animation_path);
                    AssetDatabase.CreateAsset(new_animation_clip, animation_path);

                    isCreate = true;
                }
                AssetDatabase.Refresh();
                if(isCreate)
                    Debug.Log("�Զ���������Ƭ�γɹ���" + animFolder);
                else
                    Debug.Log("δ�Զ���������Ƭ�Ρ�");
            }
        } else {
            Debug.LogError("��ѡ����Ҫһ����ȡ����Ƭ�ε�ģ��");
        }
    }
}
