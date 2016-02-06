using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using Vocabulary.Messages;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace SJ5000.Behaviors
{

    /// <summary>
    /// This behavior can be attached to a ListBox or ComboBox to 
    /// add keyboard selection
    /// </summary>
    public class DropdownBehavior : Behavior<Selector>
    {
        /// <summary>
        /// Gets or sets the Path used to select the text
        /// </summary>
        public string SelectionMemberPath { get; set; }

        /// <summary>
        /// Attaches to the specified object: subscribe on Dropdownopened event
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            var cb = this.AssociatedObject as ComboBox;
            if (cb != null)
            {
                cb.DropDownOpened += ComboBox_DropDownOpened;
            }
        }

        /// <summary>
        /// subscribe on KeyDown event of ItemsPanelRoot of a ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ComboBox_DropDownOpened(object sender, object e)
        {
            /*
            ComboBox CBox = sender as ComboBox;
            if (CBox.Items.Count == 1)
            {
                string currentValue = CBox.SelectedItem.ToString();

                // To avoid ComboBox closing:
                // Deselect the current Item
                CBox.SelectedItem = null;

                //Get the Values
                CamGetParamValuesMessage Param = await _camService.GetParamValues(CBox.Name);

                // Get the Param and update its values with reflexion
                SJ5000Plus.Models.Param parameter = (SJ5000Plus.Models.Param)GetPropertyValue(CBox.Name);

                foreach (var item in Param.options)
                {
                    parameter.Values.Add(item);
                }

                // Remove the first one (so it isn't repeated)
                parameter.Values.RemoveAt(0);

                // Select the value again
                parameter.currentValue = currentValue;

                parameter.permission = Param.permission;

                // Now set the handler for SelectionChanged
                //CBox.SelectionChanged += DropDownSelectionChanged;               
            }
            */
        }

        /// <summary>
        /// Detaches to the specified object: Unsubscribe on KeyDown event(s)
        /// </summary>
        protected override void OnDetaching()
        {
            var cb = this.AssociatedObject as ComboBox;
            if (cb != null)
            {
                cb.DropDownOpened -= ComboBox_DropDownOpened;
            }
            base.OnDetaching();
        }

        /// <summary>
        /// Helper class used for property path value retrieval
        /// </summary>
        private class BindingEvaluator : FrameworkElement
        {

            public static readonly DependencyProperty TargetProperty =
                DependencyProperty.Register(
                    "Target",
                    typeof(object),
                    typeof(BindingEvaluator), null);

            public object Target
            {
                get { return GetValue(TargetProperty); }
                set { SetValue(TargetProperty, value); }
            }

        }
    }
}