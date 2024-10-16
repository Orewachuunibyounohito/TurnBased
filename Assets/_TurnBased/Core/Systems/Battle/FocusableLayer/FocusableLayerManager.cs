using System.Collections.Generic;
using TurnBasedPractice.BattleCore.Selection;

public static class FocusableLayerManager
{
    private static Dictionary<FocusableLayer, List<IFocusable>> focusLayers = new Dictionary<FocusableLayer, List<IFocusable>>();

    public static FocusableLayer CurrentLayer{ get; private set; }
    public static void Add(FocusableLayer layer, List<IFocusable> focusables) => focusLayers.Add(layer, focusables);
    public static void Update(FocusableLayer layer, List<IFocusable> newFocusables) => focusLayers[layer] = newFocusables;
    public static void Remove(FocusableLayer layer, IFocusable focusable) => focusLayers[layer].Remove(focusable);
    public static List<IFocusable> EnterLayer(FocusableLayer layer){
        CurrentLayer = layer;
        return focusLayers[layer];
    }
    public static List<IFocusable> PeekFocusLayer(FocusableLayer layer) => focusLayers[layer];
    public static List<IFocusable> PeekCurrentLayer() => focusLayers[CurrentLayer];
}
