using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace DepthmapMaker
{
    public partial class Menu : Form
    {
        private Queue<int>diffuseValueQueue = new Queue<int>(new[] { 100 });
        private Queue<int> attenuationValueQueue = new Queue<int>(new[] { 10 });
        private bool buttonPressed = false;
        private string fileSaveLocation = "";
        public Menu()
        {
            InitializeComponent();
            saveFileDialog1.DefaultExt = "png";
            saveFileDialog1.Filter = "images | *.png";
        }

        public float getDiffuse()
        {
            return diffuseValueQueue.Peek() * 0.1f;
        }

        public float getAttenuation()
        {
            return attenuationValueQueue.Peek() * 0.01f;
        }

        public bool isbuttonPressed()
        {
            if (buttonPressed)
            {
                buttonPressed = false;
                return true;
            }
            return false;
        }

        public string GetSelectedFilepath()
        {
            return fileSaveLocation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void DiffuseBar_ValueChanged_1(object sender, EventArgs e)
        {
            diffuseValueQueue.Enqueue(DiffuseBar.Value);
            diffuseValueQueue.Dequeue();
            DiffuseUpdown.Value = (decimal)getDiffuse();
        }
        private void AttenuationBar_ValueChanged(object sender, EventArgs e)
        {
            attenuationValueQueue.Enqueue(AttenuationBar.Value);
            attenuationValueQueue.Dequeue();
            AttenuationUpDown.Value = (decimal)getAttenuation();
        }

        private void DiffuseUpdown_ValueChanged(object sender, EventArgs e)
        {
            DiffuseBar.Value = (int)(DiffuseUpdown.Value * 10);
        }

        private void AttenuationUpDown_ValueChanged(object sender, EventArgs e)
        {
            AttenuationBar.Value = (int)(AttenuationUpDown.Value * 100);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this);
            fileSaveLocation = saveFileDialog1.FileName;
            if (fileSaveLocation != "")
            {
                buttonPressed = true;
            }
        }
    }
}