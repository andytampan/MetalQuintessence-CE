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

    public const string PigmentationPermission = "pigmentation";
    public const string ChromeDispersionPermission = "chromedispersion";

    public override void LoadContent() 
    {
        Logger.Log("Metal Quintessence - Implementing sanity check");
        Atom.AddAtom();
        Instance.AddPuzzlePermission(PigmentationPermission);
        Instance.AddPuzzlePermission(ChromeDispersionPermission);
        PartCode.AddPartsType();

        

    }
    public override void Unload()
    {
        // Blank
    }

    public override void LoadCompatContent()
    {

    }

    public override void FinaliseContent()
    {

    }
}