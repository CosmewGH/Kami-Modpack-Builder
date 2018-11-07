using System;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public class LabelNoCopy : Label
    {
        private string text;

        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }

                if (text != value)
                {
                    text = value;
                    Refresh();
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }
    }
}