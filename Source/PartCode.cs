using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static class_103;

namespace MetalQuintessence
{
    internal class PartCode
    {
        public static Texture leadIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersionBase/lead_symbol");
        public static Texture quicksilverIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/pigmentation/quicksilver_symbol");
        public static Texture bowl = Assets.textures.parts.calcinator_bowl;
        public static Texture ringhole = Assets.textures.parts.projection_glyph.quicksilver_input;
        public static Texture pigmentationBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/pigmentation/pigmentationBase");
        public static Texture pigmentationGlow = AssetLoaderHelper.LoadTexture("textures/select/andytampan/pigmentation_glow");
        public static Texture pigmentationStroke = AssetLoaderHelper.LoadTexture("textures/select/andytampan/pigmentation_stroke");
        public static Texture pigmentationIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/pigmentation");
        public static Texture pigmentationIconHover = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/pigmentation_hover");

        public static readonly HexIndex pigmentationBowl = new(0, 0);
        public static readonly HexIndex pigmentationA = new(-1, 0);
        public static readonly HexIndex pigmentationB = new(0, -1);
        public static readonly HexIndex pigmentationC = new(0, -2);
        public static readonly HexIndex pigmentationD = new(2, -2);
        public static readonly HexIndex pigmentationE = new(1, -1);
        public static readonly HexIndex pigmentationF = new(1, 0);
        public static PartType Pigmentation;


        public static void AddPartsType()
        {
            Pigmentation = new()
            {
                id = "metalquintessence-pigmentation", 
                name = Translations.Translate("Glyph of Pigmentation"), 
                description = Translations.Translate("The glyph of pigmentation transmutes each grade of metallic atom into a single chromium atom"), 
                cost = 30, // Cost
                isFullHexCover = true, // Is a glyph
                baseTexture = pigmentationIcon, // Panel icon
                hoverTexture = pigmentationIconHover, // Hovered panel icon
                glowTexture = pigmentationGlow, // Shadow/glow
                strokeTexture = pigmentationStroke, // Stroke/outline

                glyphHexes = new HexIndex[]
            {
                pigmentationBowl,
                pigmentationA,
                pigmentationA,
                pigmentationB,
                pigmentationC,
                pigmentationD,
                pigmentationE,
                pigmentationF

            },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(MetalQuintessence.PigmentationPermission)
            };
            QApi.AddPartTypeToPanel(Pigmentation, false);

            QApi.AddPartType(Pigmentation, static (part, pos, editor, renderer) =>
            {
                // Vector2 offset = new(41f, 48f);
                Vector2 offset = new(130f, 200f);
                renderer.RenderBase(pigmentationBase, Vector2.Zero, offset, 0f);

                HexIndex[] inputHex = new HexIndex[]
                {
                pigmentationBowl,
                pigmentationA,
                pigmentationB,
                pigmentationC,
                pigmentationD,
                pigmentationE,
                pigmentationF

                };
                foreach (HexIndex input in inputHex)
                {
                    renderer.RenderUpright(ringhole, input, Vector2.Zero);
                    renderer.RenderUpright(leadIcon, input, Vector2.Zero);
                }
                renderer.RenderRotating(bowl, pigmentationBowl, Vector2.Zero);
                renderer.RenderUpright(quicksilverIcon, pigmentationBowl, Vector2.Zero);
            });


            QApi.RunAfterCycle((sim, first)
            =>
            {
                var seb = sim.solutionEditor;
                List<Part> allParts = seb.GetSolution().parts;
                var simStates = sim.simulationDict;

                foreach (Part part in allParts) 
                {
                    PartType partType = part.GetType();
                    if (partType == Pigmentation)
                    {
                        HexIndex[] inputHex = new HexIndex[]
                         {

                         pigmentationA,
                         pigmentationB,
                         pigmentationC,
                         pigmentationD,
                         pigmentationE,
                         pigmentationF

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

                        if (sim.FindAtomRelative(part, pigmentationBowl).GetOrDefault(out AtomReference silver) & inputLaid
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
                                if (!metals.Contains(atom.type)) { requirement = false; break; }; //iterate if each metal is contained in the inputlist
                                if (atom.reference.inMultiAtomMolecule || atom.reference.isHeldByArm) { requirement = false; break; }; //iterate if each atom are singular and dropped
                            }
                            if (quicksilver == AtomTypes.quicksilver && requirement) // if requirement is still true, and if the input are quicksilver
                            {
                                
                                // playSound(sim, MetalQuintessenceSound.pigmentationSound);
                                foreach (AtomReference atom in inputs) //remove each atom one by one
                                {
                                    seb.consumptionEffects.Add(new ConsumptionEffect(seb, atom));
                                    atom.molecule.RemoveAtom(atom.pos);
                                    
                                }
                                silver.molecule.ReplaceAtom(Atom.Chromium, silver.pos);  //transume quicksilver into chromium with it's effect
                                silver.atom.transmutationEffect = new TransmutationEffect(seb, TransmutationEffectRenderMode.AsEffect, silver.atomType, Assets.textures.atoms.projection_effect, 7.5f, 7);

                            }
                        }
                    }
                }
            });
        }

    }
}
