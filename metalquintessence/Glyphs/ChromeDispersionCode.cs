using Quintessential;
using System;
using System.Collections.Generic;
using System.Text;


namespace MetalQuintessence
{
    public class ChromeDispersionCode
    {
        public static Texture chromaticDispersionBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersion/chromaDispersionBase");
        public static Texture chromaticDispersionGlyphBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersion/glyphBase");
        public static Texture chromaticDispersionBond = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersion/chromaDispersionBondLoop");
        public static Texture hole = Assets.textures.parts.projection_glyph.quicksilver_input;

        public static Texture chromiumIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersion/chromiumIcon");
        public static Texture[] irisAnimation = Assets.textures.parts.iris_full;

        public static readonly HexIndex chromeDispersionInput = new(0, 0);
        public static readonly HexIndex chromeDispersionLead = new(1, 0);
        public static readonly HexIndex chromeDispersionTin = new(2, -1);
        public static readonly HexIndex chromeDispersionIron = new(3, -2);
        public static readonly HexIndex chromeDispersionCopper = new(3, -3);
        public static readonly HexIndex chromeDispersionSilver = new(2, -3);
        public static readonly HexIndex chromeDispersionGold = new(1, -2);
        public static readonly HexIndex chromeDispersionQuicksilver = new(0, -1);
        public static readonly HexIndex chromeDispersionGlyphA = new(1, -1);
        public static readonly HexIndex chromeDispersionGlyphB = new(2, -2);
        public static void ChromeDispersionRender(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
        {
            Vector2 offset = new(90f, 296f);


            renderer.RenderUpright(chromaticDispersionGlyphBase, chromeDispersionGlyphA, Vector2.Zero);
            renderer.RenderUpright(chromaticDispersionGlyphBase, chromeDispersionGlyphB, Vector2.Zero);
            renderer.RenderBase(chromaticDispersionBase, Vector2.Zero, offset, 0f);
            renderer.RenderRotating(hole, chromeDispersionInput, Vector2.Zero);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();
            AtomType[] cardinalAtoms = new AtomType[7]
            {
                AtomTypes.lead,
                AtomTypes.tin,
                AtomTypes.iron,
                AtomTypes.copper,
                AtomTypes.silver,
                AtomTypes.gold,
                AtomTypes.quicksilver
            };

            HexIndex[] outputHexes = new HexIndex[7]
            {
                chromeDispersionLead,
                chromeDispersionTin,
                chromeDispersionIron,
                chromeDispersionCopper,
                chromeDispersionSilver,
                chromeDispersionGold,
                chromeDispersionQuicksilver
            };



            if (pss.isProcessing)
            {
                irisFrame = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
            }

            for (int i = 0; i < 7; i++)
            {
                HexIndex h = outputHexes[i];
                Vector2 risingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(h).Rotated(uco.rotation);
                Molecule risingAtom = Molecule.CreateMonoatomic(cardinalAtoms[i]);

                renderer.RenderRotating(hole, h, Vector2.Zero);
                if (pss.isProcessing && !afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
                renderer.RenderUpright(irisAnimation[irisFrame], h, Vector2.Zero);
                if (pss.isProcessing && afterIrisOpens)
                {
                    // show atom rising infront of iris
                    Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            // 90f, 296f ????????????????? I end up just eyeballing this
            renderer.RenderBase(chromaticDispersionBond, Vector2.Zero, offset, 0f);
            Vector2 offsetRotate0 = new(48f, 366f); // -48 and +70
            renderer.RenderBase(chromaticDispersionBond, Vector2.Zero, offsetRotate0, 0f);
            Vector2 offsetRotate1 = new(172f, 296f); // +82 and 0
            renderer.RenderBase(chromaticDispersionBond, Vector2.Zero, offsetRotate1, Convert.ToSingle(Math.PI / 3));
            Vector2 offsetRotate2 = new(214f, 366); // -42 and +70
            renderer.RenderBase(chromaticDispersionBond, Vector2.Zero, offsetRotate2, Convert.ToSingle(Math.PI / 3));
            Vector2 offsetRotate3 = new(294f, 225);
            renderer.RenderBase(chromaticDispersionBond, Vector2.Zero, offsetRotate3, Convert.ToSingle(Math.PI / 3 * 2));
            renderer.RenderUpright(chromiumIcon, chromeDispersionInput, Vector2.Zero);
        }


        public static bool ChromeDispersionOnCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe recipe, bool isCycleStart)
        {

            HexIndex[] outputHexes = new HexIndex[7]
            {
                   chromeDispersionLead,
                   chromeDispersionTin,
                   chromeDispersionIron,
                   chromeDispersionCopper,
                   chromeDispersionSilver,
                   chromeDispersionGold,
                   chromeDispersionQuicksilver
            };

            if (isCycleStart && !simState.isProcessing)
            {
                if (sim.FindAtomRelative(part, chromeDispersionInput).GetOrDefault(out AtomReference chromium) && !chromium.inMultiAtomMolecule && !chromium.isHeldByArm && chromium.atomType == MetalQuintessence.Chromium)
                {
                    // (sim, MetalQuintessenceSound.chromatic_dispersionSound);
                    bool blocked = false; //
                    foreach (HexIndex h in outputHexes)
                    {

                        if (sim.FindAtomRelative(part, h).HasValue())
                        {
                            blocked = true;
                            break;
                        }
                    }
                    if (!blocked)
                    {
                        chromium.molecule.RemoveAtom(chromium.pos);
                        //  Brimstone.API.DrawFallingAtom(seb, chromium);
                        simState.isProcessing = true;
                        foreach (HexIndex h in outputHexes)
                        {
                            sim.additionalCollisions.Add(new()
                            {
                                type = Sim.ColliderType.Atom,
                                center = HexGrid.standardGrid.ToPixelCoords(h),
                                radius = 15f,
                            });
                        }
                    }
                }
            }
            else if (simState.isProcessing)
            {
                AtomType[] cardinalAtoms = new AtomType[7]
                {
                        AtomTypes.lead,
                        AtomTypes.tin,
                        AtomTypes.iron,
                        AtomTypes.copper,
                        AtomTypes.silver,
                        AtomTypes.gold,
                        AtomTypes.quicksilver
                };

                for (int i = 0; i < 7; i++)
                {
                    // Spawn new atom

                    Molecule molecule = new();
                    molecule.AddAtom(new Atom(cardinalAtoms[i]), part.InFrontBy(outputHexes[i]));
                    sim.molecules.Add(molecule);

                }
            }

            return false;
        }
    }
}
