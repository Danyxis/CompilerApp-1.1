using System.Windows.Forms;

namespace CompilerApp
{
    public partial class MainMenuForm : Form // �������� ����� ����������
    {
        private string? currentFilePath = null; // ������� ���� �����
        private bool isTextChanged = false; // ������������ ��������� ������

        public MainMenuForm()
        {
            InitializeComponent();
            fontSizeComboBox.SelectedItem = "9"; // ��������� ������ ������ (������� ��������������, ������ �����������)

            // �������� ������������ ������� �������������� � ����������
            // ����� � ������� �������������� (inputArea)
            inputArea.AllowDrop = true;
            inputArea.DragEnter += InputArea_DragEnter;
            inputArea.DragDrop += InputArea_DragDrop;
        }

        // ����������� �������
        private void ItemCreateFile_Click(object sender, EventArgs e) // ���������� ������� �������� �����
        {
            FileActions.CreateFile(this);
        }

        private void ItemOpenFile_Click(object sender, EventArgs e) // ���������� ������� �������� �����
        {
            FileActions.OpenFile(this);
        }

        private void ItemSaveFile_Click(object sender, EventArgs e) // ���������� ������� ���������� �����
        {
            FileActions.SaveFile(this);
        }

        private void ItemSaveFileAs_Click(object sender, EventArgs e) // ���������� ������� ���������� ����� ���
        {
            FileActions.SaveFileAs(this);
        }

        private void ItemExit_Click(object sender, EventArgs e) // ���������� ������� ������ �� ����������
        {
            FileActions.ExitApplication(this);
        }

        private void InputArea_TextChanged(object sender, EventArgs e) // ���������� ������� ��������� ������
        {
            SetTextChanged(true);
        }

        private void ItemUndo_Click(object sender, EventArgs e) // ���������� ������� ������ ����������� ��������� � ������
        {
            EditActions.Undo(this);
        }

        private void ItemRedo_Click(object sender, EventArgs e) // ���������� ������� ���������� ����������� ��������� � ������
        {
            EditActions.Redo(this);
        }

        private void ItemCut_Click(object sender, EventArgs e) // ���������� ������� - �������� ��������� ��������
        {
            EditActions.Cut(this);
        }

        private void ItemCopy_Click(object sender, EventArgs e) // ���������� ������� - ���������� ��������� ��������
        {
            EditActions.Copy(this);
        }

        private void ItemPaste_Click(object sender, EventArgs e) // ���������� ������� - �������� ��������� ��������
        {
            EditActions.Paste(this);
        }

        private void ItemDelete_Click(object sender, EventArgs e) // ���������� ������� - ������� ��������� ��������
        {
            EditActions.Delete(this);
        }

        private void ItemSelectAll_Click(object sender, EventArgs e) // ���������� ������� - �������� ���� �����
        {
            EditActions.SelectAll(this);
        }

        private void ItemLaunch_Click(object sender, EventArgs e) // ���������� ������� - ������ �����������
        {
            LaunchActions.Launch(this);
        }

        private void ItemCallHelp_Click(object sender, EventArgs e) // ���������� ������� ������ ���� �������
        {
            HelpActions.ShowHelp();
        }

        private void ItemAboutProgram_Click(object sender, EventArgs e) // ���������� ������� ������ ���� "� ���������"
        {
            HelpActions.ShowAboutProgram();
        }

        // ���������� ������� �������� ����� (����� �������)
        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsTextChanged())
            {
                var result = MessageBox.Show(
                    "��������� ��������� ����� �������?",
                    "������������� ���������",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    if (!FileActions.SaveFile(this))
                    {
                        e.Cancel = true; // ���� ���������� �� ������, �������� ��������
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // �������� ��������, ���� ������������ ����� �������
                }
            }
        }

        // ������ � RichTextBox � DataGridView
        public RichTextBox GetInputArea() // ���������� ������ inputArea (������� �����)
        {
            return inputArea;
        }

        public DataGridView GetOutputTable() // ���������� ������ outputArea (������� ������)
        {
            return outputTable;
        }

        // ������ � ���� �����
        public void SetCurrentFilePath(string path) // ������������� ������� ���� �����
        {
            currentFilePath = path;
        }

        public string? GetCurrentFilePath() // ���������� ������� ���� �����
        {
            return currentFilePath;
        }

        // ������ � ����������� ������
        public void SetTextChanged(bool changed) // ������������� ���� ��������� ������ (true - ����� ������, false - ����� �� ������)
        {
            isTextChanged = changed;
        }

        public bool IsTextChanged() // ���������� ���� ��������� ������
        {
            return isTextChanged;
        }

        // �������������� �����������

        // ���������� ������� ������ ������� ������ (������� ��������������, ������ �����������)
        private void FontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(fontSizeComboBox.SelectedItem.ToString(), out int fontSize))
            {
                inputArea.Font = new Font(inputArea.Font.FontFamily, fontSize);
                outputTable.DefaultCellStyle.Font = new Font(outputTable.Font.FontFamily, fontSize);
            }
        }

        // ���������� ������� ������� ������ (������� ������� ��� ������� ������)
        private void MainMenuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N) // Ctrl + N � ������� ����� ����
            {
                FileActions.CreateFile(this);
                e.SuppressKeyPress = true;       // ������ ������� ������������
            }
            else if (e.Control && e.KeyCode == Keys.O) // Ctrl + O � ������� ����
            {
                FileActions.OpenFile(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.S && !e.Shift) // Ctrl + S � ���������
            {
                FileActions.SaveFile(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.S && e.Shift) // Ctrl + Shift + S � ��������� ���
            {
                FileActions.SaveFileAs(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Q) // Ctrl + Q � �����
            {
                FileActions.ExitApplication(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Z) // Ctrl + Z � ������
            {
                EditActions.Undo(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y) // Ctrl + Y � ������
            {
                EditActions.Redo(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.X) // Ctrl + X � ��������
            {
                EditActions.Cut(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // Ctrl + C � ����������
            {
                EditActions.Copy(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.V) // Ctrl + V � ��������
            {
                EditActions.Paste(this);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.A) // Ctrl + A � �������� ��
            {
                EditActions.SelectAll(this);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1) // F1 � �������
            {
                HelpActions.ShowHelp();
                e.SuppressKeyPress = true;
            }
        }

        // ���������� ������� �������������� ����� � ������� ��������������
        private void InputArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // ���������� ������� ���������� ����� � ������� ��������������
        private void InputArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0 && Path.GetExtension(files[0]).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                string filePath = files[0];
                inputArea.Text = File.ReadAllText(filePath);
                SetCurrentFilePath(filePath);
                SetTextChanged(false);

                UpdateStatus($"������ ����: {Path.GetFileName(filePath)}"); // ���������� ������ ���������
            }
            else
            {
                MessageBox.Show("����� ������� ������ ��������� ����� (*.txt).", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // �������������� ������

        // ����� ���������� ������ ���������
        public void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }
    }
}
