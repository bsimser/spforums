using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using WebPart=Microsoft.SharePoint.WebPartPages.WebPart;

namespace BilSimser.SharePointForums.WebPartCode
{
    [Guid("ec443704-3770-4e25-9da2-749d58a8b089")]
    public class SharePointForumsWebPart : WebPart
    {
        private bool _error = false;
        private string _myProperty = null;


        public SharePointForumsWebPart()
        {
            ExportMode = WebPartExportMode.All;
        }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [Category("My Property Group")]
        [WebDisplayName("MyProperty")]
        [WebDescription("Meaningless Property")]
        public string MyProperty
        {
            get
            {
                if (_myProperty == null)
                {
                    _myProperty = "Version 2.0 (2007) of the forums, coming soon!";
                }
                return _myProperty;
            }
            set { _myProperty = value; }
        }

        /// <summary>
        /// Create all your controls here for rendering.
        /// Try to avoid using the RenderWebPart() method.
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!_error)
            {
                try
                {
                    base.CreateChildControls();

                    // Your code here...
                    Controls.Add(new LiteralControl(MyProperty));
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Ensures that the CreateChildControls() is called before events.
        /// Use CreateChildControls() to create your controls.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!_error)
            {
                try
                {
                    base.OnLoad(e);
                    EnsureChildControls();

                    // Your code here...
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Clear all child controls and add an error message for display.
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            _error = true;
            Controls.Clear();
            Controls.Add(new LiteralControl(ex.Message));
        }
    }
}