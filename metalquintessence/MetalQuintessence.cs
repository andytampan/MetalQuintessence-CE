using Quintessential;
using System;
using Quintessential.Settings;
using System.Security.Cryptography.X509Certificates;

namespace MetalQuintessence;

internal class MetalQuintessence : QuintessentialMod, IDataMod {
    public static MetalQuintessence Instance { get; }
    public override string ModId => "metalquintessence";

    public override void Load() { }

    public static string ChromeDispersionPermission = "ChromeDispersion";
    public static string ChromificationPermission = "Chromification";

    public static string BlossomPermission = "Blossom";

    public static AtomType Chromium;

    public override void LoadContent() {

       Chromium = new AtomType()
       {
           symbol = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/chromium_symbol"),
           matteTextures = new MatteAtomTextures()
           {
               shade = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/mors_shade"),
               diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/chromium_diffuse"),
           },
           shadow = Assets.textures.atoms.shadow,
           
           
       };

       QApi.AddAtomType(Instance, Chromium, "chromium");
       QApi.AddPuzzlePermission(Instance, ChromeDispersionPermission);
        QApi.AddPuzzlePermission(Instance, ChromificationPermission);
        QApi.AddPuzzlePermission(Instance, BlossomPermission);
        MetalQuintessencePart.AddParts();
        
   
            
       
    }


    public override void LoadCompatContent() { }
    public override void FinaliseContent() { }

    public override void PostLoad() { }
	public override void Unload() { }
}

