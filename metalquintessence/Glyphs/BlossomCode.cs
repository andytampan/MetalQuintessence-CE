using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static StringLoader;
using static TextureAssets;

namespace MetalQuintessence
{
    internal class BlossomCode
    {
        public static Texture singleGlossMask = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/blossom/output_ring_gloss_mask");
        public static Texture glossTexture = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/blossom/output_gloss");
        public static Texture blossomBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/blossom/blossomBase");
        public static Texture blossomFlower = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/blossom/blossomFlower");

        public static Texture blossomNumber = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/blossom/number");

        public static Texture blossomTransBowl = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/blossom/blossomBowl");
        public static readonly HexIndex blossomBowl = new(0, 0);
        public static readonly HexIndex blossomA = new(-1, 0);
        public static readonly HexIndex blossomB = new(-1, 1);
        public static readonly HexIndex blossomC = new(0, 1);
        public static readonly HexIndex blossomD = new(1, 0);
        public static readonly HexIndex blossomE = new(1, -1);
        public static readonly HexIndex blossomF = new(0, -1);

        public static HexIndex[] outputHexes = new HexIndex[]
    {

                blossomA,
                blossomB,
                blossomC,
                blossomD,
                blossomE,
                blossomF,
                blossomBowl,

    };
        public static bool BlossomOnCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe recipe, bool isCycleStart)
        {
            var pss = sim.simulationDict;

            bool[] blocked = new bool[6];

            for (int i = 0; i < 6; i++)
            {
                if (sim.FindAtomRelative(part, outputHexes[i]).HasValue())
                {
                    blocked[i] = true;
                }
                else
                {
                    blocked[i] = false;
                }
            }
            IEnumerable<Part> arms = sim.solutionEditor.GetSolution().parts.Where(p => p.GetType().isProgrammable);
            foreach (Part arm in arms)
            {

                for (int i = 0; i < 6; i++)
                    if (arm.GetHexPos() == part.GetHexPos() + outputHexes[i].Rotated(part.GetRotation()))
                    {
                        blocked[i] = true;
                    }
            }


            if (sim.FindAtomRelative(part, blossomBowl).GetOrDefault(out AtomReference input) && blocked.Contains(false))
            {
                pss[part].processingAtoms = new AtomType[1] { input.atomType };
                AtomType inputs = pss[part].processingAtoms[0];

                AtomType[] wheelType = null;






                API.ApplyBlossomRule(inputs, out wheelType);
                if (wheelType != null) // if there's an atomtype which means one of the requirement above are satisfied
                {
                    input.molecule.ReplaceAtom(wheelType[6], input.pos);
                    // do animation and transmute atom into the seventh atomtype i.e: the center atom
                    input.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, TransmutationEffectRenderMode.AsEffect, input.atomType, Assets.textures.atoms.projection_effect, 30f);

                    ; // create new molecule 

                    for (int i = 0; i < 6; i++)
                    {
                        if (!blocked[i]) input.molecule.AddAtom(new Atom(wheelType[i]), part.InFrontBy(outputHexes[i]));
                    }

                    // play sound
                    //  playSound(sim, MetalQuintessenceSound.blossomSound);



                    //play animation
                    var SEB = sim.solutionEditor;
                    Texture[] disposalFlashAnimation = Assets.textures.parts.disposal_flash;
                    Vector2 animationPosition = HexGrid.standardGrid.ToPixelCoords(part.GetHexPos() + blossomBowl.Rotated(part.GetRotation())) + new Vector2(80f, 0f);
                    // SEB.field_3936.Add(new BaseEffect(SEB, (enum_7)1, animationPosition, disposalFlashAnimation, 30f, Vector2.Zero, 0f));


                    //joins the molecule and add bond 
                    for (int i = 0; i < 6; i++)
                    {
                        // it's done here despite being inefficient to account for each part being blocked
                        if (!blocked[i])
                        {

                            input.molecule.AddBond(BondTypes.standard, part.InFrontBy(blossomBowl), part.InFrontBy(outputHexes[i]));
                        }
                    }
                }
            }
            return false;
        }


        public static void BlossomRender(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
        {
            Vector2 offset = new(125f, 120f);

            renderer.RenderBase(blossomBase, Vector2.Zero, offset, 0f);
            renderer.RenderUpright(blossomFlower, blossomBowl, Vector2.Zero);


            foreach (HexIndex input in outputHexes)
            {
                renderer.RenderRotating(ChromificationCode.ringedBowl, input, Vector2.Zero);
                drawPartGloss(renderer, glossTexture, singleGlossMask, Vector2.Zero, input, 0);
            }
            renderer.RenderUpright(blossomNumber, blossomF, Vector2.Zero);
        }


        private static Vector2 hexGraphicalOffset(HexIndex hex) => HexGrid.standardGrid.ToPixelCoords(hex);
        private static void drawPartGloss(PartRenderer renderer, Texture gloss, Texture glossMask, Vector2 offset, HexIndex hexOffset, float angle)
        {
            TextureRenderer.GetBatcher().shader = Assets.shaderAssets.maskedGloss; // MaskedGlossPS shader
            TextureRenderer.GetBatcher().textures[1] = gloss;
            var hex = new HexIndex(0, 0);
            Vector2 method2001 = 0.0001f * (renderer.partPos + hexGraphicalOffset(hex).Rotated(renderer.partRotation) - 0.5f * InputManager.screenSize);
            TextureRenderer.GetBatcher().texCoords = method2001;
            renderer.RenderRotating(glossMask, hexOffset, Vector2.Zero);
            TextureRenderer.GetBatcher().shader = TextureRenderer.GetBatcher().defaultSpriteShader; // previous shader
            TextureRenderer.GetBatcher().textures[1] = Assets.textures.white;
            TextureRenderer.GetBatcher().texCoords = Vector2.Zero;
        }

    }
}