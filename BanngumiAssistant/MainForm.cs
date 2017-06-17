using BanngumiAssistant.Controllers;
using BanngumiAssistant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace BanngumiAssistant
{
    public partial class MainForm : Form
    {
        private MainController mMainController;
        private List<SearchResultItem> mLastResult;

        public MainForm()
        {
            InitializeComponent();
            mMainController = new MainController();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void edSearchContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = edSearchContent.Text;
            if (e.KeyChar == 0x0d && !string.IsNullOrWhiteSpace(keyword))
            {
                edSearchContent.Enabled = false;
                Search(keyword);
            }
        }

        #region Action handlers

        private async void Init()
        {
            List<SearchResultItem> resultList = await mMainController.GetIndexAsync();
            UpdateUI(resultList);
        }

        private async void Search(string keyword)
        {
            List<SearchResultItem> resultList = await mMainController.GetResultAsync(keyword);
            UpdateUI(resultList);
        }

        #endregion

        private void UpdateUI(List<SearchResultItem> resultList)
        {
            if (!edSearchContent.Enabled)
            {
                edSearchContent.Enabled = true;
            }

            if (resultList == null)
            {
                MessageBox.Show("获取内容列表失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lvSearchResult.Items.Clear();
            lvSearchResult.BeginUpdate();
            foreach (var result in resultList)
            {
                ListViewItem item = new ListViewItem(result.Title);
                lvSearchResult.Items.Add(item);
            }
            lvSearchResult.EndUpdate();

            mLastResult = resultList;
        }

        #region Menu handlers

        private void menuCopyMagnet_Click(object sender, EventArgs e)
        {
            if (lvSearchResult.Items.Count == 0 || lvSearchResult.SelectedItems.Count == 0)
            {
                return;
            }

            CopyMagnetToClipboard(lvSearchResult.SelectedItems.Cast<ListViewItem>());
        }

        private void menuCopyAllMagnet_Click(object sender, EventArgs e)
        {
            CopyMagnetToClipboard(lvSearchResult.Items.Cast<ListViewItem>());
        }

        private void menuCopyTitle_Click(object sender, EventArgs e)
        {
            if (lvSearchResult.Items.Count == 0 || lvSearchResult.SelectedItems.Count == 0)
            {
                return;
            }

            var firstItem = lvSearchResult.SelectedItems[0];
            Clipboard.SetText(firstItem.Text);
        }

        #endregion

        private void CopyMagnetToClipboard(IEnumerable<ListViewItem> items)
        {
            if (items == null || items.Count() == 0)
            {
                return;
            }

            if (mLastResult == null)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            foreach (ListViewItem item in lvSearchResult.SelectedItems)
            {
                if (item.Index > mLastResult.Count)
                {
                    continue;
                }
                builder.AppendLine(mLastResult[item.Index].MagnetLink);
            }

            Clipboard.SetText(builder.ToString());
        }
    }

}
