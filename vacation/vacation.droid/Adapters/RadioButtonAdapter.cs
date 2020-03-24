using Android.Support.V7.Widget;
using Android.Views;
using FlexiMvvm.Collections;
using vacation.droid.Activities.ViewHandler;

namespace vacation.droid.Adapters
{
    public class RadioButtonAdapter : RecyclerViewObservablePlainAdapter
    {
        public RadioButtonAdapter(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        protected override RecyclerViewObservableViewHolder OnCreateItemViewHolder(ViewGroup parent, int viewType)
        {
            var buttonView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.radiobutton, parent, false);
            return new RadioButtonsViewHolder(buttonView);
        }
    }
}