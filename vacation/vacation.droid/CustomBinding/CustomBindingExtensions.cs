using System;
using Android.App;
using Android.Graphics.Drawables;
using Android.Widget;
using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using vacation.droid.Activities;
using vacation.droid.Adapters;

namespace vacation.droid.CustomBinding
{
    public static class CustomBindingExtensions
    {
        public static TargetItemBinding<ImageView, Drawable> SetDrawableBinding(
              this IItemReference<ImageView> imageViewReference)
        {
            return new TargetItemOneWayCustomBinding<ImageView, Drawable>(
                imageViewReference,
                (imageView, progress) => imageView.NotNull().SetImageDrawable(progress),
                () => "SetDrawable");
        }

        public static TargetItemBinding<DetailsScreenActivity, DateTime> SetStartDatePickerDialogTwoWayBinding(this IItemReference<DetailsScreenActivity> target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return new TargetItemTwoWayCustomBinding<DetailsScreenActivity, DateTime>(
                target,
                (t, handler) => t.StartDateTimeChanged += handler,
                (t, handler) => t.StartDateTimeChanged -= handler,
                (t, canExecuteCommand) => { },
                t => t.StartDatePickerTime,
                (t, @value) => t.StartDatePickerTime = @value,
                () => "SetStartDatePickerDialogTwoWay");
        }

        public static TargetItemBinding<DetailsScreenActivity, DateTime> SetEndDatePickerDialogTwoWayBinding(this IItemReference<DetailsScreenActivity> target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return new TargetItemTwoWayCustomBinding<DetailsScreenActivity, DateTime>(
                target,
                (t, handler) => t.EndDateTimeChanged += handler,
                (t, handler) => t.EndDateTimeChanged -= handler,
                (t, canExecuteCommand) => { },
                t => t.EndDatePickerTime,
                (t, @value) => t.EndDatePickerTime = @value,
                () => "SetEndDatePickerDialogTwoWay");
        }

        public static TargetItemBinding<VacationsAdapter, int> SetItemSwipedBinding(this IItemReference<VacationsAdapter> target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return new TargetItemOneWayToSourceCustomBinding<VacationsAdapter, int, int>(
                target,
                (t, eventHandler) => t.ItemRemoved += eventHandler,
                (t, eventHandler) => t.ItemRemoved -= eventHandler,
                (t, canExecuteCommand) => { },
                (t, @value) => @value,
                () => "SetItemSwiped");
        }
    }
}