using UnityEngine;
using UnityEditor;

public class LayerAttribute : PropertyAttribute
{
}

[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] layers = GetAllLayers();

        int layerIndex = 0;
        int currentLayer = property.intValue;
        for (int i = 0; i < layers.Length; i++)
        {
            if (LayerMask.NameToLayer(layers[i]) == currentLayer)
            {
                layerIndex = i;
                break;
            }
        }

        layerIndex = EditorGUI.Popup(position, label.text, layerIndex, layers);
        property.intValue = LayerMask.NameToLayer(layers[layerIndex]);
    }

    private string[] GetAllLayers()
    {
        var layers = new string[32];
        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (!string.IsNullOrEmpty(layerName))
            {
                layers[i] = layerName;
            }
            else
            {
                layers[i] = "Layer " + i;
            }
        }
        return layers;
    }
}