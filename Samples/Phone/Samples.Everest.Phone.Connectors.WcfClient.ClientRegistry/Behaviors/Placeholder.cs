using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interactivity;

namespace Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry.Behaviors
{
    /// <summary>
    /// Placeholder behavior, handy on mobile devices
    /// </summary>
    public class Placeholder : Behavior<TextBox>
    {
        private bool m_hasPlaceholder; 
        private Brush m_textForeground;

        /// <summary>
        /// Gets or sets the text in the placeholder
        /// </summary>
        public String Text
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the brush
        /// </summary>
        public Brush Foreground
        {
            get;
            set;
        }

        /// <summary>
        /// When attached, place the text into the textbox
        /// </summary>
        protected override void OnAttached()
        {
            this.m_textForeground = AssociatedObject.Foreground; 
            base.OnAttached(); 
            
            if (this.Text != null) 
                this.SetPlaceholderText(); 
            AssociatedObject.GotFocus += this.GotFocus; 
            AssociatedObject.LostFocus += this.LostFocus;
        }

        /// <summary>
        /// Object has lost focus, replace with text if none available
        /// </summary>
        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AssociatedObject.Text.Length == 0 && this.Text != null)
                this.SetPlaceholderText();
        }
        /// <summary>
        /// Fired when the underlying textbox gets focus
        /// </summary>
        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.m_hasPlaceholder) 
                this.RemovePlaceholderText();
        }
        /// <summary>
        /// Remove placeholder text
        /// </summary>
        private void RemovePlaceholderText()
        {
            this.AssociatedObject.Foreground = this.m_textForeground; 
            this.AssociatedObject.Text = ""; 
            this.m_hasPlaceholder = false;
        }
        /// <summary>
        /// Set placeholder text
        /// </summary>
        private void SetPlaceholderText()
        {
            this.AssociatedObject.Foreground = this.Foreground; 
            this.AssociatedObject.Text = this.Text; 
            this.m_hasPlaceholder = true;
        }
        /// <summary>
        /// Detach and remove event handlers
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching(); 
            this.AssociatedObject.GotFocus -= this.GotFocus; 
            this.AssociatedObject.LostFocus -= this.LostFocus;
        }
    }
}
