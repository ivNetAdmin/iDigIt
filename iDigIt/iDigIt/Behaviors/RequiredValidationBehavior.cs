using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iDigIt.Behaviors
{
    public class RequiredValidationBehavior : Behavior<Entry>
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
            bool isValid = e.NewTextValue == null || e.NewTextValue.Length > 0;
            Entry entry = sender as Entry;
            entry.BackgroundColor = isValid ? Color.Default : Color.LightPink;
        }
    }
}
