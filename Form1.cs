namespace TowersofHanoi
{
    public partial class Form1 : Form
    {

        int globActiveBox = 0;
        int globPoles = 3;
        int globDisks = 6;
        private List<Stack<int>> gameData = InitializePoles(3, 6);

        public Form1()
        {
            InitializeComponent();
            var gameObjects = InitializePictureButtons(globPoles);
            var (pictureBoxes, buttons) = gameObjects;
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(pictureBoxes);
            globActiveBox = 0;
        }


        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox pictureBox1 = (PictureBox)sender;
            DrawPole(e.Graphics, pictureBox1);
            for (int i = 0; i < gameData.Count; i++)
            {
                DrawDisks(e.Graphics, pictureBox1, gameData[i], Color.Red);
            }
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

        // Function to draw a single pole
        public void DrawDisks(Graphics g, PictureBox pictureBox, Stack<int> currentStack, Color diskColour)
        {
            int count = 0;
            foreach (int disk in currentStack)
            {
                if (disk == null)
                {
                    break;
                }
                

                count++;
                int poleTop = Convert.ToInt16(pictureBox.Height * 0.15); // Top position of the pole
                int width = Convert.ToInt16(pictureBox.Width * 0.05);
                int poleBottom = Convert.ToInt16(pictureBox.Height * 0.9) - Convert.ToInt16(width * 2); ; // Bottom position of the pole
                int diameter = ((poleBottom - poleTop) / globDisks);
                int x = Convert.ToInt16((pictureBox.Width - diameter) / 2);
                int y = poleBottom - (diameter * count);
                Color brushColour = GetDarkerColor((int)disk, diskColour);
                SolidBrush myBrush = new SolidBrush(brushColour);

                g.FillEllipse(myBrush, x, y, diameter, diameter);

            }
        }
        private static void DrawPole(Graphics g, PictureBox pictureBox)
        {
            int poleTop = Convert.ToInt16(pictureBox.Height * 0.1); // Top position of the pole
            int poleBase = Convert.ToInt16(pictureBox.Height * 0.9); // Bottom position of the pole
            int width = Convert.ToInt16(pictureBox.Width * 0.05);
            int x = Convert.ToInt16(pictureBox.Width / 2);

            // Draw the pole
            g.FillRectangle(Brushes.Brown, x - width / 2, poleTop, width, poleBase - poleTop);

            // Draw the base
            int baseWidth = 15 * width;
            int baseHeight = Convert.ToInt16(width * 2);
            g.FillRectangle(Brushes.Brown, x - baseWidth / 2, poleBase - baseHeight, baseWidth, baseHeight);
        }



        private static List<Stack<int>> InitializePoles(int poles, int disks)
        {
            List<Stack<int>> poleList = new List<Stack<int>>();

            for (int i = 0; i < poles; i++)
            {
                Stack<int> pole = new Stack<int>();

                // Only fill the first stack
                if (i == 0)
                {
                    for (int j = disks; j >= 1; j--)
                    {
                        pole.Push(j);
                    }
                }

                if (i == 1)
                {
                    pole.Push(disks);
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
                BackColor = Color.LightGoldenrodYellow
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
            int poles = Convert.ToInt16(PoleCount.Value);
            int disks = Convert.ToInt16(DiskCount.Value);

            GamePanel.Controls.Clear();
            var gameObjects = InitializePictureButtons(poles);
            var (pictureBoxes, buttons) = gameObjects;
            List<Stack<int>> gameData = InitializePoles(poles, disks);
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(pictureBoxes);
        }
    }
}