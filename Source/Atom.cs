using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalQuintessence
{
    public static class Atom
    {

        public static AtomType Chromium;
        public static void AddAtom()
        {
            Logger.Log("Metal Quintessence - Implementing sanity check");
            Chromium = new AtomType()
            {
                byteId = 25,
                QuintAtomType = MetalQuintessence.Instance.ModId + ":chromium",
                defaultName = Translations.Translate(MetalQuintessence.Instance.ModId + ".atom.chromium"),
                elementalName = Translations.Translate(MetalQuintessence.Instance.ModId + ".atom.chromium.elemental"),
                symbol = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/chromium_symbol"),
                shadow = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/mors_shade"),


                matteTextures = new()
                {
                    diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/chromium_diffuse"),
                    shade = AssetLoaderHelper.LoadTexture("textures/atoms/andytampan/metalquintessence/mors_shade")
                },
                isQuicksilver = true
            };

            QApi.AddAtomType(Chromium);
        }
    }
}
