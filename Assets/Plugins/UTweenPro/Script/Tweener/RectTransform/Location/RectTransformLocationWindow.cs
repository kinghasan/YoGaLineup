#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Aya.TweenPro
{
    public class RectTransformLocationWindow : PopupWindowContent
    {
        public TweenRectTransformLocation Tweener;
        public SerializedProperty ShowProperty;

        public Vector2 WindowSize => NodeSize * 5f + new Vector2(2f, 20f);
        public Vector2 NodeSize = new Vector2(50f, 50f);

        public Color UnselectBorderColor = Color.gray * 0.8f;
        public Color SelectBorderColor = Color.white;
        public Color ScreenColor = EditorGUIUtility.isProSkin ? Color.white * 0.75f : Color.white;
        public Color UiColor = EditorGUIUtility.isProSkin ? Color.cyan * 0.75f : Color.cyan;
        public Color CoordinateColor = Color.white * 0.5f;
        public Color OuterAreaColor = Color.black * 0.25f;
        public Color InnerAreaColor = Color.white * 0.35f;

        public RectTransformLocationWindow(TweenRectTransformLocation tweener)
        {
            Tweener = tweener;
        }

        public override Vector2 GetWindowSize()
        {
            return WindowSize;
        }

        public override void OnOpen()
        {

        }

        public override void OnGUI(Rect rect)
        {
            using (GUIRectArea.Create(rect))
            {
                GUILayout.Label("Location Presets", EditorStyles.boldLabel);
                using (GUIVertical.Create())
                {
                    for (var i = 0; i < 5; i++)
                    {
                        using (GUIHorizontal.Create())
                        {
                            for (var j = 0; j < 5; j++)
                            {
                                var offset = new Vector2(j * NodeSize.x, i * NodeSize.y + 18f);
                                var nodeRect = new Rect()
                                {
                                    position = offset,
                                    size = NodeSize
                                };

                                DrawNode(ShowProperty, nodeRect, j, i);
                            }
                        }
                    }
                }
            }
        }

        public override void OnClose()
        {

        }

        public void DrawNode(SerializedProperty property , Rect rect, bool showSelect = true)
        {
            var index = property.intValue;
            var x = index % 5;
            var y = (index - x) / 5;
            DrawNode(property, rect, x, y, showSelect);
        }

        public void DrawNode(SerializedProperty property, Rect rect, int x, int y, bool showSelect = true)
        {
            var index = y * 5 + x;
            var isInner = x >= 1 && x <= 3 && y >= 1 && y <= 3;
            var center = new Vector2((x + 0.5f) * NodeSize.x, (y + 0.5f) * NodeSize.y);

            var border = 3f;
            var showSpace = 3f;
            var borderRect = rect.Reduce(border);
            var showRect = borderRect.Reduce(showSpace);
            var cellSize = showRect.width / 5f;

            // Back
            EditorGUI.DrawRect(rect, isInner ? InnerAreaColor : OuterAreaColor);

            // Coordinate
            var horizontalRect = new Rect
            {
                x = showRect.x,
                y = showRect.y + showRect.height / 2f,
                width = showRect.width,
                height = 1
            };
            var verticalRect = new Rect
            {
                x = showRect.x + showRect.width / 2f,
                y = showRect.y,
                width = 1,
                height = showRect.height
            };

            EditorGUI.DrawRect(horizontalRect, CoordinateColor);
            EditorGUI.DrawRect(verticalRect, CoordinateColor);

            // Screen
            var screenRect = new Rect
            {
                x = showRect.x + cellSize,
                y = showRect.y + cellSize,
                width = cellSize * 3f,
                height = cellSize * 3f
            };

            GUIUtil.DrawEmptyRect(screenRect, ScreenColor);

            // UI
            var uiRect = new Rect
            {
                x = showRect.x + cellSize * x,
                y = showRect.y + cellSize * y,
                width = cellSize,
                height = cellSize
            };

            GUIUtil.DrawEmptyRect(uiRect, UiColor);

            // Button
            var style = EditorStyles.label;
            var click = GUI.Button(rect, GUIContent.none, style);

            var borderColor = property.intValue == index && showSelect ? SelectBorderColor : UnselectBorderColor;
            GUIUtil.DrawEmptyRect(borderRect, borderColor);

            if (click)
            {
                property.intValue = index;
                property.serializedObject.ApplyModifiedProperties();
            }

            if (!GUI.enabled)
            {
                EditorGUI.DrawRect(rect, Color.black * 0.05f);
            }
        }
    }
}
#endif