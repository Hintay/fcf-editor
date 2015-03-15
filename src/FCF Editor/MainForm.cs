using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Arrows;

namespace FCF_Editor
{
    public partial class MainForm : Form
    {
        private Dictionary<int, Labels> collection = new Dictionary<int, Labels>();
        private Dictionary<String, String> variablesList = new Dictionary<String, String>();

        private Labels currentLabel;
        private int previousID;
        private string previousType;
        private Point point;
        private Point mouseDownCoords;

        private string charactersDisallowed = "'";
        private string numbersAllowed = "0123456789";

        private bool isMoving;
        //private Point previousLocation;

        private int margin = 20;
        private string fileName = "";

        private string format = "";
        private string variableData = "";
        private Button resetButton;

        private bool _changesMade = false;

        private Button tempButton;

        public bool ChangesMade
        {
            get { return _changesMade; }
            set
            {
                _changesMade = value;
                if (_changesMade)
                {
                    this.Text += " *";
                }
                else this.Text.Replace(" *", "");
            }
        }

        public MainForm(string[] args)
        {
            InitializeComponent();

            point = new Point(flowLayout.Width / 2, flowLayout.Height / 2);
            this.Text = "Flowchart Editor";

            clearEverything(true);

            if (args.Any()) readFile(args[0]);

            // Start the BackgroundWorker.
            //backgroundWorker1.RunWorkerAsync();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
        }

        private bool clearEverything(bool newFile)
        {
            if (ChangesMade)
            {
                DialogResult dialog = MessageBox.Show("Some changes have been made, which aren't saved yet... Do you want to save?", "Unsaved changes!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dialog == DialogResult.Yes)
                {
                    save(false, false);
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return false;
                }
            }
            this.Text = "Flowchart Editor";
            this.fileName = "";

            //flowLayout.Controls.Remove(FindControlByName("canvas", flowLayout));
            log.Text = "";
            properties_title.Text = "";
            properties_id.Text = "";
            properties_y.Text = "";
            properties_x.Text = "";
            properties_type.Text = "";
            properties_jumpTarget.Text = "";
            properties_file_target.Items.Clear();
            properties_jumpTarget.Items.Clear();
            comment_box.Text = "";
            properties_id.Enabled = false;
            properties_title.Enabled = false;
            properties_type.Enabled = false;
            properties_x.Enabled = false;
            properties_y.Enabled = false;
            properties_jumpTarget.Enabled = false;
            update.Visible = false;
            update.Enabled = false;
            removeButton.Visible = false;
            removeButton.Enabled = false;
            resetButton.Visible = false;
            resetButton.Enabled = false;
            outerlabel_browse.Visible = false;
            properties_title_label.Text = "Title:";
            properties_jumpTarget.Visible = true;

            collection.Clear();
            flowLayout.Controls.Clear();
            alternatives.Visible = false;
            checkFlags.Visible = false;
            flagOperations.Visible = false;
            properties_file_target.Visible = false;

            currentLabel = null;

            properties_jumpTarget_label.Text = "Default Jump Target:";
            flowLayout.Refresh();

            if (newFile)
            {
                format = "";
                string variableFile = Properties.Settings.Default.Variable_filepath;
                variableData = "";
                # region formatsList
                ComboBox formatsList = new ComboBox();
                formatsList.SelectionChangeCommitted += formatsList_SelectionChangeCommitted;
                formatsList.Width = 100;
                formatsList.Top = 80;
                formatsList.Left = 150;
                formatsList.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                # endregion

                if (variableFile == "" || !System.IO.File.Exists(variableFile))
                {
                    variableData = Properties.Resources.variables;
                }

                if (variableData != "" || (variableFile != "" && System.IO.File.Exists(variableFile)))
                {
                    string[] readlines;
                    if (variableData != "") readlines = variableData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    else readlines = System.IO.File.ReadAllLines(variableFile);
                    foreach (var line in readlines)
                    {
                        if (line.Contains("==>"))
                        {
                            formatsList.Items.Add(line.Remove(0, 3));
                        }
                    }
                    if (formatsList.Items.Count > 1)
                    {
                        string source = (variableData != "") ? "from the default variable data" : "from your specified variable file";
                        # region choose_button
                        Button choose_button = new Button();
                        choose_button.Left = 150;
                        choose_button.Width = 100;
                        choose_button.Height = 30;
                        choose_button.Top = 115;
                        choose_button.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        choose_button.Text = "CHOOSE!";
                        choose_button.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
                        choose_button.Click += choose_button_Click;
                        choose_button.Enabled = false;
                        # endregion

                        tempButton = choose_button;

                        # region choose_format
                        Form choose_format = new Form();
                        choose_format.Size = new System.Drawing.Size(400, 200);
                        choose_format.MaximumSize = choose_format.Size;
                        choose_format.MinimumSize = choose_format.Size;
                        choose_format.MaximizeBox = false;
                        choose_format.MinimizeBox = false;
                        choose_format.Text = "Choose your FCF file format!";
                        choose_format.FormClosing += choose_format_FormClosing;
                        choose_format.ControlBox = false;
                        choose_format.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
                        choose_format.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
                        choose_format.StartPosition = FormStartPosition.CenterScreen;
                        choose_format.AcceptButton = choose_button;
                        # endregion
                        # region info
                        Label info = new Label();
                        info.Top = 10;
                        info.TextAlign = ContentAlignment.MiddleCenter;
                        //info.Left = 10;
                        info.Height = 60;
                        info.Width = choose_format.Width - 10;
                        info.Text = formatsList.Items.Count + " FCF formats have been found with their respective \nvariables' data " + source + "...\n Which one do you want to work on?";
                        # endregion

                        choose_format.Controls.Add(info);
                        choose_format.Controls.Add(formatsList);
                        choose_format.Controls.Add(choose_button);

                        choose_format.ShowDialog();
                    }
                    else
                    {
                        if (formatsList.Items.Count == 1) format = formatsList.Items[0].ToString();
                        else format = "FSN";
                    }
                }
            }

            variables_list();
            debug.Text = "Currently editing " + format.ToUpper() + " flowchart format.";

            ChangesMade = false;
            return true;
        }

        private bool save(bool saveAs, bool quit)
        {
            if (log.Text == "")
            {
                MessageBox.Show("Atleast add something in the flowchart before saving!\nOr, do you just want to create an empty flowchart file? If that's the case, then just create a plain text file and change it's extension to .fcf. Easy, right?", "What do you wish to save?");
                return false;
            }
            if (update.Enabled)
            {
                MessageBox.Show("Update the flow element properly before trying to save!", "Incomplete data!");
                return false;
            }
            if (!saveAs)
            {
                if (System.IO.File.Exists(fileName))
                {
                    UnicodeEncoding unicode = new UnicodeEncoding();
                    string comment = (comment_box.Text == "") ? comment = "COMMENT!" : comment = comment_box.Text;
                    System.IO.File.WriteAllText(fileName, comment + Environment.NewLine + log.Text, unicode);
                }
                else saveFile.ShowDialog();
            }
            else saveFile.ShowDialog();

            if (!quit)
            {
                ChangesMade = false;
                string filename = this.fileName;
                if (clearEverything(false)) readFile(filename);
            }
            return true;
        }

        void choose_button_Click(object sender, EventArgs e)
        {
            ((Button)sender).FindForm().Close();
        }

        void formatsList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            format = ((ComboBox)sender).SelectedItem.ToString();
            if (format != "") { tempButton.Text = "DONE!"; tempButton.Enabled = true; }
        }

        void choose_format_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (format == "")
            {
                MessageBox.Show("Select the desired .fcf format!", "No way to go nowhere!");
                e.Cancel = true;
            }
        }

        private void openFile_FileOk(object sender, CancelEventArgs e)
        {
            // Open document 
            string filename = openFile.FileName;
            if (clearEverything(true)) readFile(filename);
        }

        private void readFile(string filename)
        {
            this.fileName = filename;
            string[] readlines = System.IO.File.ReadAllLines(fileName);
            if (!readlines.Any())
            {
                MessageBox.Show("There's not a single character in the file... How can this stupid program read your mind and output what you want to show...?\nIf you want to create a flowchart anew, then just go to Edit->Add and start adding flow elements... Easy, right?", "Empty file!");
                return;
            }
            this.Text = "Flowchart Editor ( " + filename + " )";
            comment_box.Text = readlines[0];
            for (var line = 1; line < readlines.Length; line++)
            {
                if (line == (readlines.Length - 1)) log.Text += readlines[line];
                else log.Text += readlines[line] + Environment.NewLine;
                string[] pm = readlines[line].Split(';');
                pm[1] = pm[1].Replace("\\'", "`"); // For FHA \' compatibility!
                pm = pm[1].Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);

                int l = int.Parse(pm[2]); // LEFT
                int t = int.Parse(pm[3]); // TOP
                int r = int.Parse(pm[4]); // RIGHT
                int b = int.Parse(pm[5]); // BOTTOM

                //Use center-based coordinates because the size of the element is different, we want it to stay aligned relative to its center.
                var cp = centerPoint(l, t, r - l, b - t);

                switch (pm[0])
                {
                    case "SCENE":
                        int njumps = int.Parse(pm[11]);

                        List<String[]> jumps = new List<String[]>();

                        int j;
                        for (j = 0; j < njumps; j++)
                        {
                            string[] jumpSplit = pm[12 + j].Split(':');
                            int nDecisions = int.Parse(jumpSplit[0]);
                            if (nDecisions != 0)
                            {
                                for (int decision = 0; decision < nDecisions; decision++)
                                {
                                    string[] decisionSplit = jumpSplit[decision + 1].Split(new string[] { "//" }, StringSplitOptions.None);
                                    string flagName = decisionSplit[0];
                                    string decisionType = decisionSplit[1];
                                    string arg = decisionSplit[2];

                                    string linkType = jumpSplit[nDecisions + 1];
                                    string target = jumpSplit[nDecisions + 2];

                                    jumps.Add(new string[] { target, linkType, flagName, decisionType, arg });
                                }
                            }
                            else
                            {
                                //string id = pm[1];
                                string linkType = jumpSplit[1];
                                string target = jumpSplit[2];
                                jumps.Add(new string[] { target, linkType });
                            }
                        }
                        string[] flagOperation = pm[pm.Length - 1].Split(':');
                        int nflagOperations = int.Parse(flagOperation[0]);
                        List<String[]> flagOperations = new List<String[]>();
                        if (nflagOperations != 0)
                        {
                            for (int i = 1; i == nflagOperations; i++)
                            {
                                string[] flag = flagOperation[i].Split(new string[] { "//" }, StringSplitOptions.None);
                                string flagName = flag[0];
                                string arg = flag[1];
                                string operationType = flag[2];

                                flagOperations.Add(new string[] { flagName, operationType, arg });
                            }

                        }

                        Labels scene = new Labels(pm[0], int.Parse(pm[1]), pm[10], int.Parse(pm[2]), int.Parse(pm[3]), int.Parse(pm[4]), int.Parse(pm[5]), point, jumps, flagOperations);

                        TextBox sceneButton = scene.drawSceneButton();

                        setLocationFromCenterPoint(sceneButton, cp);

                        sceneButton.MouseDown += box_MouseDown;
                        sceneButton.MouseUp += box_MouseUp;
                        sceneButton.MouseMove += box_MouseMove;
                        sceneButton.Click += new EventHandler(box_Click);
                        sceneButton.KeyDown += Button_KeyDown;

                        collection.Add(int.Parse(pm[1]), scene);
                        flowLayout.Controls.Add(sceneButton);
                        break;

                    case "SELECTER":
                        List<String[]> alts = new List<String[]>();
                        List<String[]> check = new List<String[]>();
                        int nAlts = int.Parse(pm[11]);
                        for (int i = 0; i < nAlts; i++)
                        {
                            string target = pm[12 + (i * 3)];
                            string text = pm[12 + (i * 3) + 2];
                            if (format == "FHA")
                            {
                                if (text.Contains("//") && text.Contains(":"))
                                {
                                    string[] checkSplit = text.Split(':');
                                    int nDecisions = int.Parse(checkSplit[1]);
                                    for (int decision = 0; decision < nDecisions; decision++)
                                    {
                                        string[] decisionSplit = checkSplit[decision + 2].Split(new string[] { "//" }, StringSplitOptions.None);
                                        string flagName = decisionSplit[0];
                                        string decisionType = decisionSplit[1];
                                        string arg = decisionSplit[2];

                                        string linkType = checkSplit[nDecisions + 2];

                                        check.Add(new string[] { target, linkType, flagName, decisionType, arg });
                                    }
                                    text = checkSplit[0];
                                }
                            }
                            alts.Add(new String[] { text, target });
                        }
                        Labels selecter;
                        if (format == "FHA") selecter = new Labels(pm[0], int.Parse(pm[1]), pm[10], int.Parse(pm[2]), int.Parse(pm[3]), int.Parse(pm[4]), int.Parse(pm[5]), alts, check);
                        else selecter = new Labels(pm[0], int.Parse(pm[1]), pm[10], int.Parse(pm[2]), int.Parse(pm[3]), int.Parse(pm[4]), int.Parse(pm[5]), point, alts);
                        TextBox selecterButton = selecter.drawSelecterButton();

                        setLocationFromCenterPoint(selecterButton, cp);

                        selecterButton.Click += new EventHandler(box_Click);
                        selecterButton.MouseDown += box_MouseDown;
                        selecterButton.MouseUp += box_MouseUp;
                        selecterButton.MouseMove += box_MouseMove;
                        selecterButton.KeyDown += Button_KeyDown;

                        collection.Add(int.Parse(pm[1]), selecter);
                        flowLayout.Controls.Add(selecterButton);
                        break;
                    case "OUTERLABEL":
                        Labels outerlabel = new Labels(pm[0], int.Parse(pm[1]), int.Parse(pm[2]), int.Parse(pm[3]), int.Parse(pm[4]), int.Parse(pm[5]), point, pm[10], int.Parse(pm[11]));
                        TextBox outerlabel_Button = outerlabel.drawOuterLabel();

                        setLocationFromCenterPoint(outerlabel_Button, cp);

                        outerlabel_Button.Click += new EventHandler(box_Click);
                        outerlabel_Button.MouseDown += box_MouseDown;
                        outerlabel_Button.MouseUp += box_MouseUp;
                        outerlabel_Button.MouseMove += box_MouseMove;
                        outerlabel_Button.KeyDown += Button_KeyDown;

                        collection.Add(int.Parse(pm[1]), outerlabel);
                        flowLayout.Controls.Add(outerlabel_Button);
                        break;
                }
            }

            update_jumpsList();

            List<int> xNegative = new List<int>();
            List<int> yNegative = new List<int>();
            foreach (var item in collection)
            {
                Control control = FindControlByName(item.Value.type + "_" + item.Key, flowLayout);
                //control.Location.Offset((flowLayout.Width) / 2, (flowLayout.Height) / 2);
                if (control.Left <= 0)
                {
                    xNegative.Add(control.Left);
                }
                if (control.Top <= 0)
                {
                    yNegative.Add(control.Top);
                }
            }

            if (xNegative.Count > 0 || yNegative.Count > 0)
            {
                foreach (var item in collection)
                {
                    Control control = FindControlByName(item.Value.type + "_" + item.Key, flowLayout);
                    if (xNegative.Count > 0) control.Left -= xNegative.Min() - margin;
                    if (yNegative.Count > 0) control.Top -= yNegative.Min() - margin;
                }
            }
            flowLayout.Refresh();
        }

        private void update_jumpsList()
        {
            properties_jumpTarget.Items.Clear();
            foreach (var item in collection)
            {
                properties_jumpTarget.Items.Add(item.Key);
            }

        }

        private void setLocationFromCenterPoint(Control ctrl, Point cp)
        {
            ctrl.Location = new Point(cp.X - ctrl.Width/2, cp.Y - ctrl.Height/2);
        }

        private Point centerPoint(int x, int y, int w, int h)
        {
            Point cp = new Point(x + (w / 2), y + (h / 2));
            return cp;
        }

        private void flowLayout_Paint(object sender, PaintEventArgs e)
        {
            foreach (var data in collection)
            {
                Control control = FindControlByName(data.Value.type + "_" + data.Key, flowLayout);
                // Set the starting and ending coordinates for the line.
                switch (data.Value.type)
                {
                    case "SELECTER":
                        if (data.Value.alts.Count >= 1)
                        {
                            for (int i = 0; i < data.Value.alts.Count; i++)
                            {
                                Graphics g = e.Graphics;
                                Pen pen = new Pen(Properties.Settings.Default.arrows_color);
                                Brush brush = new SolidBrush(Properties.Settings.Default.arrows_color);
                                ArrowRenderer arrowRenderer = new ArrowRenderer(15, (float)Math.PI / 6, true);
                                arrowRenderer.SetThetaInDegrees(90);

                                if (data.Value.alts[i][1] != "")
                                {
                                    int targetID = int.Parse(data.Value.alts[i][1]);
                                    if (collection.Keys.Contains(targetID))
                                    {
                                        Control targetControl = FindControlByName(collection[targetID].type + "_" + collection[targetID].id, flowLayout);

                                        if (targetControl != null)
                                        {
                                            PointF p1 = new PointF(control.Left + (control.Width / 2), control.Top + control.Height);
                                            PointF p2 = new PointF(targetControl.Left + (targetControl.Width / 2), targetControl.Top);
                                            PointF p1c = new PointF(p1.X + ((p2.X - p1.X) / 4), p1.Y + ((p2.Y - p1.Y) / 4));
                                            PointF p2c = new PointF(p1.X + ((p2.X - p1.X) / 2), p1.Y + ((p2.Y - p1.Y) / 4));
                                            arrowRenderer.DrawArrowOnCurve(g, pen, brush, p1, p2, p1c, p2c);
                                        }
                                    }
                                }
                            }
                        }
                        control.BackColor = Properties.Settings.Default.selecter_color;
                        control.ForeColor = Properties.Settings.Default.selecter_fore_color;
                        control.Font = Properties.Settings.Default.selecter_font;
                        break;
                    case "SCENE":
                        if (data.Value.jumps.Count >= 1)
                        {
                            for (int i = 0; i < data.Value.jumps.Count; i++)
                            {
                                Graphics g = e.Graphics;
                                Pen pen = new Pen(Properties.Settings.Default.arrows_color);
                                Brush brush = new SolidBrush(Properties.Settings.Default.arrows_color);
                                ArrowRenderer arrowRenderer = new ArrowRenderer(15, (float)Math.PI / 6, true);
                                arrowRenderer.SetThetaInDegrees(90);

                                if (data.Value.jumps[i][0] != "")
                                {
                                    int targetID = int.Parse(data.Value.jumps[i][0]);
                                    if (collection.Keys.Contains(targetID))
                                    {
                                        Control targetControl = FindControlByName(collection[targetID].type + "_" + collection[targetID].id, flowLayout);

                                        if (targetControl != null)
                                        {
                                            PointF p1 = new PointF(control.Left + (control.Width / 2), control.Top + control.Height);
                                            PointF p2 = new PointF(targetControl.Left + (targetControl.Width / 2), targetControl.Top);
                                            PointF p1c = new PointF(p1.X + ((p2.X - p1.X) / 4), p1.Y + ((p2.Y - p1.Y) / 4));
                                            PointF p2c = new PointF(p1.X + ((p2.X - p1.X) / 2), p1.Y + ((p2.Y - p1.Y) / 4));
                                            arrowRenderer.DrawArrowOnCurve(g, pen, brush, p1, p2, p1c, p2c);
                                        }
                                    }
                                }
                                //arrowRenderer.DrawArrow(g, pen, brush, control.Left + (control.Width / 2), control.Top + control.Height, targetControl.Left + (targetControl.Width / 2), targetControl.Top);
                            }
                        }
                        control.BackColor = Properties.Settings.Default.scene_color;
                        control.ForeColor = Properties.Settings.Default.scene_fore_color;
                        control.Font = Properties.Settings.Default.scene_font;
                        break;

                    case "OUTERLABEL":
                        control.BackColor = Properties.Settings.Default.outerlabel_color;
                        control.ForeColor = Properties.Settings.Default.outerlabel_fore_color;
                        control.Font = Properties.Settings.Default.outerlabel_font;
                        break;
                }
            }
        }

        void box_MouseDown(object sender, MouseEventArgs e)
        {
            if (update.Enabled)
            {
                MessageBox.Show("Update the selected flow element before attempting to choose another flow element!", "Update!");
                return;
            }
            //activeControl = sender as Control;
            //previousLocation = e.Location;
            foreach (var label in collection)
            {
                if (((TextBox)sender).Name == (label.Value.type + "_" + label.Key))
                {
                    currentLabel = new Labels(label.Value); // label.Value.Clone() as Labels;
                    previousID = currentLabel.id;
                    previousType = currentLabel.type;
                    mouseDownCoords.X = e.Location.X;
                    mouseDownCoords.Y = e.Location.Y;
                    break;
                }
            }
            isMoving = true;
            Cursor = Cursors.SizeAll;
        }
        void box_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMoving) return;
            //if (activeControl == null || activeControl != sender)
            //    return;
            //var location = activeControl.Location;
            //location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            //activeControl.Location = location;
            ((TextBox)sender).SelectionLength = 0;

            MoveControl((TextBox)sender, new Point(e.Location.X - mouseDownCoords.X, e.Location.Y - mouseDownCoords.Y));

            //debug.Text = ((TextBox)sender).Left + "|" + flowLayout.Width;
            //debug.Text = "X: " + ((TextBox)sender).Left + ", Y: " + ((TextBox)sender).Top + " | " + flowLayout.VerticalScroll.Value;
            if (((TextBox)sender).Left > flowLayout.Width || flowLayout.HorizontalScroll.Value > 0)
            {
                currentLabel.left = ((TextBox)sender).Left + (flowLayout.HorizontalScroll.Maximum - flowLayout.Width);
            }
            else currentLabel.left = ((TextBox)sender).Left;
            if (((TextBox)sender).Top > flowLayout.Height || flowLayout.VerticalScroll.Value > 0)
            {
                currentLabel.top = ((TextBox)sender).Top + (flowLayout.VerticalScroll.Maximum - flowLayout.Height);
            }
            else currentLabel.top = ((TextBox)sender).Top;
            currentLabel.right = currentLabel.left + 30;
            currentLabel.bottom = currentLabel.top + 16;
            if (currentLabel is Labels) update_position();//update_line();
            //edit_properties((TextBox)sender);

            flowLayout.Refresh();
        }

        void box_MouseUp(object sender, MouseEventArgs e)
        {
            //if (isMoving && allowMoving)
            //{
            //update.Enabled = true;
            //int top = ((TextBox)sender).Top;
            //if (top >= flowLayout.Height) top += flowLayout.VerticalScroll.Value;
            //flowLayout.Refresh();
            //}
            isMoving = false;
            Cursor = Cursors.Default;
        }

        private void update_position()
        {
            string[] lines = log.Lines;
            if (lines.Length != 0)
            {
                for (int line = 0; line < lines.Length; line++)
                {
                    if (lines[line] == "") break;
                    string[] lineSplit = lines[line].Split(';');
                    string[] lineData = lineSplit[1].Split(new string[] { "'" }, StringSplitOptions.None);
                    if (int.Parse(lineData[1]) == previousID && lineData[0] == previousType) //if (int.Parse(lineData[1]) == currentLabel.id && lineData[0] == currentLabel.type)
                    {
                        lineData[2] = currentLabel.left.ToString();
                        lineData[3] = currentLabel.top.ToString();
                        lineData[4] = currentLabel.right.ToString();
                        lineData[5] = currentLabel.bottom.ToString();

                        lineSplit[1] = String.Join("'", lineData);
                        string lineString = String.Join(";", lineSplit);

                        lines[line] = lineString;
                        log.Lines = lines;
                        break;
                    }
                    else
                    {
                        string[] newLines = new string[lines.Length + 1];
                        string[] newString = new string[] { form_string(currentLabel) };

                        lines.CopyTo(newLines, 0);
                        newString.CopyTo(newLines, lines.Length);
                        log.Lines = newLines;
                    }
                }
            }
            else
            {
                string[] newLines = new string[lines.Length + 1];
                string[] newString = new string[] { form_string(currentLabel) };

                lines.CopyTo(newLines, 0);
                newString.CopyTo(newLines, lines.Length);
                log.Lines = newLines;
            }
            if (ChangesMade != true) ChangesMade = true;
        }

        private void box_Click(object sender, EventArgs e)
        {
            if (update.Enabled)
            {
                MessageBox.Show("Update the selected flow element before attempting to choose another flow element!", "Update!");
                return;
            }
            update.Visible = false;
            removeButton.Visible = false;
            resetButton.Visible = false;
            foreach (var label in collection)
            {
                if (((TextBox)sender).Name == (label.Value.type + "_" + label.Key))
                {
                    currentLabel = new Labels(label.Value); // label.Value.Clone() as Labels;

                    previousID = currentLabel.id;
                    previousType = currentLabel.type;

                    //debug.Text = "X: " + ((TextBox)sender).Left + ", Y: " + ((TextBox)sender).Top + " | " + flowLayout.VerticalScroll.Value;
                    if (((TextBox)sender).Left > flowLayout.Width || flowLayout.HorizontalScroll.Value > 0)
                    {
                        currentLabel.left = ((TextBox)sender).Left + (flowLayout.HorizontalScroll.Maximum - flowLayout.Width);
                    }
                    else currentLabel.left = ((TextBox)sender).Left;
                    if (((TextBox)sender).Top > flowLayout.Height || flowLayout.VerticalScroll.Value > 0)
                    {
                        currentLabel.top = ((TextBox)sender).Top + (flowLayout.VerticalScroll.Maximum - flowLayout.Height);
                    }
                    else currentLabel.top = ((TextBox)sender).Top;
                    currentLabel.right = currentLabel.left + 30;
                    currentLabel.bottom = currentLabel.top + 16;
                    update_position();

                    edit_properties((TextBox)sender);
                    ((TextBox)sender).Focus();

                    break;
                }
            }
        }

        private void edit_properties(TextBox label)
        {
            change_properties(currentLabel);

            Control control = FindControlByName(currentLabel.type + "_" + currentLabel.id, flowLayout);
            properties_id.Text = "" + currentLabel.id;
            properties_type.SelectedItem = currentLabel.type;
            properties_title.Text = currentLabel.title;
            properties_x.Text = "" + label.Left;
            properties_y.Text = "" + label.Top;

            switch (currentLabel.type)
            {
                case "SCENE":
                    for (int i = 0; i < currentLabel.jumps.Count; i++)
                    {
                        if (currentLabel.jumps[i].Length > 2)
                        {
                            add_Check_Tabs();
                            //foreach (TabPage tab in properties_check_tabs.TabPages){
                            ComboBox flagName = properties_check_tabs.TabPages[i].Controls.Find("check_tab_flagName_" + i, true).First() as ComboBox;
                            if (variablesList.Any())
                            {
                                foreach (var variable in variablesList)
                                {
                                    if (variable.Key == currentLabel.jumps[i][2])
                                    {
                                        flagName.SelectedItem = currentLabel.jumps[i][2] + " (" + variable.Value + ")";
                                        break;
                                    }
                                    else flagName.Text = currentLabel.jumps[i][2];
                                }
                            }
                            else flagName.Text = currentLabel.jumps[i][2];
                            ComboBox decisionType = properties_check_tabs.TabPages[i].Controls.Find("check_tab_decisionType_" + i, true).First() as ComboBox;
                            decisionType.SelectedIndex = int.Parse(currentLabel.jumps[i][3]);
                            TextBox value = properties_check_tabs.TabPages[i].Controls.Find("check_tab_value_" + i, true).First() as TextBox;
                            value.Text = currentLabel.jumps[i][4];

                            ComboBox jumpTarget = properties_check_tabs.TabPages[i].Controls.Find("check_tab_jumpTarget_" + i, true).First() as ComboBox;
                            if (currentLabel.jumps[i][0] != "") jumpTarget.SelectedItem = int.Parse(currentLabel.jumps[i][0]);

                            ComboBox linkType = properties_check_tabs.TabPages[i].Controls.Find("check_tab_linkType_" + i, true).First() as ComboBox;
                            linkType.SelectedIndex = int.Parse(currentLabel.jumps[i][1]) - 1;
                            //}
                        }
                        else
                        {
                            properties_jumpTarget.SelectedItem = int.Parse(currentLabel.jumps[i][0]);
                        }
                    }
                    if (currentLabel.flagOperations.Count != 0)
                    {
                        for (int i = 0; i < currentLabel.flagOperations.Count; i++)
                        {
                            add_Operation_Tabs();
                            ComboBox flagName = properties_operation_tabs.TabPages[i].Controls.Find("operation_tab_flagName_" + i, true).First() as ComboBox;
                            if (variablesList.Any())
                            {
                                foreach (var variable in variablesList)
                                {
                                    if (variable.Key == currentLabel.flagOperations[i][0])
                                    {
                                        flagName.SelectedItem = currentLabel.flagOperations[i][0] + " (" + variable.Value + ")";
                                        break;
                                    }
                                    else flagName.Text = currentLabel.flagOperations[i][0];
                                }
                            }
                            else flagName.Text = currentLabel.flagOperations[i][0];
                            ComboBox operationType = properties_operation_tabs.TabPages[i].Controls.Find("operation_tab_operationType_" + i, true).First() as ComboBox;
                            operationType.SelectedIndex = int.Parse(currentLabel.flagOperations[i][1]) - 1;

                            TextBox value = properties_operation_tabs.TabPages[i].Controls.Find("operation_tab_value_" + i, true).First() as TextBox;
                            value.Text = currentLabel.flagOperations[i][2];
                        }
                    }
                    break;

                case "SELECTER":
                    for (int i = 0; i < currentLabel.alts.Count; i++)
                    {
                        add_Alt_tabs();
                        TextBox choice = properties_alternative_tabs.TabPages[i].Controls.Find("alt_tab_choice_value_" + i, true).First() as TextBox;
                        choice.Text = currentLabel.alts[i][0];
                        ComboBox target = properties_alternative_tabs.TabPages[i].Controls.Find("alt_tab_target_value_" + i, true).First() as ComboBox;
                        if (currentLabel.alts[i][1] != "") target.SelectedItem = int.Parse(currentLabel.alts[i][1]);
                    }
                    if (format == "FHA")
                    {
                        for (int j = 0; j < currentLabel.jumps.Count; j++)
                        {
                            add_Check_Tabs();
                            ComboBox flagName = properties_check_tabs.TabPages[j].Controls.Find("check_tab_flagName_" + j, true).First() as ComboBox;
                            if (variablesList.Any())
                            {
                                foreach (var variable in variablesList)
                                {
                                    if (variable.Key == currentLabel.jumps[j][2])
                                    {
                                        flagName.SelectedItem = currentLabel.jumps[j][2] + " (" + variable.Value + ")";
                                        break;
                                    }
                                    else flagName.Text = currentLabel.jumps[j][2];
                                }
                            }
                            else flagName.Text = currentLabel.jumps[j][2];
                            ComboBox decisionType = properties_check_tabs.TabPages[j].Controls.Find("check_tab_decisionType_" + j, true).First() as ComboBox;
                            decisionType.SelectedIndex = int.Parse(currentLabel.jumps[j][3]);
                            TextBox value = properties_check_tabs.TabPages[j].Controls.Find("check_tab_value_" + j, true).First() as TextBox;
                            value.Text = currentLabel.jumps[j][4];

                            ComboBox jumpTarget = properties_check_tabs.TabPages[j].Controls.Find("check_tab_jumpTarget_" + j, true).First() as ComboBox;
                            if (currentLabel.jumps[j][0] != "") jumpTarget.SelectedItem = int.Parse(currentLabel.jumps[j][0]);

                            ComboBox linkType = properties_check_tabs.TabPages[j].Controls.Find("check_tab_linkType_" + j, true).First() as ComboBox;
                            linkType.SelectedIndex = int.Parse(currentLabel.jumps[j][1]) - 1;
                        }
                    }
                    break;

                case "OUTERLABEL":
                    properties_title.Text = currentLabel.file;
                    properties_title_OuterLabel();
                    properties_file_target.SelectedItem = currentLabel.target;
                    properties_file_target.Text = currentLabel.target.ToString();
                    //properties_jumpTarget.SelectedItem = currentLabel.target;
                    break;
            }
            update.Enabled = false;
            removeButton.Enabled = true;
            removeButton.Visible = true;
            resetButton.Visible = true;
            resetButton.Enabled = true;
            update.Visible = true;
        }
        //}
        //}

        private void change_properties(Labels label)
        {
            clearProperties();
            currentLabel = new Labels(label); //label.Clone() as Labels;
            properties_id.Enabled = true;
            properties_title.Enabled = true;
            properties_type.Enabled = true;
            properties_x.Enabled = true;
            properties_y.Enabled = true;
            properties_jumpTarget.Enabled = true;
            foreach (TabPage tab in properties_check_tabs.TabPages)
                properties_check_tabs.TabPages.Remove(tab);
            foreach (TabPage tab in properties_operation_tabs.TabPages)
                properties_operation_tabs.TabPages.Remove(tab);
            foreach (TabPage tab in properties_alternative_tabs.TabPages)
                properties_alternative_tabs.TabPages.Remove(tab);
            switch (label.type)
            {
                case "SCENE":
                    if (properties_title.Left > 62)
                    {
                        properties_title.Left -= 15;
                        properties_title.Width += 20;
                    }
                    properties_jumpTarget_label.Visible = true;
                    properties_jumpTarget_label.Text = "Default Jump Target:";
                    properties_title_label.Text = "Title: ";
                    checkFlags.Visible = true;
                    flagOperations.Visible = true;
                    properties_jumpTarget.Visible = true;
                    checkFlags.Top = 198;

                    alternatives.Visible = false;
                    properties_file_target.Visible = false;
                    outerlabel_browse.Visible = false;
                    break;

                case "SELECTER":
                    if (properties_title.Left > 62)
                    {
                        properties_title.Left -= 15;
                        properties_title.Width += 20;
                    }
                    properties_jumpTarget_label.Visible = false;
                    properties_jumpTarget.Visible = false;
                    properties_title_label.Text = "Title: ";
                    alternatives.Visible = true;
                    checkFlags.Top = 198;

                    checkFlags.Visible = false;
                    flagOperations.Visible = false;
                    properties_file_target.Visible = false;
                    outerlabel_browse.Visible = false;

                    if (format == "FHA")
                    {
                        checkFlags.Visible = true;
                        checkFlags.Top += alternatives.Height + 10;
                    }
                    break;

                case "OUTERLABEL":
                    properties_jumpTarget_label.Visible = true;
                    properties_jumpTarget_label.Text = "File Jump Target:";
                    if (properties_title.Left == 62)
                    {
                        properties_title.Left += 15;
                        properties_title.Width -= 20;
                    }
                    properties_title_label.Text = "Filename: ";
                    properties_file_target.Visible = true;
                    outerlabel_browse.Visible = true;
                    checkFlags.Top = 198;

                    properties_jumpTarget.Visible = false;
                    checkFlags.Visible = false;
                    flagOperations.Visible = false;
                    alternatives.Visible = false;
                    break;
            }
        }

        private Control FindControlByName(string name, Control container)
        {
            foreach (Control c in container.Controls)//flowLayout.Controls)
            {
                if (c.Name == name)
                    return c; //found
            }
            return null; //not found
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearEverything(true);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options_form = new Options();
            options_form.ShowDialog(this);
            variables_list();
            flowLayout.Refresh();
        }

        private void variables_list()
        {
            variablesList.Clear();
            string variableFile = Properties.Settings.Default.Variable_filepath;
            if (variableData != "" || (variableFile != "" && System.IO.File.Exists(variableFile)))
            {
                string[] readlines;
                if (variableData != "") readlines = variableData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                else readlines = System.IO.File.ReadAllLines(variableFile);
                int index = Array.IndexOf(readlines, "==>" + format);
                for (int i = (index + 1); i < readlines.Length; i++)
                {
                    if (!readlines[i].Contains("==>") && readlines[i] != "" && readlines[i] != Environment.NewLine)
                    {
                        string[] vars = readlines[i].Split('|');
                        variablesList.Add(vars[0], vars[1]);
                    }
                    else break;
                }
            }
        }

        private void addCheckTab_Click(object sender, EventArgs e)
        {
            add_Check_Tabs();
            update.Enabled = true;
            if (format == "FHA" && currentLabel.type == "SELECTER") currentLabel.jumps.Add(new string[] { "", "", "", "", "" });
            else currentLabel.jumps.Insert(currentLabel.jumps.Count - 1, new string[] { "", "", "", "", "" });
            properties_check_tabs.SelectedIndex = properties_check_tabs.TabCount - 1;
        }

        private void addOperationTab_Click(object sender, EventArgs e)
        {
            add_Operation_Tabs();
            update.Enabled = true;
            currentLabel.flagOperations.Add(new string[] { "", "", "" });
            properties_operation_tabs.SelectedIndex = properties_operation_tabs.TabCount - 1;
        }

        private void add_Operation_Tabs()
        {
            TabPage operation_tab = new TabPage();

            # region operation_tab_flag_Label

            Label operation_tab_flag_Label = new Label();
            operation_tab_flag_Label.AutoSize = true;
            operation_tab_flag_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            operation_tab_flag_Label.Location = new System.Drawing.Point(3, 13);
            operation_tab_flag_Label.Name = "operation_tab_flag_Label_" + properties_operation_tabs.TabCount;
            operation_tab_flag_Label.Size = new System.Drawing.Size(35, 13);
            operation_tab_flag_Label.TabIndex = 16;
            operation_tab_flag_Label.Text = "Flag:";

            # endregion

            # region operation_tab_flagName

            ComboBox operation_tab_flagName = new ComboBox();
            operation_tab_flagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            operation_tab_flagName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            operation_tab_flagName.Location = new System.Drawing.Point(44, 10);
            operation_tab_flagName.Name = "operation_tab_flagName_" + properties_operation_tabs.TabCount;
            operation_tab_flagName.Size = new System.Drawing.Size(151, 21);
            operation_tab_flagName.TabIndex = 15;
            foreach (var variable in variablesList)
            {
                operation_tab_flagName.Items.Add(variable.Key + " (" + variable.Value + ")");
            }
            operation_tab_flagName.SelectionChangeCommitted += operation_tab_flagName_SelectionChangeCommitted;
            operation_tab_flagName.TextChanged += operation_tab_flagName_TextChanged;
            # endregion

            # region operation_tab_value

            TextBox operation_tab_value = new TextBox();
            operation_tab_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            operation_tab_value.Location = new System.Drawing.Point(107, 41);
            operation_tab_value.Name = "operation_tab_value_" + properties_operation_tabs.TabCount;
            operation_tab_value.Size = new System.Drawing.Size(48, 20);
            operation_tab_value.TabIndex = 18;

            operation_tab_value.TextChanged += operation_tab_value_TextChanged;

            # endregion

            # region operation_tab_operationType

            ComboBox operation_tab_operationType = new ComboBox();
            operation_tab_operationType.FormattingEnabled = true;
            operation_tab_operationType.Items.AddRange(new object[] {
            "+=",
            "-=",
            "="});
            operation_tab_operationType.Location = new System.Drawing.Point(48, 37);
            operation_tab_operationType.Name = "operation_tab_operationType_" + properties_operation_tabs.TabCount;
            operation_tab_operationType.Size = new System.Drawing.Size(40, 27);
            operation_tab_operationType.TabIndex = 18;

            operation_tab_operationType.SelectionChangeCommitted += operation_tab_operationType_SelectionChangeCommitted;

            # endregion

            # region operation_tab_removeTab
            Button operation_tab_removeTab = new Button();
            operation_tab_removeTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            operation_tab_removeTab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            operation_tab_removeTab.Location = new System.Drawing.Point(170, 37);
            operation_tab_removeTab.Name = "operation_tab_removeTab_" + properties_operation_tabs.TabCount;
            operation_tab_removeTab.Size = new System.Drawing.Size(30, 29);
            operation_tab_removeTab.TabIndex = 24;
            operation_tab_removeTab.Text = "-";
            operation_tab_removeTab.UseVisualStyleBackColor = true;
            operation_tab_removeTab.MouseClick += operation_tab_removeTab_MouseClick;
            # endregion

            operation_tab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            operation_tab.Location = new System.Drawing.Point(4, 23);
            operation_tab.Name = "tab" + properties_operation_tabs.TabCount;
            operation_tab.Padding = new System.Windows.Forms.Padding(3);
            operation_tab.Size = new System.Drawing.Size(212, 103);
            operation_tab.TabIndex = 0;
            operation_tab.Text = "Flag " + properties_operation_tabs.TabCount;
            operation_tab.UseVisualStyleBackColor = true;
            operation_tab.Controls.Add(operation_tab_flag_Label);
            operation_tab.Controls.Add(operation_tab_flagName);
            operation_tab.Controls.Add(operation_tab_value);
            operation_tab.Controls.Add(operation_tab_operationType);
            operation_tab.Controls.Add(operation_tab_removeTab);
            //check_tab.Controls.Add(addTab);
            properties_operation_tabs.TabPages.Add(operation_tab);
        }

        void operation_tab_flagName_TextChanged(object sender, EventArgs e)
        {
            string text = ((ComboBox)sender).Text;
            string letter;
            int selectionIndex = ((ComboBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((ComboBox)sender).Text.Length - 1; x++)
            {
                letter = ((ComboBox)sender).Text.Substring(x, 1);
                if (charactersDisallowed.Contains(letter))
                {
                    text = text.Replace(letter, "`");
                    ((ComboBox)sender).Select(selectionIndex + 1, 0);
                }
                if (letter == ":" || letter == "/")
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((ComboBox)sender).Text = text;
            ((ComboBox)sender).Select(selectionIndex - change, 0);

            string[] name = ((ComboBox)sender).Name.Split('_');
            //int.Parse(name[name.Length - 1])
            if (currentLabel.flagOperations[int.Parse(name[name.Length - 1])][0] != ((ComboBox)sender).Text)
            {
                update.Enabled = true;
                if (((ComboBox)sender).Text.Contains('('))
                {
                    string[] variableData = ((ComboBox)sender).Text.ToString().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                    currentLabel.flagOperations[int.Parse(name[name.Length - 1])][0] = variableData[0];
                }
                else currentLabel.flagOperations[int.Parse(name[name.Length - 1])][0] = ((ComboBox)sender).Text;
            }
        }

        private void operation_tab_value_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (charactersDisallowed.Contains(letter))
                {
                    text = text.Replace(letter, "`");
                    ((TextBox)sender).Select(selectionIndex + 1, 0);
                }
                if (letter == ":" || letter == "/")
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex - change, 0);

            string[] name = ((TextBox)sender).Name.Split('_');
            //int.Parse(name[name.Length - 1])
            if (currentLabel.flagOperations[int.Parse(name[name.Length - 1])][2] != ((TextBox)sender).Text)
            {
                currentLabel.flagOperations[int.Parse(name[name.Length - 1])][2] = ((TextBox)sender).Text;
                update.Enabled = true;
            }
        }

        void operation_tab_operationType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentLabel.flagOperations[properties_operation_tabs.SelectedIndex][1] = (((ComboBox)sender).SelectedIndex + 1).ToString();
            update.Enabled = true;
        }

        void operation_tab_flagName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string[] variableData = ((ComboBox)sender).SelectedItem.ToString().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            currentLabel.flagOperations[properties_operation_tabs.SelectedIndex][0] = variableData[0];
            update.Enabled = true;
        }

        void operation_tab_removeTab_MouseClick(object sender, MouseEventArgs e)
        {
            currentLabel.flagOperations.Remove(currentLabel.flagOperations[properties_operation_tabs.SelectedIndex]);
            properties_operation_tabs.TabPages.Remove(properties_operation_tabs.SelectedTab);
            update.Enabled = true;
        }

        private void add_Check_Tabs()
        {
            TabPage check_tab = new TabPage();

            # region check_tab_jumpTarget
            ComboBox check_tab_jumpTarget = new ComboBox();
            check_tab_jumpTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            check_tab_jumpTarget.FormattingEnabled = true;
            check_tab_jumpTarget.Location = new System.Drawing.Point(107, 72);
            check_tab_jumpTarget.Size = new System.Drawing.Size(60, 21);
            check_tab_jumpTarget.Name = "check_tab_jumpTarget_" + properties_check_tabs.TabCount;
            foreach (var item in collection)
            {
                check_tab_jumpTarget.Items.Add(item.Key);
            }

            check_tab_jumpTarget.SelectionChangeCommitted += check_tab_jumpTarget_SelectionChangeCommitted;
            # endregion

            # region jumpTarget_label
            Label jumpTarget_label = new Label();
            jumpTarget_label.AutoSize = true;
            jumpTarget_label.Cursor = System.Windows.Forms.Cursors.Default;
            jumpTarget_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            jumpTarget_label.Location = new System.Drawing.Point(21, 75);
            jumpTarget_label.Size = new System.Drawing.Size(81, 13);
            jumpTarget_label.Text = "Jump Target:";
            jumpTarget_label.Name = "jumpTarget_label_" + properties_check_tabs.TabCount;
            # endregion

            # region check_tab_value
            TextBox check_tab_value = new TextBox();
            check_tab_value.Name = "check_tab_value_" + properties_check_tabs.TabCount;
            check_tab_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            check_tab_value.Location = new System.Drawing.Point(119, 43);
            check_tab_value.Size = new System.Drawing.Size(48, 20);

            check_tab_value.TextChanged += check_tab_value_TextChanged;
            # endregion

            # region check_tab_with_Label
            Label check_tab_with_Label = new Label();
            check_tab_with_Label.Name = "check_tab_with_Label_" + properties_check_tabs.TabCount;
            check_tab_with_Label.AutoSize = true;
            check_tab_with_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            check_tab_with_Label.Location = new System.Drawing.Point(77, 46);
            check_tab_with_Label.Size = new System.Drawing.Size(30, 13);
            check_tab_with_Label.Text = "with";
            # endregion

            # region check_tab_decisionType
            ComboBox check_tab_decisionType = new ComboBox();
            check_tab_decisionType.Name = "check_tab_decisionType_" + properties_check_tabs.TabCount;
            check_tab_decisionType.FormattingEnabled = true;
            check_tab_decisionType.Items.AddRange(new object[] {
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">="});
            check_tab_decisionType.Location = new System.Drawing.Point(24, 39);
            check_tab_decisionType.Size = new System.Drawing.Size(40, 27);
            check_tab_decisionType.SelectionChangeCommitted += check_tab_decisionType_SelectionChangeCommitted;

            # endregion

            # region check_tab_flagName
            ComboBox check_tab_flagName = new ComboBox();
            check_tab_flagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            check_tab_flagName.Name = "check_tab_flagName_" + properties_check_tabs.TabCount;
            check_tab_flagName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            check_tab_flagName.Location = new System.Drawing.Point(37, 11);
            check_tab_flagName.Size = new System.Drawing.Size(151, 21);
            foreach (var variable in variablesList)
            {
                check_tab_flagName.Items.Add(variable.Key + " (" + variable.Value + ")");
            }

            check_tab_flagName.SelectionChangeCommitted += check_tab_flagName_SelectionChangeCommitted;
            check_tab_flagName.TextChanged += check_tab_flagName_TextChanged;
            # endregion

            # region check_tab_then_Label
            Label check_tab_then_Label = new Label();
            check_tab_then_Label.Name = "check_tab_then_Label_" + properties_check_tabs.TabCount;
            check_tab_then_Label.AutoSize = true;
            check_tab_then_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            check_tab_then_Label.Location = new System.Drawing.Point(174, 46);
            check_tab_then_Label.Size = new System.Drawing.Size(36, 13);
            check_tab_then_Label.Text = "Then";
            # endregion

            # region check_tab_flag_Label
            Label check_tab_flag_Label = new Label();
            check_tab_flag_Label.Name = "check_tab_flag_Label_" + properties_check_tabs.TabCount;
            check_tab_flag_Label.AutoSize = true;
            check_tab_flag_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            check_tab_flag_Label.Location = new System.Drawing.Point(3, 13);
            check_tab_flag_Label.Size = new System.Drawing.Size(35, 13);
            check_tab_flag_Label.Text = "Flag:";
            # endregion

            # region check_tab_is_Label
            Label check_tab_is_Label = new Label();
            check_tab_is_Label.Name = "check_tab_is_Label_" + properties_check_tabs.TabCount;
            check_tab_is_Label.AutoSize = true;
            check_tab_is_Label.Cursor = System.Windows.Forms.Cursors.Default;
            check_tab_is_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            check_tab_is_Label.Location = new System.Drawing.Point(3, 46);
            check_tab_is_Label.Size = new System.Drawing.Size(16, 13);
            check_tab_is_Label.Text = "is";
            # endregion

            # region check_tab_removeTab
            Button check_tab_removeTab = new Button();
            check_tab_removeTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            check_tab_removeTab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            check_tab_removeTab.Location = new System.Drawing.Point(160, 72);
            check_tab_removeTab.Name = "check_tab_removeTab_" + properties_check_tabs.TabCount;
            check_tab_removeTab.Size = new System.Drawing.Size(30, 29);
            check_tab_removeTab.TabIndex = 24;
            check_tab_removeTab.Text = "-";
            check_tab_removeTab.UseVisualStyleBackColor = true;
            check_tab_removeTab.MouseClick += check_tab_removeTab_MouseClick;
            # endregion

            # region check_tab_linkType_label
            Label check_tab_linkType_label = new Label();
            check_tab_linkType_label.AutoSize = true;
            check_tab_linkType_label.Location = new System.Drawing.Point(200, 72);
            check_tab_linkType_label.Name = "check_tab_linkType_label_" + properties_check_tabs.TabCount;
            check_tab_linkType_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            check_tab_linkType_label.Size = new System.Drawing.Size(54, 14);
            check_tab_linkType_label.TabIndex = 24;
            check_tab_linkType_label.Text = "Link Type";
            # endregion

            # region check_tab_linktype
            ComboBox check_tab_linktype = new ComboBox();
            check_tab_linktype.FormattingEnabled = true;
            check_tab_linktype.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            check_tab_linktype.Items.AddRange(new object[] {
            "AND (&&)",
            "OR (||)"});
            check_tab_linktype.Location = new System.Drawing.Point(276, 72);
            check_tab_linktype.Name = "check_tab_linktype_" + properties_check_tabs.TabCount;
            check_tab_linktype.Size = new System.Drawing.Size(61, 32);
            check_tab_linktype.TabIndex = 25;

            check_tab_linktype.SelectionChangeCommitted += check_tab_linktype_SelectionChangeCommitted;
            # endregion

            //# region check_tab_alternativeID
            //ComboBox check_tab_alternativeID = new ComboBox();
            //check_tab_alternativeID.FormattingEnabled = true;
            //check_tab_alternativeID.Name = "check_tab_alternativeID_" + properties_check_tabs.TabCount;
            //check_tab_alternativeID.Size = new System.Drawing.Size(40, 27);
            //check_tab_alternativeID.TabIndex = 18;
            //check_tab_alternativeID.Location = new System.Drawing.Point(48, 37);
            //# endregion // For FHA

            check_tab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            check_tab.Location = new System.Drawing.Point(4, 23);
            check_tab.Name = "checkTab" + properties_check_tabs.TabCount;
            check_tab.Padding = new System.Windows.Forms.Padding(3);
            check_tab.Size = new System.Drawing.Size(212, 103);
            check_tab.Text = "Flag " + (properties_check_tabs.TabCount + 1);
            check_tab.UseVisualStyleBackColor = true;
            check_tab.Controls.Add(check_tab_jumpTarget);
            check_tab.Controls.Add(jumpTarget_label);
            check_tab.Controls.Add(check_tab_value);
            check_tab.Controls.Add(check_tab_with_Label);
            check_tab.Controls.Add(check_tab_decisionType);
            check_tab.Controls.Add(check_tab_flagName);
            check_tab.Controls.Add(check_tab_then_Label);
            check_tab.Controls.Add(check_tab_flag_Label);
            check_tab.Controls.Add(check_tab_is_Label);
            check_tab.Controls.Add(check_tab_removeTab);
            check_tab.Controls.Add(check_tab_linkType_label);
            check_tab.Controls.Add(check_tab_linktype);

            //if (format == "FHA") check_tab.Controls.Add(check_tab_alternativeID);
            //check_tab.Controls.Add(addTab);
            properties_check_tabs.TabPages.Add(check_tab);
        }

        void check_tab_flagName_TextChanged(object sender, EventArgs e)
        {
            string text = ((ComboBox)sender).Text;
            string letter;
            int selectionIndex = ((ComboBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((ComboBox)sender).Text.Length - 1; x++)
            {
                letter = ((ComboBox)sender).Text.Substring(x, 1);
                if (charactersDisallowed.Contains(letter))
                {
                    text = text.Replace(letter, "`");
                    ((ComboBox)sender).Select(selectionIndex + 1, 0);
                }
                if (letter == ":" || letter == "/")
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((ComboBox)sender).Text = text;
            ((ComboBox)sender).Select(selectionIndex - change, 0);

            string[] name = ((ComboBox)sender).Name.Split('_');
            if (currentLabel.jumps[int.Parse(name[name.Length - 1])][2] != ((ComboBox)sender).Text)
            {
                update.Enabled = true;
                if (((ComboBox)sender).Text.Contains('('))
                {
                    string[] variableData = ((ComboBox)sender).Text.ToString().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                    currentLabel.jumps[int.Parse(name[name.Length - 1])][2] = variableData[0];
                }
                else currentLabel.jumps[int.Parse(name[name.Length - 1])][2] = ((ComboBox)sender).Text;
            }
        }

        void check_tab_linktype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentLabel.jumps[properties_check_tabs.SelectedIndex][1] = (((ComboBox)sender).SelectedIndex + 1).ToString();
            update.Enabled = true;
        }

        void check_tab_jumpTarget_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentLabel.jumps[properties_check_tabs.SelectedIndex][0] = (((ComboBox)sender).SelectedItem).ToString();
            update.Enabled = true;
        }

        void check_tab_decisionType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            update.Enabled = true;
            currentLabel.jumps[properties_check_tabs.SelectedIndex][3] = (((ComboBox)sender).SelectedIndex).ToString();
        }

        void check_tab_flagName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            update.Enabled = true;
            string[] variableData = ((ComboBox)sender).SelectedItem.ToString().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            currentLabel.jumps[properties_check_tabs.SelectedIndex][2] = variableData[0];
        }

        void check_tab_value_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (charactersDisallowed.Contains(letter))
                {
                    text = text.Replace(letter, "`");
                    ((TextBox)sender).Select(selectionIndex + 1, 0);
                }
                if (letter == ":" || letter == "/")
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex - change, 0);

            string[] name = ((TextBox)sender).Name.Split('_');
            //int.Parse(name[name.Length - 1])
            if (currentLabel.jumps[int.Parse(name[name.Length - 1])][4] != ((TextBox)sender).Text)
            {
                update.Enabled = true;
                currentLabel.jumps[int.Parse(name[name.Length - 1])][4] = ((TextBox)sender).Text;
            }
        }

        void check_tab_removeTab_MouseClick(object sender, MouseEventArgs e)
        {
            currentLabel.jumps.Remove(currentLabel.jumps[properties_check_tabs.SelectedIndex]);
            properties_check_tabs.TabPages.Remove(properties_check_tabs.SelectedTab);
            update.Enabled = true;
        }

        private void addAlt_Click(object sender, EventArgs e)
        {
            add_Alt_tabs();
            currentLabel.alts.Add(new string[] { "", "" });
            properties_alternative_tabs.SelectedIndex = properties_alternative_tabs.TabCount - 1;
            update.Enabled = true;
        }

        private void add_Alt_tabs()
        {
            TabPage alt_tab = new TabPage();

            # region alt_tab_choice_label
            Label alt_tab_choice_label = new Label();
            alt_tab_choice_label.AutoSize = true;
            alt_tab_choice_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            alt_tab_choice_label.Location = new System.Drawing.Point(7, 13);
            alt_tab_choice_label.Name = "alt_tab_choice_label_" + properties_alternative_tabs.TabCount;
            alt_tab_choice_label.Size = new System.Drawing.Size(50, 13);
            alt_tab_choice_label.TabIndex = 0;
            alt_tab_choice_label.Text = "Choice:";
            # endregion

            # region alt_tab_choice_value
            TextBox alt_tab_choice_value = new TextBox();
            alt_tab_choice_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            alt_tab_choice_value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            alt_tab_choice_value.Location = new System.Drawing.Point(63, 10);
            alt_tab_choice_value.Name = "alt_tab_choice_value_" + properties_alternative_tabs.TabCount;
            alt_tab_choice_value.Size = new System.Drawing.Size(130, 20);
            alt_tab_choice_value.TabIndex = 1;

            alt_tab_choice_value.TextChanged += alt_tab_choice_value_TextChanged;
            # endregion

            # region alt_tab_target_label
            Label alt_tab_target_label = new Label();
            alt_tab_target_label.AutoSize = true;
            alt_tab_target_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            alt_tab_target_label.Location = new System.Drawing.Point(7, 47);
            alt_tab_target_label.Name = "alt_tab_target_label_" + properties_alternative_tabs.TabCount;
            alt_tab_target_label.Size = new System.Drawing.Size(48, 13);
            alt_tab_target_label.TabIndex = 2;
            alt_tab_target_label.Text = "Target:";
            # endregion

            # region alt_tab_target_value
            ComboBox alt_tab_target_value = new ComboBox();
            alt_tab_target_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            alt_tab_target_value.FormattingEnabled = true;
            alt_tab_target_value.Location = new System.Drawing.Point(63, 39);
            alt_tab_target_value.Name = "alt_tab_target_value_" + properties_alternative_tabs.TabCount;
            alt_tab_target_value.Size = new System.Drawing.Size(60, 21);
            alt_tab_target_value.TabIndex = 25;
            foreach (var item in collection)
            {
                alt_tab_target_value.Items.Add(item.Key);
            }

            alt_tab_target_value.SelectionChangeCommitted += alt_tab_target_value_SelectionChangeCommitted;
            # endregion

            # region alt_tab_removeTab
            Button alt_tab_removeTab = new Button();
            alt_tab_removeTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            alt_tab_removeTab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            alt_tab_removeTab.Location = new System.Drawing.Point(150, 37);
            alt_tab_removeTab.Name = "alt_tab_removeTab_" + properties_alternative_tabs.TabCount;
            alt_tab_removeTab.Size = new System.Drawing.Size(30, 29);
            alt_tab_removeTab.TabIndex = 24;
            alt_tab_removeTab.Text = "-";
            alt_tab_removeTab.UseVisualStyleBackColor = true;
            alt_tab_removeTab.MouseClick += alt_tab_removeTab_MouseClick;
            # endregion

            alt_tab.Controls.Add(alt_tab_target_value);
            alt_tab.Controls.Add(alt_tab_target_label);
            alt_tab.Controls.Add(alt_tab_choice_value);
            alt_tab.Controls.Add(alt_tab_choice_label);
            alt_tab.Controls.Add(alt_tab_removeTab);
            alt_tab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            alt_tab.Location = new System.Drawing.Point(4, 23);
            alt_tab.Name = "alt" + properties_alternative_tabs.TabCount;
            alt_tab.Size = new System.Drawing.Size(216, 78);
            alt_tab.TabIndex = 0;
            alt_tab.Text = "Alt " + (properties_alternative_tabs.TabCount + 1);
            alt_tab.UseVisualStyleBackColor = true;

            properties_alternative_tabs.TabPages.Add(alt_tab);
        }

        void alt_tab_target_value_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentLabel.alts[properties_alternative_tabs.SelectedIndex][1] = ((ComboBox)sender).SelectedItem.ToString();
            update.Enabled = true;
        }

        void alt_tab_choice_value_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (format == "FHA")
                {
                    if (letter == ":" || letter == "/")
                    {
                        text = text.Replace(letter, String.Empty);
                        change = 1;
                    }
                }
                if (charactersDisallowed.Contains(letter))
                {
                    text = text.Replace(letter, "`");
                    ((TextBox)sender).Select(selectionIndex + 1, 0);
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex - change, 0);

            string[] name = ((TextBox)sender).Name.Split('_');
            //int.Parse(name[name.Length - 1])
            int id = int.Parse(name[name.Length - 1]);
            if (currentLabel.alts[id][0] != ((TextBox)sender).Text)
            {
                currentLabel.alts[id][0] = ((TextBox)sender).Text;
                //debug.Text = currentLabel.alts[id][0] + "/" + collection[currentLabel.id].alts[id][0];
                update.Enabled = true;
            }
        }

        void alt_tab_removeTab_MouseClick(object sender, MouseEventArgs e)
        {
            currentLabel.alts.Remove(currentLabel.alts[properties_alternative_tabs.SelectedIndex]);
            properties_alternative_tabs.TabPages.Remove(properties_alternative_tabs.SelectedTab);
            update.Enabled = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about_dialog = new AboutBox();
            about_dialog.ShowDialog(this);
        }

        private void outerlabel_browse_Click(object sender, EventArgs e)
        {
            outerlabel_open_target.ShowDialog();
        }

        private void outerlabel_open_target_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.FileInfo fInfo = new System.IO.FileInfo(outerlabel_open_target.FileName);
            properties_title.Text = fInfo.Name.Replace(".fcf", "");
            currentLabel.title = fInfo.Name.Replace(".fcf", "");
            properties_file_target.Items.Clear();

            string[] readlines = System.IO.File.ReadAllLines(fInfo.FullName);
            for (var line = 1; line < readlines.Length; line++)
            {
                string[] pm = readlines[line].Split(';');
                pm = pm[1].Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);
                if (pm[0] == "SCENE" || pm[0] == "SELECTER" || pm[0] == "OUTERLABEL")
                {
                    properties_file_target.Items.Add(pm[1]);
                }
            }
        }

        private void properties_title_OuterLabel()
        {
            properties_file_target.Items.Clear();
            if (System.IO.File.Exists(Properties.Settings.Default.Flowchart_folderpath + "\\" + properties_title.Text + ".fcf"))
            {
                string[] readlines = System.IO.File.ReadAllLines(Properties.Settings.Default.Flowchart_folderpath + "\\" + properties_title.Text + ".fcf");
                for (var line = 1; line < readlines.Length; line++)
                {
                    string[] pm = readlines[line].Split(';');
                    pm = pm[1].Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);
                    if (pm[0] == "SCENE" || pm[0] == "SELECTER" || pm[0] == "OUTERLABEL")
                    {
                        properties_file_target.Items.Add(pm[1]);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(false, false);
        }

        private void properties_type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentLabel.type = (string)((ComboBox)sender).SelectedItem;
            change_type();
            Control control = FindControlByName(previousType + "_" + previousID, flowLayout);

            change_properties(currentLabel);
            edit_properties((TextBox)control);
            update.Enabled = true;
        }

        private void change_type()
        {
            switch (currentLabel.type)
            {
                case "SCENE":
                    if (currentLabel.jumps == null) currentLabel.jumps = new List<string[]> { new string[] { currentLabel.id.ToString(), "1" } };
                    if (currentLabel.flagOperations == null) currentLabel.flagOperations = new List<string[]>();
                    break;

                case "SELECTER":
                    if (currentLabel.alts == null) currentLabel.alts = new List<string[]>();
                    if (format == "FHA" && currentLabel.jumps == null) currentLabel.jumps = new List<string[]>();
                    break;

                case "OUTERLABEL":
                    if (currentLabel.file == null) currentLabel.file = "";
                    //currentLabel.target = 0;
                    break;
            }
        }

        private void properties_jumpTarget_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (currentLabel.jumps == null)
            {
                //currentLabel.jumps = new List<string[]>();
                currentLabel.jumps.Add(new string[] { properties_jumpTarget.SelectedItem.ToString(), "1" });
            }
            if (currentLabel.jumps.Count == 0) currentLabel.jumps.Add(new string[] { properties_jumpTarget.SelectedItem.ToString(), "1" });
            else currentLabel.jumps[currentLabel.jumps.Count - 1][0] = properties_jumpTarget.SelectedItem.ToString();
            update.Enabled = true;
        }

        private void properties_file_target_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLabel.target = int.Parse(properties_file_target.SelectedItem.ToString());
            update.Enabled = true;
        }

        private void update_label()
        {
            if (previousID != currentLabel.id || previousType != currentLabel.type)
            {
                Control control = FindControlByName(previousType + "_" + previousID, flowLayout);
                //Console.WriteLine("Control Found!");
                control.Name = currentLabel.type + "_" + currentLabel.id;
                control.Text = currentLabel.type + ": " + currentLabel.id;
                //Console.Write(" " + control.Name + ", " + control.Text);
                collection.Remove(previousID);
                delete_line(previousID);
                Labels newLabel;
                switch (currentLabel.type)
                {
                    case "SCENE":
                        newLabel = new Labels("SCENE", currentLabel.id, currentLabel.title, currentLabel.left, currentLabel.top, currentLabel.right, currentLabel.bottom, point, new List<string[]> { new string[] { "" + currentLabel.id, "1" } }, new List<string[]>());
                        break;
                    case "SELECTER":
                        newLabel = new Labels("SELECTER", currentLabel.id, currentLabel.title, currentLabel.left, currentLabel.top, currentLabel.right, currentLabel.bottom, point, new List<string[]>());
                        break;
                    default:
                        newLabel = new Labels("OUTERLABEL", currentLabel.id, currentLabel.left, currentLabel.top, currentLabel.right, currentLabel.bottom, point, currentLabel.file, currentLabel.target);
                        break;
                }
                collection.Add(currentLabel.id, newLabel);
                update_jumpsList();
                //Console.WriteLine(collection.Count);
            }

            update_line();
        }

        private void update_line()
        {
            string[] lines = log.Lines;
            if (lines.Length != 0)
            {
                for (int line = 0; line < lines.Length; line++)
                {
                    if (lines[line] == "") break;
                    string[] lineSplit = lines[line].Split(';');
                    string[] lineData = lineSplit[1].Split(new string[] { "'" }, StringSplitOptions.None);
                    if (int.Parse(lineData[1]) == previousID && lineData[0] == previousType) //if (int.Parse(lineData[1]) == currentLabel.id && lineData[0] == currentLabel.type)
                    {
                        collection[currentLabel.id] = currentLabel;
                        lines[line] = form_string(currentLabel);
                        log.Lines = lines;
                        break;
                    }
                    else
                    {
                        collection[currentLabel.id] = currentLabel;
                        string[] newLines = new string[lines.Length + 1];
                        string[] newString = new string[] { form_string(currentLabel) };

                        lines.CopyTo(newLines, 0);
                        newString.CopyTo(newLines, lines.Length);
                        log.Lines = newLines;
                    }
                }
            }
            else
            {
                collection[currentLabel.id] = currentLabel;
                string[] newLines = new string[lines.Length + 1];
                string[] newString = new string[] { form_string(currentLabel) };

                lines.CopyTo(newLines, 0);
                newString.CopyTo(newLines, lines.Length);
                log.Lines = newLines;
            }
            if (ChangesMade != true) ChangesMade = true;
        }

        private void delete_line(int previousID)
        {
            string[] lines = log.Lines;
            if (lines.Length != 0)
            {
                for (int line = 0; line < lines.Length; line++)
                {
                    if (lines[line] != "")
                    {
                        string[] lineSplit = lines[line].Split(';');
                        string[] lineData = lineSplit[1].Split(new string[] { "'" }, StringSplitOptions.None);
                        if (int.Parse(lineData[1]) == previousID && lineData[0] == previousType)
                        {
                            lines = lines.Where(w => w != lines[line]).ToArray();
                            log.Lines = lines;
                            break;
                        }
                    }
                }
            }
            if (ChangesMade != true) ChangesMade = true;
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (collection.ContainsKey(currentLabel.id) && previousID != currentLabel.id)
            {
                MessageBox.Show("The same ID already exists. Please choose a different one.", "NO duplicates allowed!");
                properties_id.Focus();
                properties_id.SelectAll();
                return;
            }
            if (properties_id.Text == "" || properties_type.SelectedIndex == -1 || properties_x.Text == "" || properties_y.Text == "" || properties_title.Text == "")
            {
                MessageBox.Show("Atleast input all the basic data before updating!", "Hey, you lazy person!");
                return;
            }

            switch (currentLabel.type)
            {
                case "SCENE":
                    if (currentLabel.jumps.Count >= 1)
                    {
                        foreach (var jump in currentLabel.jumps)
                        {
                            if (jump.Length > 2 && (jump[0] == "" || jump[1] == "" || jump[2] == "" || jump[3] == ""))
                            {
                                MessageBox.Show("Jump data isn't complete! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            else if (jump[0] == "" || jump[1] == "")
                            {
                                MessageBox.Show("Jump data isn't complete! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                        foreach (var operation in currentLabel.flagOperations)
                        {
                            if (operation[0] == "" || operation[1] == "" || operation[2] == "")
                            {
                                MessageBox.Show("Flag operations data isn't complete! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                    }
                    if (currentLabel.flagOperations.Count >= 1)
                    {
                        foreach (var flag in currentLabel.flagOperations)
                        {
                            if (flag[0] == "" || flag[1] == "" || flag[2] == "")
                            {
                                MessageBox.Show("Flag Operations data isn't complete! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!");
                                return;
                            }
                        }
                    }
                    break;

                case "SELECTER":
                    if (format == "FHA")
                    {
                        foreach (var jump in currentLabel.jumps)
                        {
                            if (jump[0] == "" || jump[1] == "" || jump[2] == "" || jump[3] == "")
                            {
                                MessageBox.Show("The Flag Check data for alternatives isn't complete! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!");
                                return;
                            }
                        }
                    }
                    foreach (var alt in currentLabel.alts)
                    {
                        if (alt[0] == "" || alt[1] == "")
                        {
                            MessageBox.Show("The Alternative data isn't complete! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!");
                            return;
                        }
                    }
                    break;

                case "OUTERLABEL":
                    if (currentLabel.file == "" || currentLabel.target == null)
                    {
                        MessageBox.Show("The destination file and/or target is missing! Please make sure that all the fields are filled, before attempting to update!", "Hey, you lazy person!");
                        return;
                    }
                    break;
            }

            update_label();

            update.Enabled = false;

            clearProperties();
            flowLayout.Refresh();
        }

        private void clearProperties()
        {
            currentLabel = null;
            properties_id.Text = "";
            properties_title.Text = "";
            properties_type.SelectedIndex = -1;
            properties_x.Text = "";
            properties_y.Text = "";
            properties_jumpTarget.SelectedIndex = -1;
            properties_jumpTarget_label.Text = "Default Jump Target:";
            properties_title_label.Text = "Title:";
            properties_jumpTarget_label.Visible = true;

            properties_file_target.Visible = false;
            properties_jumpTarget.Visible = true;
            outerlabel_browse.Visible = false;

            checkFlags.Visible = false;
            flagOperations.Visible = false;
            alternatives.Visible = false;

            removeButton.Visible = false;
            removeButton.Enabled = false;
            resetButton.Visible = false;
            resetButton.Enabled = false;
            update.Visible = false;
            update.Enabled = false;

            properties_id.Enabled = false;
            properties_title.Enabled = false;
            properties_type.Enabled = false;
            properties_x.Enabled = false;
            properties_y.Enabled = false;
            properties_jumpTarget.Enabled = false;
            foreach (TabPage tab in properties_check_tabs.TabPages)
                properties_check_tabs.TabPages.Remove(tab);
            foreach (TabPage tab in properties_operation_tabs.TabPages)
                properties_operation_tabs.TabPages.Remove(tab);
            foreach (TabPage tab in properties_alternative_tabs.TabPages)
                properties_alternative_tabs.TabPages.Remove(tab);
        }

        private string form_string(Labels label)
        {
            string line = "";
            int left = label.left;// -flowLayout.Width;
            int top = label.top;// -flowLayout.Height;
            int right = label.right;//left+30;
            int bottom = label.bottom;//top + 16;

            Control control = FindControlByName(label.type + "_" + label.id, flowLayout);
            Random rnd = new Random();
            switch (label.type)
            {
                case "SCENE":
                    //debug.Text = "";
                    string jumps = "";
                    int njumps = 0;
                    for (int i = 0; i < label.jumps.Count; i++)
                    {
                        if (label.jumps[i].Length > 2)
                        {
                            int nDecisions = 0;
                            string decisions = "";
                            foreach (var jump in label.jumps)
                            {
                                if (jump[0] == label.jumps[i][0] && jump[1] == label.jumps[i][1])
                                {
                                    if (jump.Length > 2)
                                    {
                                        nDecisions++;
                                        decisions += jump[2] + "//" + jump[3] + "//" + jump[4] + ":";
                                    }
                                }
                            }
                            jumps = nDecisions + ":" + decisions + label.jumps[i][1] + ":" + label.jumps[i][0] + ":" + rnd.Next(1, 12) + "'";
                            if (nDecisions > 1) njumps = 1;
                            else njumps++;
                            //debug.Text += njumps + ". ";
                        }
                        else
                        {
                            jumps += 0 + ":" + label.jumps[i][1] + ":" + label.jumps[i][0] + ":" + rnd.Next(1, 12) + "'";
                            njumps++;
                            //debug.Text += njumps + ". ";
                        }
                    }
                    string operations = "";
                    if (label.flagOperations.Count == 0)
                    {
                        operations = "0";
                    }
                    else
                    {
                        operations += label.flagOperations.Count;
                        for (int i = 0; i < label.flagOperations.Count; i++)
                        {
                            operations += ":" + label.flagOperations[i][0] + "//" + label.flagOperations[i][2] + "//" + label.flagOperations[i][1];
                        }
                    }
                    line = label.id + ";" + label.type + "'" + label.id + "'" + left + "'" + top + "'" + right + "'" + bottom + "'" + 0 + "'" + 16777215 + "'" + 0 + "'" + 0 + "'" + label.title + "'" + njumps + "'" + jumps + operations;
                    return line;

                case "SELECTER":
                    string alts = "";
                    for (int i = 0; i < label.alts.Count; i++)
                    {
                        alts += "'" + label.alts[i][1] + "'" + rnd.Next(1, 12) + "'" + label.alts[i][0];

                        if (format == "FHA")
                        {
                            string decisions = "";
                            int nDecisions = 0;
                            string linkType = "";
                            for (int j = 0; j < label.jumps.Count; j++)
                            {   // target, linkType, flagName, decisionType, arg
                                if (label.alts[i][1] == label.jumps[j][0])
                                {
                                    if (nDecisions > 1)
                                    {
                                        if (linkType != label.jumps[j][1]) break;
                                    }
                                    linkType = label.jumps[j][1];

                                    decisions += label.jumps[j][2] + "//" + label.jumps[j][3] + "//" + label.jumps[j][4] + ":";
                                    nDecisions++;
                                }
                            }
                            if (nDecisions > 0) alts += ":" + nDecisions + ":" + decisions + linkType;
                        }
                    }
                    line = label.id + ";" + label.type + "'" + label.id + "'" + left + "'" + top + "'" + right + "'" + bottom + "'" + 0 + "'" + 16777215 + "'" + 0 + "'" + 0 + "'" + label.title + "'" + label.alts.Count + alts;
                    return line;

                case "OUTERLABEL":
                    line = "";
                    line = label.id + ";" + label.type + "'" + label.id + "'" + left + "'" + top + "'" + right + "'" + bottom + "'" + 0 + "'" + 16777215 + "'" + 0 + "'" + 0 + "'" + label.file + "'" + label.target;
                    return line;
            }
            return "";
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                openFile.ShowDialog();
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                save(false, false);
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                save(true, false);
            }

            if (e.Control && e.KeyCode == Keys.N)
            {
                clearEverything(true);
            }

            if (e.Control && e.KeyCode == Keys.R)
            {
                flowLayout.Refresh();
            }
        }

        private void saveFile_FileOk(object sender, CancelEventArgs e)
        {
            // If the file name is not an empty string open it for saving.
            if (saveFile.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                UnicodeEncoding unicode = new UnicodeEncoding();
                string comment = (comment_box.Text == "") ? comment = "COMMENT!" : comment = comment_box.Text;
                System.IO.File.WriteAllText(saveFile.FileName, comment + Environment.NewLine + log.Text, unicode);
                this.fileName = saveFile.FileName;
            }
        }

        private void sCENEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (update.Enabled)
            {
                MessageBox.Show("Update the selected flow element before attempting to create another flow element!", "Update!");
                return;
            }
            int max = 0;
            if (collection.Count > 0) max = collection.Keys.Max() + 1;
            Labels scene = new Labels("SCENE", max, "", 0, 0, 0, 0, point, new List<string[]> { new string[] { max.ToString(), "1" } }, new List<string[]> { });
            collection.Add(max, scene);
            TextBox sceneButton = scene.drawSceneButton();


            sceneButton.MouseClick += box_Click;
            sceneButton.MouseDown += box_MouseDown;
            sceneButton.MouseUp += box_MouseUp;
            sceneButton.MouseMove += box_MouseMove;
            sceneButton.KeyDown += Button_KeyDown;

            update_jumpsList();

            flowLayout.Controls.Add(sceneButton);
            flowLayout.Refresh();

            currentLabel = new Labels(scene);
            previousID = currentLabel.id;
            previousType = currentLabel.type;
            edit_properties(sceneButton);
            sceneButton.Focus();
            update.Enabled = true;

            if (ChangesMade != true) ChangesMade = true;
        }

        void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                delete_box((TextBox)sender);
            }
            if (e.KeyCode == Keys.Tab)
            {
                GetNextControl((TextBox)sender, true).Focus();
            }
        }

        private void delete_box(TextBox label)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete the selected Flow element?", "Confirm Deletion!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (confirm == DialogResult.Yes)
            {
                currentLabel = null;
                string[] data = label.Name.Split('_');
                flowLayout.Controls.Remove(label);
                collection.Remove(int.Parse(data[1]));
                update_jumpsList();
                clearProperties();
                delete_line(int.Parse(data[1]));

                //update.Enabled = false;
                flowLayout.Refresh();
            }
            else return;
        }

        private void sELECTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (update.Enabled)
            {
                MessageBox.Show("Update the selected flow element before attempting to create another flow element!", "Update!");
                return;
            }
            int max = 0;
            if (collection.Count > 0) max = collection.Keys.Max() + 1;
            Labels selecter;
            if (format == "FHA") selecter = new Labels("SELECTER", max, "", 0, 0, 0, 0, new List<string[]> { }, new List<string[]> { });
            else selecter = new Labels("SELECTER", max, "", 0, 0, 0, 0, point, new List<string[]> { });
            collection.Add(max, selecter);
            TextBox selecterButton = selecter.drawSelecterButton();

            selecterButton.MouseClick += box_Click;
            selecterButton.MouseDown += box_MouseDown;
            selecterButton.MouseUp += box_MouseUp;
            selecterButton.MouseMove += box_MouseMove;
            selecterButton.KeyDown += Button_KeyDown;

            update_jumpsList();

            flowLayout.Controls.Add(selecterButton);
            flowLayout.Refresh();

            currentLabel = new Labels(selecter);
            previousID = currentLabel.id;
            previousType = currentLabel.type;
            edit_properties(selecterButton);
            selecterButton.Focus();
            update.Enabled = true;

            if (ChangesMade != true) ChangesMade = true;
        }

        private void oUTERLABELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (update.Enabled)
            {
                MessageBox.Show("Update the selected flow element before attempting to create another flow element!", "Update!");
                return;
            }
            int max = 0;
            if (collection.Count > 0) max = collection.Keys.Max() + 1;
            Labels outerlabel = new Labels("OUTERLABEL", max, 0, 0, 0, 0, point, "", 0);
            collection.Add(max, outerlabel);
            TextBox outerlabel_Button = outerlabel.drawOuterLabel();

            outerlabel_Button.MouseClick += box_Click;
            outerlabel_Button.MouseDown += box_MouseDown;
            outerlabel_Button.MouseUp += box_MouseUp;
            outerlabel_Button.MouseMove += box_MouseMove;
            outerlabel_Button.KeyDown += Button_KeyDown;

            update_jumpsList();

            flowLayout.Controls.Add(outerlabel_Button);
            flowLayout.Refresh();

            currentLabel = new Labels(outerlabel);
            previousID = currentLabel.id;
            previousType = currentLabel.type;
            edit_properties(outerlabel_Button);
            outerlabel_Button.Focus();
            update.Enabled = true;

            if (ChangesMade != true) ChangesMade = true;
        }

        private void properties_title_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (charactersDisallowed.Contains(letter))
                {
                    text = text.Replace(letter, "'");
                    change = 1;
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex + change, 0);

            if (((TextBox)sender).Text != "")
            {
                if (currentLabel.type == "OUTERLABEL")
                {
                    if (currentLabel.file != ((TextBox)sender).Text)
                    {
                        currentLabel.file = ((TextBox)sender).Text;
                        update.Enabled = true;
                    }
                    return;
                }
                else
                {
                    if (currentLabel.title != ((TextBox)sender).Text)
                    {
                        currentLabel.title = ((TextBox)sender).Text;
                        update.Enabled = true;
                    }
                }
            }
        }

        private void properties_x_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (!numbersAllowed.Contains(letter))
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex - change, 0);

            if (((TextBox)sender).Text != "")
            {
                if (currentLabel.left != int.Parse(((TextBox)sender).Text))
                {
                    currentLabel.left = int.Parse(((TextBox)sender).Text);
                    update.Enabled = true;
                }
            }
        }

        private void properties_y_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (!numbersAllowed.Contains(letter))
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex - change, 0);

            if (((TextBox)sender).Text != "")
            {
                if (currentLabel.top != int.Parse(((TextBox)sender).Text))
                {
                    currentLabel.top = int.Parse(((TextBox)sender).Text);
                    update.Enabled = true;
                }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (clearEverything(true))
            {

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 1)
                {
                    MessageBox.Show("Wait up a second! My small brain possible can't read through multiple files at the same time! That's absurd!!! \nPlease, give me only one file to eat, else I blow up!", "Brain too slow to read " + files.Length + " files...");
                    return;
                }
                foreach (string file in files)
                {
                    Console.WriteLine("Reading: " + file);
                    if (file.EndsWith(".fcf")) { readFile(file); }
                    else
                    {
                        string[] errorFile = file.Split('.');
                        MessageBox.Show("What is this stuff you're feeding me?! I only eat top-class Flowcharts and TypeMoon variables!\nRest goes all over my small head... So, please feed me '.fcf' files only!", "Can't comprehend ." + errorFile[errorFile.Length - 1] + " file...");
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// This method checks to see if the control needs to be moved based on the current location
        /// of the mouse cursor, calculates the new location and moves the control to the new location.
        /// </summary>
        /// <param name="ctrl">The control to be moved.</param>
        /// <param name="moveOffset">How much to move.</param>
        /// <remarks></remarks>
        private void MoveControl(Control ctrl, Point moveOffset)
        {
            //No need to perform any of the moving methods if the control isn't moving
            if (!(moveOffset.X == 0 && moveOffset.Y == 0))
            {
                //set the button new x coordinate
                int newX = ctrl.Location.X + moveOffset.X;
                int newY = ctrl.Location.Y + moveOffset.Y;

                //check for top left edge of the client area. Bottom right is infinite because the control auto scrolls.
                if (newX < 5) newX = 5;
                if (newY < 5) newY = 5;

                //assign new control location
                ctrl.Location = new Point(newX, newY);
            }
        }

        private void properties_id_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            string letter;
            int selectionIndex = ((TextBox)sender).SelectionStart;
            int change = 0;

            for (int x = 0; x <= ((TextBox)sender).Text.Length - 1; x++)
            {
                letter = ((TextBox)sender).Text.Substring(x, 1);
                if (!numbersAllowed.Contains(letter))
                {
                    text = text.Replace(letter, String.Empty);
                    change = 1;
                }
            }

            ((TextBox)sender).Text = text;
            ((TextBox)sender).Select(selectionIndex - change, 0);

            if (((TextBox)sender).Text != "")
            {
                if (currentLabel.id != int.Parse(((TextBox)sender).Text))
                {
                    currentLabel.id = int.Parse(((TextBox)sender).Text);
                    update.Enabled = true;
                }
            }
        }

        private void flowLayout_Click(object sender, EventArgs e)
        {
            addLabelsInPanel.Show(Cursor.Position);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowLayout.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void log_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                log.SelectAll();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            delete_box(FindControlByName(currentLabel.type + "_" + currentLabel.id, flowLayout) as TextBox);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true, false);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChangesMade && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialog = MessageBox.Show("Some changes have been made, which aren't saved yet... Do you want to save them before closing?", "Unsaved changes!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dialog == DialogResult.Yes)
                {
                    if (!save(false, true)) e.Cancel = true;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            currentLabel = collection[currentLabel.id];
            edit_properties((TextBox)FindControlByName(currentLabel.type + "_" + currentLabel.id, flowLayout));
            update.Enabled = false;
        }
    }
}