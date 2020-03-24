using Android.Views;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using vacation.droid.Converters;
using vacation.droid.CustomBinding;
using Vacations.Models.Models;

namespace vacation.droid.Activities.ViewHandler
{
    public class VacationsViewHolder : RecyclerViewBindableItemViewHolder<object, VacationModel>
    {
        private readonly View _itemView;
        public VacationsViewHolder(View itemView)
            : base(itemView)
        {
            _itemView = itemView;
        }
       
        public override void Bind(BindingSet<VacationModel> bindingSet)
        {
            base.Bind(bindingSet);
            var image = _itemView.FindViewById<ImageView>(Resource.Id.vacationStatusImage);
            var date = _itemView.FindViewById<TextView>(Resource.Id.dateView);
            var vacationType = _itemView.FindViewById<TextView>(Resource.Id.vacationType);
            var vacationStatus = _itemView.FindViewById<TextView>(Resource.Id.vacationStatus);

            bindingSet.Bind(date).For(v => v.TextBinding()).To(vm => vm.Period);
            bindingSet.Bind(vacationStatus).For(v => v.TextBinding()).To(vm => vm.VacationStatusTitle);
            bindingSet.Bind(vacationType).For(v => v.TextBinding()).To(vm => vm.VacationTypeTitle);
            bindingSet.Bind(image).For(v => v.SetDrawableBinding()).To(vm => vm.VacationType).WithConversion<NumberToImageValueConverter>();
        }
    }
}