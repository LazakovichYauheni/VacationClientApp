using System;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using Vacations.Models.Models;

namespace vacation.droid.Activities.ViewHandler
{
    public class RadioButtonsViewHolder : RecyclerViewBindableItemViewHolder<object, VacationStatusModel>
    {
        private View _buttonView;
        private RadioButton _radioButton;

        public EventHandler RadioButtonClicked;

        public RadioButtonsViewHolder(View buttonView) : base(buttonView)
        {
            _buttonView = buttonView;
            _radioButton = _buttonView.FindViewById<RadioButton>(Resource.Id.radioButton1);
        }

        public override void Bind(BindingSet<VacationStatusModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(_radioButton).For(v => v.CheckedAndCheckedChangeBinding()).To(vm => vm.IsSelected);
            bindingSet.Bind(_radioButton).For(v => v.TextBinding()).To(vm => vm.Title);
        }
    }
}