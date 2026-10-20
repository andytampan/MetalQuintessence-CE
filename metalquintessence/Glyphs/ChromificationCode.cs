using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetalQuintessence
{
    public class ChromificationCode
    {

        public static Texture chromificationBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromification/chromificationBase");
        public static Texture hole = Assets.textures.parts.projection_glyph.quicksilver_input;
        public static readonly HexIndex chromificationBowl = new(0, 0);
        public static readonly HexIndex chromificationA = new(-1, 0);
        public static readonly HexIndex chromificationB = new(0, -1);
        public static readonly HexIndex chromificationC = new(0, -2);
        public static readonly HexIndex chromificationD = new(2, -2);
        public static readonly HexIndex chromificationE = new(1, -1);
        public static readonly HexIndex chromificationF = new(1, 0);

        public static Texture leadIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersion/lead_symbol");

        public static Texture quicksilverIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromification/quicksilver_symbol");
        public static Texture bowl = Assets.textures.parts.calcinator_bowl;

        public static void ChromificationRender(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
        {
            Vector2 offset = new(130f, 200f);
            renderer.RenderBase(chromificationBase, Vector2.Zero, offset, 0f);

            HexIndex[] inputHex = new HexIndex[]
            {
                chromificationBowl,
                chromificationA,
                chromificationB,
                chromificationC,
                chromificationD,
                chromificationE,
                chromificationF

            };
            foreach (HexIndex input in inputHex)
            {
                renderer.RenderRotating(hole, input, Vector2.Zero);
                renderer.RenderUpright(leadIcon, input, Vector2.Zero);
            }
            renderer.RenderRotating(bowl, chromificationBowl, Vector2.Zero);
            renderer.RenderUpright(quicksilverIcon, chromificationBowl, Vector2.Zero);
        }

        public static bool ChromificationOnCycle(Sim sim, Part part, PartSimState simState, GlyphRecipe recipe, bool isCycleStart)
        {
            HexIndex[] inputHex = new HexIndex[]
                     {

                         chromificationA,
                         chromificationB,
                         chromificationC,
                         chromificationD,
                         chromificationE,
                         chromificationF

                     };
            List<AtomReference> inputs = new List<AtomReference>();
            // Input are laid
            bool inputLaid = true;
            foreach (HexIndex input in inputHex)
            {
                if (sim.FindAtomRelative(part, input).GetOrDefault(out AtomReference atom))
                {
                    inputs.Add(atom);
                }
                else
                {
                    inputLaid = false;
                    break;
                }

            }

            if (sim.FindAtomRelative(part, chromificationBowl).GetOrDefault(out AtomReference silver) & inputLaid
            )
            {


                List<AtomType> input = new List<AtomType>();

                AtomType quicksilver = silver.atomType;
                foreach (AtomReference atomReference in inputs)
                {
                    input.Add(atomReference.atomType);
                }

                AtomType[] metals = new AtomType[]
                {
                            AtomTypes.lead,
                            AtomTypes.tin,
                            AtomTypes.iron,
                            AtomTypes.copper,
                            AtomTypes.silver,
                            AtomTypes.gold,
                };
                bool requirement = true; //assume requirement is true, then
                foreach (var atom in metals.Zip(inputs, (a, r) => new { type = a, reference = r })) //this iterate each atomtype and reference as one.
                {
                    if (!metals.Contains(atom.type)) { requirement = false; break; }
                    ; //iterate if each metal is contained in the inputlist
                    if (atom.reference.inMultiAtomMolecule && atom.reference.isHeldByArm) { requirement = false; break; }
                    ; //iterate if each atom are singular and dropped
                }
                if (quicksilver == AtomTypes.quicksilver && requirement) // if requirement is still true, and if the input are quicksilver
                {
                    // playSound(sim, MetalQuintessenceSound.chromificationSound);
                    foreach (AtomReference atom in inputs) //remove each atom one by one
                    {
                        atom.molecule.RemoveAtom(atom.pos);
                        
                    }
                    silver.molecule.ReplaceAtom(MetalQuintessence.Chromium, silver.pos);
                    silver.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, TransmutationEffectRenderMode.AsEffect, silver.atomType, Assets.textures.atoms.projection_effect, 30f);
                    // Brimstone.API.ChangeAtom(silver, MetalQuintessence.Chromium); //transume quicksilver into chromium with it's effect
                                                                                       // silver.field_2279.field_2276 = new class_168(seb, (enum_7)0, (enum_132)1, silver.AtomType, projectAtomAnimation, 30f);

                }
            }
            return false;
        }
    }
}
