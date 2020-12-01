using System;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaBehavioursAndEvents
{
    public class Pawn_SwallowWhole : Pawn, IThingHolder
    {

        public ThingOwner innerContainer = null;

        public Pawn_SwallowWhole()
        {
            //Constructor initializes the pawn container
            this.innerContainer = new ThingOwner<Thing>(this, false, LookMode.Deep);
         
        }
        public override void ExposeData()
        {
            //Save all the key variables so they work on game save / load
            base.ExposeData();
            Scribe_Deep.Look<ThingOwner>(ref this.innerContainer, "innerContainer", new object[] { this });
         }
        public new ThingOwner GetDirectlyHeldThings()
        {
            //Not used, included just in case something external calls it           
            return this.innerContainer;
        }

        public new void GetChildHolders(List<IThingHolder> outChildren)
        {
            //Not used, included just in case something external calls it
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        public virtual void EjectContents()
        {
            //Remove ingredients from the pawn container. 
            this.innerContainer?.TryDropAll(this.InteractionCell, base.Map, ThingPlaceMode.Near, null, null);           
        }

        public void DestroyContents()
        {
            //Empties all containers and destroys contents
            
            if (this.innerContainer != null && this.innerContainer.Any)
            {
                this.innerContainer.ClearAndDestroyContents();
            }
        }

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            
            EjectContents();           
            base.Destroy(mode);
        }

        public bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            if (!this.innerContainer.CanAcceptAnyOf(thing, true))
            {
                return false;
            }
            bool flag;
            if (thing.holdingOwner != null)
            {
                thing.holdingOwner.TryTransferToContainer(thing, this.innerContainer, thing.stackCount, true);
                flag = true;
            }
            else
            {
                flag = this.innerContainer.TryAdd(thing, true);
            }
            if (flag)
            {
                
                return true;
            }
            return false;
        }

       


    }
}
