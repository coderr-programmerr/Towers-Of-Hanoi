using System.Collections;
using System.Drawing;

namespace TowersofHanoi
{
    public partial class Form1 : Form
    {
        Color colourOfDisks = Color.Red;
        Color colourOfPoles = Color.Brown;
        Color backgroundOfPoles = Color.LightGoldenrodYellow;
        Color backgroundOfGame = Color.LightCyan;


        Dictionary<PictureBox, Stack<int?>> globPoleStack = new Dictionary<PictureBox, Stack<int?>>();
        int globPoles = 3;
        int globDisks = 6;
        private List<Stack<int?>> gameData = InitializePoles(3, 6);

        public Form1()
        {
            InitializeComponent();
            var gameObjects = InitializePictureButtons(globPoles);
            var (pictureBoxes, buttons) = gameObjects;
            updateDictionary(pictureBoxes);
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(pictureBoxes);
        }


        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox pictureBox1 = (PictureBox)sender;
            DrawPole(e.Graphics, pictureBox1);
            DrawDisks(e.Graphics, pictureBox1, Color.Red);
        }

        private void DrawPolesAndDisks(List<PictureBox> pictureBoxes)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Invalidate();
            }
        }

        private Color GetDarkerColor(int index, Color baseColor)
        {
            float factor = (float)index / globDisks;
            int r = (int)(baseColor.R * factor);
            int g = (int)(baseColor.G * factor);
            int b = (int)(baseColor.B * factor);
            return Color.FromArgb(r, g, b);
        }

        public void DrawDisks(Graphics g, PictureBox pictureBox, Color diskColour)
        {
            int count = 0;
            foreach (int? disk in globPoleStack[pictureBox])
            {
                if (disk == null)
                {
                    break;
                }


                count++;
                int poleTop = Convert.ToInt16(pictureBox.Height * 0.15); // Top position of the pole
                int width = Convert.ToInt16(pictureBox.Width * 0.05);
                int ballWidth = Convert.ToInt16(pictureBox.Width * 0.8);
                int poleBottom = Convert.ToInt16(pictureBox.Height * 0.9) - Convert.ToInt16(width * 2); ; // Bottom position of the pole
                int diameter = ((poleBottom - poleTop) / globDisks);
                int y = poleBottom - (diameter * count);

                int ballSizeToUse;

                if (ballWidth < diameter)
                {
                    ballSizeToUse = ballWidth;
                }
                else
                {
                    ballSizeToUse = diameter;
                }

                int x = Convert.ToInt16((pictureBox.Width - ballSizeToUse) / 2);

                Color brushColour = GetDarkerColor((int)disk, colourOfDisks);
                SolidBrush myBrush = new SolidBrush(brushColour);

                g.FillEllipse(myBrush, x, y, ballSizeToUse, diameter);

            }
        }

        private void DrawPole(Graphics g, PictureBox pictureBox)
        {
            int poleTop = Convert.ToInt16(pictureBox.Height * 0.1); // Top position of the pole
            int poleBase = Convert.ToInt16(pictureBox.Height * 0.9); // Bottom position of the pole
            int width = Convert.ToInt16(pictureBox.Width * 0.05);
            int x = Convert.ToInt16(pictureBox.Width / 2);
            Brush brush = new SolidBrush(colourOfPoles);

            // Draw the pole
            g.FillRectangle(brush, x - width / 2, poleTop, width, poleBase - poleTop);

            // Draw the base
            int baseWidth = 15 * width;
            int baseHeight = Convert.ToInt16(width * 2);
            g.FillRectangle(brush, x - baseWidth / 2, poleBase - baseHeight, baseWidth, baseHeight);
        }

        private void updateDictionary(List<PictureBox> pictureBoxes)
        {
            globPoleStack.Clear();
            for (int i = 0; i < gameData.Count; i++)
            {
                globPoleStack.Add(pictureBoxes[i], gameData[i]);
            }
        }

        private static List<Stack<int?>> InitializePoles(int poles, int disks)
        {
            List<Stack<int?>> poleList = new List<Stack<int?>>();

            for (int i = 0; i < poles; i++)
            {
                Stack<int?> pole = new Stack<int?>();

                // Only fill the first stack
                if (i == 0)
                {
                    for (int j = disks; j >= 1; j--)
                    {
                        pole.Push(j);
                    }
                }

                poleList.Add(pole);
            }

            return poleList;
        }

        private Tuple<List<PictureBox>, List<Button>> InitializePictureButtons(int x)
        {

            // Initialize a list of PictureBox objects
            List<PictureBox> pictureBoxes = new List<PictureBox>(x);
            List<Button> buttons = new List<Button>(x);

            for (int i = 1; i <= x; i++)
            {
                pictureBoxes.Add(InitializePictureBox(i, x));
                GamePanel.Controls.Add(pictureBoxes[i - 1]); // Use (i - 1) as the index
                buttons.Add(InitializeButton(i, x));
                GamePanel.Controls.Add(buttons[i - 1]); // Use (i - 1) as the index
            }
            var gameObjects = Tuple.Create(pictureBoxes, buttons);
            return gameObjects;

        }

        private PictureBox InitializePictureBox(int i, int x)
        {
            int margin = 30;
            int height = GamePanel.Height - 90;
            int gameSpace = GamePanel.Width - (2 * margin);
            int horizontaldistance = (margin + (i * (gameSpace / ((x + 1) * (x + 1)))) + ((i - 1) * (gameSpace / (x + 1))));

            PictureBox pictureBox = new PictureBox
            {
                // Set the location and size of the PictureBox control.
                Location = new System.Drawing.Point(Convert.ToInt16(horizontaldistance), 30),
                Size = new System.Drawing.Size(Convert.ToInt16(gameSpace / (x + 1)), Convert.ToInt16(height * 0.9)),
                BackColor = backgroundOfPoles
            };

            // Set additional properties or event handlers if needed.

            return pictureBox;
        }

        private Button InitializeButton(int i, int x)
        {
            int margin = 30;
            int height = GamePanel.Height - 90;
            int gameSpace = GamePanel.Width - (2 * margin);
            int horizontaldistance = (margin + (i * (gameSpace / ((x + 1) * (x + 1)))) + ((i - 1) * (gameSpace / (x + 1))));

            Button button = new Button
            {
                // Set the location and size of the PictureBox control.
                Location = new System.Drawing.Point(Convert.ToInt16(horizontaldistance), Convert.ToInt16((height * 0.9) + 60)),
                Size = new System.Drawing.Size(Convert.ToInt16(gameSpace / (x + 1)), Convert.ToInt16(height * 0.1)),
                BackColor = Color.LightGray
            };


            // Set additional properties or event handlers if needed.

            return button;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RedrawButton_Click(object sender, EventArgs e)
        {
            globPoles = Convert.ToInt16(PoleCount.Value);
            globDisks = Convert.ToInt16(DiskCount.Value);

            GamePanel.Controls.Clear();
            var gameObjects = InitializePictureButtons(globPoles);
            var (pictureBoxes, buttons) = gameObjects;
            gameData = InitializePoles(globPoles, globDisks);
            updateDictionary(pictureBoxes);
            GamePanel.BackColor = backgroundOfGame;
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(pictureBoxes);
        }

        private void DiskColour_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = DiskColour.BackColor;

            
            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                DiskColour.BackColor = MyDialog.Color;
                colourOfDisks = MyDialog.Color;
            }
                
        }

        private void PoleColour_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = PoleColour.BackColor;

            

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                PoleColour.BackColor = MyDialog.Color;
                colourOfPoles = MyDialog.Color;
            }
        }

        private void PoleBackground_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = PoleBackground.BackColor;

            

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                PoleBackground.BackColor = MyDialog.Color;
                backgroundOfPoles = MyDialog.Color;
            }


        }

        private void GameBackground_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = GameBackground.BackColor;

            

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                GameBackground.BackColor = MyDialog.Color;
                backgroundOfGame = MyDialog.Color;
            }

        }
    }
}