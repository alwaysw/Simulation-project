using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
namespace AutoProjectSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string currentFolderPath = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            //string folderPath = @"C:\Users\user\Desktop\��������";
            //if (Directory.Exists(folderPath))
            //{
            //    string[] files = Directory.GetFiles(folderPath);
            //    comboBox_project.Items.Clear();
            //    foreach (var file in files)
            //    {
            //        comboBox_project.Items.Add(Path.GetFileName(file));
            //    }
            //}
        }
        private void btn_chooseproject_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "����ɮ�";
                openFileDialog.Filter = "�Ҧ��ɮ� (*.*)|*.*"; // �i�̻ݨD�վ��ɮ�����

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // �u����ɮצW��
                    //textBox_appsetting.Text = Path.GetFileName(openFileDialog.FileName);
                    textBox_appsetting.Text = openFileDialog.FileName;
                    // �Y�n��ܧ�����|�A�Чאּ openFileDialog.FileName
                }
            }
        }
        private void btn_chooseproject_Click2(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "���JSON�ɮ�";
                openFileDialog.Filter = "JSON�ɮ� (*.json)|*.json|�Ҧ��ɮ� (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    textBox_appsetting.Text = Path.GetFileName(filePath);

                    // Ū���øѪRJSON
                    try
                    {
                        string jsonText = File.ReadAllText(filePath);
                        textBox_content.Clear();

                        // �ϥ� JsonNode �ѪR�û��j���
                        var node = JsonNode.Parse(jsonText);
                        DisplayJsonNode(node, "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ū���θѪRJSON����: " + ex.Message);
                    }
                }
            }
        }
        private void DisplayJsonNode(JsonNode node, string prefix)
        {
            if (node is JsonObject obj)
            {
                foreach (var kvp in obj)
                {
                    DisplayJsonNode(kvp.Value, $"{prefix}{kvp.Key}: ");
                }
            }
            else if (node is JsonArray arr)
            {
                int idx = 0;
                foreach (var item in arr)
                {
                    DisplayJsonNode(item, $"{prefix}[{idx}]: ");
                    idx++;
                }
            }
            else if (node is JsonValue val)
            {
                // �N���e�[�� textBoxContent
                textBox_content.AppendText($"{prefix}{val.ToJsonString()}{Environment.NewLine}");
            }
        }
       
        
        
    }
}
