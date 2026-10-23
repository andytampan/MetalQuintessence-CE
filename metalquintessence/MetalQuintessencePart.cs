

using Quintessential;
using System;
using static Quintessential.PartCycleDelegate;

namespace MetalQuintessence
{
    public static class MetalQuintessencePart
    {


        public static Texture chromaticDispersionIcon =  AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/chromaDispersion");
        public static Texture chromaticDispersionIconHover =  AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/chromaDispersion_hover");
        public static Texture chromaticDispersionGlow =  AssetLoaderHelper.LoadTexture("textures/select/andytampan/chromaDispersion_glow");
        public static Texture chromaticDispersionStroke =  AssetLoaderHelper.LoadTexture("textures/select/andytampan/chromaDispersion_stroke");
        public static PartType ChromeDispersion;

        

        public static Texture chromificationIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/chromification");
        public static Texture chromificationIconHover = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/chromification_hover");
        public static Texture chromificationGlow = AssetLoaderHelper.LoadTexture("textures/select/andytampan/chromification_glow");
        public static Texture chromificationStroke = AssetLoaderHelper.LoadTexture("textures/select/andytampan/chromification_stroke");

        public static PartType Chromification;

        public static Texture blossomGlow = AssetLoaderHelper.LoadTexture("textures/select/andytampan/disposal_glow");
        public static Texture blossomStroke = AssetLoaderHelper.LoadTexture("textures/select/andytampan/disposal_stroke");
        public static Texture blossomIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/blossom");
        public static Texture blossomIconHover = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/blossom_hover");
        public static PartType Blossom;

        public static void AddParts()
        {


            ChromeDispersion = new()
            {
                cost = 30,
                isFullHexCover = true, // Is a glyph
                glowTexture = chromaticDispersionGlow, // Shadow/glow
                strokeTexture = chromaticDispersionStroke, // Stroke/outline
                baseTexture = chromaticDispersionIcon, // Panel icon
                hoverTexture = chromaticDispersionIconHover, // Hovered panel icon
                glyphHexes = new HexIndex[]
            {
                ChromeDispersionCode.chromeDispersionInput,
                ChromeDispersionCode.chromeDispersionLead,
                ChromeDispersionCode.chromeDispersionTin,
                ChromeDispersionCode.chromeDispersionIron,
                ChromeDispersionCode.chromeDispersionCopper,
                ChromeDispersionCode.chromeDispersionSilver,
                ChromeDispersionCode.chromeDispersionGold,
                ChromeDispersionCode.chromeDispersionGlyphA,
                ChromeDispersionCode.chromeDispersionGlyphB

            },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(MetalQuintessence.Instance.GetIdentifier("ChromeDispersion")),
            };



            QApi.AddPartTypeToPanel(ChromeDispersion, false);
            QApi.AddPartType(MetalQuintessence.Instance, ChromeDispersion, "chromedispersion", ChromeDispersionCode.ChromeDispersionRender);
            QApi.BindGlyphCycle(ChromeDispersion, new(PartCycleExecutionType.Normal, ChromeDispersionCode.ChromeDispersionOnCycle));
            MetalQuintessence.Instance.AddRecipe(new(), ChromeDispersion.Id, ChromeDispersion.Id);


            Chromification = new()
            {
                //   field_1529 = class_134.method_253("Glyph of Chromification", string.Empty), // Name
                // field_1530 = class_134.method_253("The glyph of Chromification transmutes each grade of metallic atom into a single chromium atom", string.Empty), // Description
                cost = 30, // Cost
                isFullHexCover = true, // Is a glyph
                glowTexture = chromificationGlow, // Shadow/glow
                strokeTexture = chromificationStroke, // Stroke/outline
                baseTexture = chromificationIcon, // Panel icon
                hoverTexture = chromificationIconHover, // Hovered panel icon
                glyphHexes = new HexIndex[]
                {
                ChromificationCode.chromificationBowl,
                ChromificationCode.chromificationA,
                ChromificationCode.chromificationA,
                ChromificationCode.chromificationB,
                ChromificationCode.chromificationC,
                ChromificationCode.chromificationD,
                ChromificationCode.chromificationE,
                ChromificationCode.chromificationF

                },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(MetalQuintessence.Instance.GetIdentifier("ChromeDispersion")),
            };

            QApi.AddPartTypeToPanel(Chromification, false);
            QApi.AddPartType(MetalQuintessence.Instance, Chromification, "chromification", ChromificationCode.ChromificationRender);
            QApi.BindGlyphCycle(Chromification, new(PartCycleExecutionType.Normal, ChromificationCode.ChromificationOnCycle));
            MetalQuintessence.Instance.AddRecipe(new(), Chromification.Id, Chromification.Id);


            Blossom = new()
            {
                cost = 30, // Cost
                isFullHexCover = true, // Is a glyph
                glowTexture = blossomGlow, // Shadow/glow
                strokeTexture = blossomStroke, // Stroke/outline
                baseTexture = blossomIcon, // Panel icon
                hoverTexture = blossomIconHover, // Hovered panel icon
                glyphHexes = new HexIndex[]

    {
                BlossomCode.blossomBowl,
                BlossomCode.blossomA,
                BlossomCode.blossomB,
                BlossomCode.blossomC,
                BlossomCode.blossomD,
                BlossomCode.blossomE,
                BlossomCode.blossomF

    },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(MetalQuintessence.Instance.GetIdentifier("Blossom")),
            };

            QApi.AddPartTypeToPanel(Blossom, false);
            QApi.AddPartType(MetalQuintessence.Instance, Blossom, "blossom", BlossomCode.BlossomRender);
            QApi.BindGlyphCycle(Blossom, new(PartCycleExecutionType.Normal, BlossomCode.BlossomOnCycle));
            MetalQuintessence.Instance.AddRecipe(new(), Blossom.Id, Blossom.Id);


            AtomType[] ChromiumWheel = new AtomType[]
{
                            AtomTypes.lead,
                            AtomTypes.tin,
                            AtomTypes.iron,
                            AtomTypes.copper,
                            AtomTypes.silver,
                            AtomTypes.gold,
                            AtomTypes.quicksilver

};
            API.AddBlossomRule(MetalQuintessence.Chromium, ChromiumWheel);

            AtomType[] QuintessenceWheel = new AtomType[]
            {
                            AtomTypes.water,
                            AtomTypes.salt,
                            AtomTypes.earth,
                            AtomTypes.fire,
                            AtomTypes.salt,
                            AtomTypes.air,
                            AtomTypes.salt
            };
            API.AddBlossomRule(AtomTypes.quintessence, QuintessenceWheel);
        }
        



    }
}
