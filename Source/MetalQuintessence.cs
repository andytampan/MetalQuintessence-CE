using Quintessential;


namespace MetalQuintessence;

public class MetalQuintessence : QuintessentialMod
{
    public override string ModId => "metal_quintessence";


    public static MetalQuintessence Instance { get; }
    public override void Load()
    {
        Logger.Log("Metal Quintessence - Implementing sanity check");
    }

    public override void PostLoad()
    {

    }

    public const string PigmentationPermission = "MetalQuintessence:Pigmentation";
    public override void LoadPuzzleContent() 
    {
        Logger.Log("Metal Quintessence - Implementing sanity check");
        Atom.AddAtom();
        QApi.AddPuzzlePermission(PigmentationPermission, "Glyph of Pigmentation", "MetalQuintessence");
        PartCode.AddPartsType();

    }
    public override void Unload()
    {
        // Blank
    }
}