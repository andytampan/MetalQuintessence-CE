using Quintessential;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MetalQuintessence
{
    public class API
    {
        public static Dictionary<AtomType, AtomType[]> Blossom = new();
        public static bool ApplyBlossomRule(AtomType input, out AtomType[] output)
        {

            bool ret = Blossom.ContainsKey(input);
            output = ret ? Blossom[input] : default(AtomType[]);
            return ret;
        }

        public static void AddBlossomRule(AtomType input, AtomType[] output)
        {
            if (Blossom.ContainsKey(input))
            {
                Logger.Log("Rule already exist: ");
                throw new Exception("Unable to add rule");
            }

            Blossom.Add(input, output);
        }

    }
}
