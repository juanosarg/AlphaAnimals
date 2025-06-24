using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public static class Utils
    {


        public static IEnumerable<Hediff_Injury> GetInjuriesTendable(List<Hediff> hediffs)
        {
            int i = 0;
            while (i < hediffs.Count)
            {
                Hediff_Injury hediff_Injury = hediffs[i] as Hediff_Injury;
                if (hediff_Injury != null && hediff_Injury.TendableNow())
                {
                    yield return hediff_Injury;
                }
                int num = i + 1;
                i = num;
            }
        }

    }
}
