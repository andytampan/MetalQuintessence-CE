using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static class_103;
using static Quintessential.Serialization.PuzzleModel;

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

        public static Texture chromaticDispersionBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersionBase/pigmentationBase");
        public static Texture chromaticDispersionGlyphBase = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersionBase/glyphBase");
        public static Texture chromaticDispersionIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/chromaDispersion");
        public static Texture chromaticDispersionIconHover = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/icons/chromaDispersion_hover");
        public static Texture chromaticDispersionGlow = AssetLoaderHelper.LoadTexture("textures/select/andytampan/chromaDispersion_glow");
        public static Texture chromaticDispersionStroke = AssetLoaderHelper.LoadTexture("textures/select/andytampan/chromaDispersion_stroke");
        public static Texture chromaticDispersionBond = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/chromaticDispersionBase/chromaDispersionBondLoop");

        public static Texture[] irisAnimation = Assets.textures.parts.iris_full;
        public static Texture chromiumIcon = AssetLoaderHelper.LoadTexture("textures/parts/andytampan/pigmentation/chromiumIcon");

        public static readonly HexIndex pigmentationBowl = new(0, 0);
        public static readonly HexIndex pigmentationA = new(-1, 0);
        public static readonly HexIndex pigmentationB = new(0, -1);
        public static readonly HexIndex pigmentationC = new(0, -2);
        public static readonly HexIndex pigmentationD = new(2, -2);
        public static readonly HexIndex pigmentationE = new(1, -1);
        public static readonly HexIndex pigmentationF = new(1, 0);

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
        public static PartType Pigmentation;

        public static PartType ChromeDispersion;


        public static void AddPartsType()
        {
            Pigmentation = new()
            {
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
                pigmentationF,
                

            },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(MetalQuintessence.Instance.GetIdentifier("pigmentation"))
            };
            QApi.AddPartTypeToPanel(Pigmentation, false);

            MetalQuintessence.Instance.AddPartType(Pigmentation, "pigmentation", static (part, pos, editor, renderer) =>
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

            ChromeDispersion = new()
            {
                cost = 30, // Cost
                isFullHexCover = true, // Is a glyph
                glowTexture= chromaticDispersionGlow, // Shadow/glow
                strokeTexture = chromaticDispersionStroke, // Stroke/outline
                baseTexture = chromaticDispersionIcon, // Panel icon
                hoverTexture = chromaticDispersionIconHover, // Hovered panel icon
                glyphHexes = new HexIndex[]
            {
                chromeDispersionInput,
                chromeDispersionQuicksilver,
                chromeDispersionLead,
                chromeDispersionTin,
                chromeDispersionIron,
                chromeDispersionCopper,
                chromeDispersionSilver,
                chromeDispersionGold,
                chromeDispersionGlyphA,
                chromeDispersionGlyphB

            },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(MetalQuintessence.Instance.GetIdentifier("chromedispersion"))

            };
             MetalQuintessence.Instance.AddPartType(ChromeDispersion, "chromedispersion", static (part, pos, editor, renderer) =>
            {
                Vector2 offset = new(90f, 296f);


                renderer.RenderUpright(chromaticDispersionGlyphBase, chromeDispersionGlyphA, Vector2.Zero);
                renderer.RenderUpright(chromaticDispersionGlyphBase, chromeDispersionGlyphB, Vector2.Zero);
                renderer.RenderBase(chromaticDispersionBase, Vector2.Zero, offset, 0f);
                renderer.RenderUpright(ringhole, chromeDispersionInput, Vector2.Zero);

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

                    renderer.RenderRotating(ringhole, h, Vector2.Zero);
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
            });

            QApi.AddPartTypeToPanel(ChromeDispersion, false);
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
                    if (partType == ChromeDispersion)
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

                        if (first && !simStates[part].isProcessing)
                        {
                            if (sim.FindAtomRelative(part, chromeDispersionInput).GetOrDefault(out AtomReference chromium) && !chromium.inMultiAtomMolecule && !chromium.isHeldByArm && chromium.atomType == Atom.Chromium)
                            {
                                // playSound(sim, MetalQuintessenceSound.chromatic_dispersionSound);
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
                                    seb.consumptionEffects.Add(new ConsumptionEffect(seb, chromium));
                                    simStates[part].isProcessing = true;
                                    foreach (HexIndex h in outputHexes)
                                    {
                                        
                                        sim.additionalCollisions.Add(new Sim.Collider
                                        {
                                            type = 0,
                                            center = HexGrid.standardGrid.ToPixelCoords(part.InFrontBy(h)),
                                            radius = 15f
                                        });
                                    }
                                }
                            }
                        }
                        else if (simStates[part].isProcessing)
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
                                Molecule molecule = new Molecule();
                                molecule.AddAtom(new global::Atom(cardinalAtoms[i]), part.InFrontBy(outputHexes[i]));
                                sim.molecules.Add(molecule);
                            }
                        }
                    }

                }
            });
        }

    }
}
