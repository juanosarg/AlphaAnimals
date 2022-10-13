using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBehavioursAndEvents
{
    [StaticConstructorOnStartup]
    public static class AlphaBehavioursAndEvents
    {
       

        public static bool Settings_Button(this Listing_Standard ls, string label, Rect rect)
        {
           
            bool result = Widgets.ButtonText(rect, label, true, true, true);
           
            ls.Gap((2f));
            return result;
           
        }

       
    }
}
