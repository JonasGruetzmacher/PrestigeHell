using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using LeroGames.PrestigeHell;
using LeroGames.Tools;


public class StatsEditorWindow : EditorWindow
{
    [MenuItem("Tools/StatsEditor")]
    public static void ShowExample()
    {
        StatsEditorWindow wnd = GetWindow<StatsEditorWindow>();
        wnd.titleContent = new GUIContent("StatsEditor");
    }

    private void OnEnable()
    {
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_MyGame/Scripts/Editor/StatsEditor/StatsEditorWindow.uxml");
        TemplateContainer treeAsset = visualTree.CloneTree();
        rootVisualElement.Add(treeAsset);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_MyGame/Scripts/Editor/StatsEditor/StatsEditorWindow.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        CreateStatsListView();
    }

    private void CreateStatsListView()
    {
        FindAllStats(out Stats[] stats);

        ListView statsList = rootVisualElement.Q<ListView>("stats-list");
        statsList.makeItem = () => new Label();
        statsList.bindItem = (element, i) => (element as Label).text = stats[i].name;

        statsList.itemsSource = stats;
        statsList.fixedItemHeight = 16;
        statsList.selectionType = SelectionType.Single;

        statsList.onSelectionChange += (list) =>
        {
            foreach (var item in list)
            {
                Box statsInfoBox = rootVisualElement.Q<Box>("stats-info");
                statsInfoBox.Clear();

                Stats stats = item as Stats;

                foreach (var stat in stats.stats)
                {
                    statsInfoBox.Add(new Label(stat.name));

                    SerializedObject serializedObject = new SerializedObject(stat.BaseValue);
                    SerializedProperty serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);

                    while (serializedProperty.NextVisible(false))
                    {
                        PropertyField propertyField = new PropertyField(serializedProperty);

                        propertyField.SetEnabled(serializedProperty.name != "m_Script");
                        propertyField.Bind(serializedObject);
                        statsInfoBox.Add(propertyField);
                    }
                }
            }
        };

        statsList.Rebuild();

    }

    private void CreateStatsInfoBox()
    {

    }

    private void FindAllStats(out Stats[] stats)
    {
        var guids = AssetDatabase.FindAssets("t:Stats");

        stats = new Stats[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            stats[i] = AssetDatabase.LoadAssetAtPath<Stats>(path);
        }
    }
}