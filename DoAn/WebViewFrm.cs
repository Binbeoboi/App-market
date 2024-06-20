using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2;

namespace DoAn
{
    public partial class WebViewFrm : Form
    {
        private string url;
        public WebViewFrm()
        {
            InitializeComponent();
        }

        public WebViewFrm(string url)
        {
            this.url = url;
            InitializeComponent();
        }
        private async Task Initialized()
        {
            await webView.EnsureCoreWebView2Async(null);
        }

        public async void InitBrowser()
        {
            try
            {
                await Initialized();
                webView.CoreWebView2.Navigate(url);
            }
            catch
            {
                MessageBox.Show("Đã có lỗi khi thực hiện.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void WebViewFrm_Load(object sender, EventArgs e)
        {
            InitBrowser();
        }
    }
}
