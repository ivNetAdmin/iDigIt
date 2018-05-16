using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iDigIt.Behaviors
{
    public class IntValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = int.TryParse(e.NewTextValue, out int result);
            Entry entry = sender as Entry;
            entry.TextColor = isValid ? Color.Default : Color.LightPink;
        }
    }
}
