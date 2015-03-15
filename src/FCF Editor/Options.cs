using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCF_Editor
{
    public partial class Options : Form
    {
        private Control currentButton;

        public Options()
        {
            InitializeComponent();

            // FOR ARROWS!
            Bitmap arrowsBitmap = new Bitmap(arrowsColor.Image);
            for (int Xcount = 0; Xcount < arrowsBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < arrowsBitmap.Height; Ycount++)
                {
                    arrowsBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.arrows_color);
                }
            } arrowsColor.Image = arrowsBitmap;

            // FOR SCENES!
            Bitmap sceneBitmap = (Bitmap)sceneColor.Image;
            for (int Xcount = 0; Xcount < sceneBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < sceneBitmap.Height; Ycount++)
                {
                    sceneBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.scene_color);
                }
            } 
            sceneColor.Image = sceneBitmap;
            sceneColor.Font = Properties.Settings.Default.scene_font;
            sceneColor.ForeColor = Properties.Settings.Default.scene_fore_color;
            /////////////////////////////////////////////////////////////////////////

            // FOR SELECTERS!
            Bitmap selecterBitmap = (Bitmap)selecterColor.Image;
            for (int Xcount = 0; Xcount < selecterBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < selecterBitmap.Height; Ycount++)
                {
                    selecterBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.selecter_color);
                }
            }
            selecterColor.Image = selecterBitmap;
            selecterColor.Font = Properties.Settings.Default.selecter_font;
            selecterColor.ForeColor = Properties.Settings.Default.selecter_fore_color;
            //////////////////////////////////////////////////////////////////////////////

            // FOR OUTERLABELs
            Bitmap outerlabelBitmap = (Bitmap)outerlabelColor.Image;
            for (int Xcount = 0; Xcount < outerlabelBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < outerlabelBitmap.Height; Ycount++)
                {
                    outerlabelBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.outerlabel_color);
                }
            }
            outerlabelColor.Image = outerlabelBitmap;
            outerlabelColor.Font = Properties.Settings.Default.outerlabel_font;
            outerlabelColor.ForeColor = Properties.Settings.Default.outerlabel_fore_color;
            /////////////////////////////////////////////////////////////////////////////////////

            variable_file_path.Text = Properties.Settings.Default.Variable_filepath;
            folder_path.Text = Properties.Settings.Default.Flowchart_folderpath;
        }

        private void variable_browse_Click(object sender, EventArgs e)
        {
            open_variables_file.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.FileInfo fInfo = new System.IO.FileInfo(open_variables_file.FileName);
            Properties.Settings.Default.Variable_filepath = fInfo.FullName;
            variable_file_path.Text = Properties.Settings.Default.Variable_filepath;
            Properties.Settings.Default.Save();
        }

        private void folder_browse_Click(object sender, EventArgs e)
        {
            fcf_folder_browser.SelectedPath = Properties.Settings.Default.Flowchart_folderpath;
            DialogResult result = fcf_folder_browser.ShowDialog();
            Properties.Settings.Default.Flowchart_folderpath = fcf_folder_browser.SelectedPath;// +'\\';
            folder_path.Text = Properties.Settings.Default.Flowchart_folderpath;
            Properties.Settings.Default.Save();
        }

        private void save_options_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void folder_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Flowchart_folderpath = folder_path.Text;
            Properties.Settings.Default.Save();
        }

        private void variable_file_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Variable_filepath = variable_file_path.Text;
            Properties.Settings.Default.Save();
        }

        private void arrowsColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Properties.Settings.Default.arrows_color;
            DialogResult result = colorDialog.ShowDialog();
            Properties.Settings.Default.arrows_color = colorDialog.Color;

            Bitmap arrowsBitmap = (Bitmap)arrowsColor.Image;
            for (int Xcount = 0; Xcount < arrowsBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < arrowsBitmap.Height; Ycount++)
                {
                    arrowsBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.arrows_color);
                }
            }
            arrowsColor.Image = arrowsBitmap;
            Properties.Settings.Default.Save();
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chooseFontOrColor.SourceControl != null)
            {
                currentButton = chooseFontOrColor.SourceControl;
            }
            if (currentButton == sceneColor)
            {
                fontDialog.Color = Properties.Settings.Default.scene_fore_color;
                fontDialog.Font = Properties.Settings.Default.scene_font;
                DialogResult result = fontDialog.ShowDialog();

                Properties.Settings.Default.scene_font = fontDialog.Font;
                Properties.Settings.Default.scene_fore_color = fontDialog.Color;
                sceneColor.Font = Properties.Settings.Default.scene_font;
                sceneColor.ForeColor = Properties.Settings.Default.scene_fore_color;
            }
            else if (currentButton == selecterColor)
            {
                fontDialog.Color = Properties.Settings.Default.selecter_fore_color;
                fontDialog.Font = Properties.Settings.Default.selecter_font;
                DialogResult result = fontDialog.ShowDialog();

                Properties.Settings.Default.selecter_font = fontDialog.Font;
                Properties.Settings.Default.selecter_fore_color = fontDialog.Color;
                selecterColor.Font = Properties.Settings.Default.selecter_font;
                selecterColor.ForeColor = Properties.Settings.Default.selecter_fore_color;
            }
            else
            {
                fontDialog.Color = Properties.Settings.Default.outerlabel_fore_color;
                fontDialog.Font = Properties.Settings.Default.outerlabel_font;
                DialogResult result = fontDialog.ShowDialog();

                Properties.Settings.Default.outerlabel_font = fontDialog.Font;
                Properties.Settings.Default.outerlabel_fore_color = fontDialog.Color;
                outerlabelColor.Font = Properties.Settings.Default.outerlabel_font;
                outerlabelColor.ForeColor = Properties.Settings.Default.outerlabel_fore_color;
            }

            Properties.Settings.Default.Save();
            currentButton = null;
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chooseFontOrColor.SourceControl != null)
            {
                currentButton = chooseFontOrColor.SourceControl;
            }

            if (currentButton == sceneColor)
            {
                colorDialog.Color = Properties.Settings.Default.scene_color;
                DialogResult result = colorDialog.ShowDialog();
                Properties.Settings.Default.scene_color = colorDialog.Color;

                Bitmap sceneBitmap = (Bitmap)sceneColor.Image;
                for (int Xcount = 0; Xcount < sceneBitmap.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < sceneBitmap.Height; Ycount++)
                    {
                        sceneBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.scene_color);
                    }
                }
                sceneColor.Image = sceneBitmap;
            }
            else if (currentButton == selecterColor)
            {
                colorDialog.Color = Properties.Settings.Default.selecter_color;
                DialogResult result = colorDialog.ShowDialog();
                Properties.Settings.Default.selecter_color = colorDialog.Color;

                Bitmap selecterBitmap = (Bitmap)selecterColor.Image;
                for (int Xcount = 0; Xcount < selecterBitmap.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < selecterBitmap.Height; Ycount++)
                    {
                        selecterBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.selecter_color);
                    }
                }
                selecterColor.Image = selecterBitmap;
            }
            else
            {
                colorDialog.Color = Properties.Settings.Default.outerlabel_color;
                DialogResult result = colorDialog.ShowDialog();
                Properties.Settings.Default.outerlabel_color = colorDialog.Color;

                Bitmap outerlabelBitmap = (Bitmap)outerlabelColor.Image;
                for (int Xcount = 0; Xcount < outerlabelBitmap.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < outerlabelBitmap.Height; Ycount++)
                    {
                       outerlabelBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.outerlabel_color);
                    }
                }
                outerlabelColor.Image = outerlabelBitmap;
            }
            
            Properties.Settings.Default.Save();
            currentButton = null;
        }

        private void sceneColor_Click(object sender, EventArgs e)
        {
            currentButton = sceneColor;
            chooseFontOrColor.Show(Cursor.Position);
        }

        private void selecterColor_Click(object sender, EventArgs e)
        {
            currentButton = selecterColor;
            chooseFontOrColor.Show(Cursor.Position);
        }

        private void outerlabelColor_Click(object sender, EventArgs e)
        {
            currentButton = outerlabelColor;
            chooseFontOrColor.Show(Cursor.Position);
        }



    }
}