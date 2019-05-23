using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.OLE.Interop;
using System;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Editor;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualStudio.ComponentModelHost;

namespace Extension
{
    /// <summary>
    /// Establishes an <see cref="IAdornmentLayer"/> to place the adornment on and exports the <see cref="IWpfTextViewCreationListener"/>
    /// that instantiates the adornment on the event of a <see cref="IWpfTextView"/>'s creation
    /// </summary>
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    internal sealed class GetTypedCharactersTextViewCreationListener : IVsTextViewCreationListener
    {
        // Disable "Field is never assigned to..." and "Field is never used" compiler's warnings. Justification: the field is used by MEF.
#pragma warning disable 649, 169

        /// <summary>
        /// Defines the adornment layer for the adornment. This layer is ordered
        /// after the selection layer in the Z-order
        /// </summary>
        /// //MultiEditCommandFilter is a standard class that inherits from IOleCommandTarget, allowing it to communicate with and receive keyboard input from Visual Studio. 
        /// This allows our extension to capture and provide keystrokes functionality in the editor.
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService = null;

        public object ServiceProvider { get; private set; }

        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
             ITextView textView = AdapterService.GetWpfTextView(textViewAdapter);
            if (textView == null)
                return;
            var adornment = textView.Properties.GetProperty<AdornmentForTimeSpeed>(typeof(AdornmentForTimeSpeed));
            textView.Properties.GetOrCreateSingletonProperty(
          () => new EditCommandFilter(textView, textViewAdapter, adornment));
        }


#pragma warning restore 649, 169

        #region IWpfTextViewCreationListener

        internal sealed class EditCommandFilter : IOleCommandTarget
        {
            private ITextView m_textView;
            internal IOleCommandTarget m_nextTarget;
            AdornmentForTimeSpeed adornmentTimeSpeed;
            internal int typedText { get; set; }

            public EditCommandFilter(ITextView textView, IVsTextView adapter, AdornmentForTimeSpeed adornmentForSpeed)
            {
                m_textView = textView;
                this.adornmentTimeSpeed = adornmentForSpeed;
                adapter.AddCommandFilter(this, out m_nextTarget);
            }

            public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
            {
                return m_nextTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
            }

            public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
            {
                int hr = VSConstants.S_OK;
                char typedText;
                if (GetTypedCharacters(pguidCmdGroup, nCmdID, pvaIn, out typedText))
                {
                    adornmentTimeSpeed.ListenToTextSpeed(typedText++);
                }
                hr = m_nextTarget.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
                return hr;

            }
            public bool GetTypedCharacters(Guid pguidCmdGroup, uint nCmdID, IntPtr pvaIn, out char typedText)
            {
                typedText = char.MinValue;
                if (pguidCmdGroup != VSConstants.VSStd2K || nCmdID != (uint)VSConstants.VSStd2KCmdID.TYPECHAR)
                {
                    return false;
                }
                typedText = (char)(ushort)Marshal.GetObjectForNativeVariant(pvaIn);
                return true;
            }

        }
       
        /// <summary>
        /// Called when a text view having matching roles is created over a text data model having a matching content type.
        /// Instantiates a GetTypedCharacters manager when the textView is created.
        /// </summary>
        /// <param name="textView">The <see cref="IWpfTextView"/> upon which the adornment should be placed</param>
      
        #endregion
    }
}
