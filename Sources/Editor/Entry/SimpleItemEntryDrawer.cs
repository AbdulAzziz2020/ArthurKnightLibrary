using ArthurKnight.Core;
using UnityEditor;
using UnityEngine;

namespace ArthurKnight.Editor
{
    [CustomPropertyDrawer(typeof(SimpleItemEntry))]
    public class SimpleItemEntryDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float line = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            var idProperty = property.FindPropertyRelative("id");

            bool hasValue = !string.IsNullOrEmpty(idProperty.stringValue);
            int lines = hasValue ? 2 : 1;

            return lines * line + (lines - 1) * spacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float line = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            position = EditorGUI.PrefixLabel(position, label);

            var idProperty = property.FindPropertyRelative("id");
            var nameProperty = property.FindPropertyRelative("name");
            var guidProperty = property.FindPropertyRelative("editorGuid");

            bool hasValue = !string.IsNullOrEmpty(idProperty.stringValue);

            // =========================
            // AUTO SYNC FROM SOURCE
            // =========================

            if (!string.IsNullOrEmpty(guidProperty.stringValue))
            {
                string path = AssetDatabase.GUIDToAssetPath(guidProperty.stringValue);
                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                var source = asset as IIdentity;

                if (source != null)
                {
                    if (idProperty.stringValue != source.ID ||
                        nameProperty.stringValue != source.Name)
                    {
                        idProperty.stringValue = source.ID;
                        nameProperty.stringValue = source.Name;
                        property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }
                }
                else
                {
                    // asset deleted / missing
                    idProperty.stringValue = string.Empty;
                    nameProperty.stringValue = string.Empty;
                    guidProperty.stringValue = string.Empty;

                    property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    hasValue = false;
                }
            }

            float y = position.y;

            EditorGUI.BeginChangeCheck();

            // =========================
            // PICK MODE
            // =========================
            if (!hasValue)
            {
                Rect assetRect = new Rect(position.x, y, position.width, line);

                IIdentity picked = (IIdentity)EditorGUI.ObjectField(
                    assetRect,
                    GUIContent.none,
                    null,
                    typeof(IIdentity),
                    false
                );

                if (picked != null)
                {
                    idProperty.stringValue = picked.ID;
                    nameProperty.stringValue = picked.Name;

                    string path = AssetDatabase.GetAssetPath(picked as UnityEngine.Object);
                    guidProperty.stringValue = AssetDatabase.AssetPathToGUID(path);
                }
            }
            // =========================
            // DISPLAY MODE
            // =========================
            else
            {
                Rect idRect = new Rect(position.x, y, position.width - 90, line);
                Rect clearRect = new Rect(position.x + position.width - 88, y, 44, line);
                Rect pingRect = new Rect(position.x + position.width - 42, y, 42, line);

                y += line + spacing;

                Rect nameRect = new Rect(position.x, y, position.width, line);

                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUI.TextField(idRect, idProperty.stringValue);
                    EditorGUI.TextField(nameRect, nameProperty.stringValue);
                }

                if (GUI.Button(clearRect, "Clear"))
                {
                    idProperty.stringValue = string.Empty;
                    nameProperty.stringValue = string.Empty;
                    guidProperty.stringValue = string.Empty;
                }

                if (GUI.Button(pingRect, "Ping"))
                {
                    if (!string.IsNullOrEmpty(guidProperty.stringValue))
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guidProperty.stringValue);
                        var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);

                        if (asset != null)
                            EditorGUIUtility.PingObject(asset);
                    }
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }
    }
}