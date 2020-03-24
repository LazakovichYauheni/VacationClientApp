using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;

namespace vacation.droid.Adapters
{
    public class SwipeToDeleteCallback : ItemTouchHelper.SimpleCallback
    {
        private Context _context;
        private VacationsAdapter _vacationsAdapter;
        public SwipeToDeleteCallback(int dragDirs, int swipeDirs, Context context) : base(dragDirs, swipeDirs)
        {
            _context = context;
        }

        public SwipeToDeleteCallback(int dragDirs, int swipeDirs, Context context, VacationsAdapter mRecyclerView) : this(dragDirs, swipeDirs, context)
        {
            _context = context;
            _vacationsAdapter = mRecyclerView;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            _vacationsAdapter.RemoveItem(viewHolder.AdapterPosition);
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            throw new NotImplementedException();
        }
    }
}