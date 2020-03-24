using Android.Views;
using Android.Support.V7.Widget;
using vacation.droid.Activities.ViewHandler;
using FlexiMvvm.Collections;
using System;

namespace vacation.droid.Adapters
{
    public class VacationsAdapter : RecyclerViewObservablePlainAdapter
    {
        public EventHandler<int> ItemRemoved { get; set; }
        public VacationsAdapter(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        public void RemoveItem(int position)
        {
            ItemRemoved.Invoke(this, position - 1);
            NotifyDataSetChanged();
            NotifyItemChanged(position);
        }

        protected override RecyclerViewObservableViewHolder OnCreateItemViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.vacation, parent, false);
            return new VacationsViewHolder(itemView);
        }

    }

}