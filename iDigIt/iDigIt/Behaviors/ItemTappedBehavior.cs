using System;
using System.Collections.Generic;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.Behaviors
{
    class ItemTappedBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView listView)
        {
            listView.ItemTapped += OnListViewItemTapped;
            base.OnAttachedTo(listView);
        }

        protected override void OnDetachingFrom(ListView listView)
        {
            listView.ItemTapped -= OnListViewItemTapped;
            base.OnDetachingFrom(listView);
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            var plant = (Plant)e.Item;
        }
    }
}
