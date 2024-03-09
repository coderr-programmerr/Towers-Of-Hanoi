using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace TowersofHanoi
{
    public partial class Form1 : Form
    {

        Color colourOfDisks = Color.Red;
        Color colourOfPoles = Color.Brown;
        Color backgroundOfPoles = Color.LightGoldenrodYellow;
        Color backgroundOfGame;

        int buttonPresses = 0;

        Stack<int> startStack = new Stack<int>();
        Stack<int> destStack = new Stack<int>();
        PictureBox startPBox = new PictureBox();

        Dictionary<PictureBox, Stack<int>> globPoleStack = new Dictionary<PictureBox, Stack<int>>();

        Dictionary<Button, PictureBox> globButtonPictureBox = new Dictionary<Button, PictureBox>();

        int globPoles = 3;
        int globDisks = 6;
        private List<Stack<int>> gameData = InitializePoles(3, 6);
        List<PictureBox> globPBoxes;


        private SoundPlayer soundPlayer;
        public Form1()
        {
            InitializeComponent();
            var gameObjects = InitializePictureButtons(globPoles);
            var (locPictureBoxes, buttons) = gameObjects;
            globPBoxes = locPictureBoxes;
            updateDictionary(locPictureBoxes);
            updateBPDictionary(locPictureBoxes, buttons);
            foreach (var pictureBox in globPBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(locPictureBoxes);


            soundPlayer = new SoundPlayer();

            // Set the path to your sound file
            soundPlayer.Stream = Properties.Resources.I_Deserve_A_Little_Bit_More;

            soundPlayer.PlayLooping();




        }

        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox pictureBox1 =
                (PictureBox)sender;
            DrawPole(e.Graphics, pictureBox1);
            DrawDisks(e.Graphics, pictureBox1);
        }

        private void DynamicButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            PictureBox pBox = globButtonPictureBox[clickedButton];
            startPBox = pBox;
            int value;
            bool startStackIsEmpty = false;
            bool destStackIsEmpty = false;
            destStack = globPoleStack[pBox];

            if (destStack.Count() == 0)
            {
                destStackIsEmpty = true;
            }

            if (startStack.Count() == 0)
            {
                startStackIsEmpty = true;
            }


            buttonPresses += 1;

            if (buttonPresses % 2 == 0)
            {
                if (startStackIsEmpty)
                {
                    return;
                }

                if (destStackIsEmpty)
                {
                    value = startStack.Peek();
                    startStack.Pop();
                    destStack.Push(value);
                    return;

                }

                if (startStack.Peek() < destStack.Peek())
                {
                    value = startStack.Peek();
                    startStack.Pop();
                    destStack.Push(value);
                }
                globPoleStack[startPBox] = startStack;
                globPoleStack[pBox] = destStack;
                GamePanel.BackColor = backgroundOfGame;
                foreach (var pictureBox in globPBoxes)
                {
                    pictureBox.Paint += PictureBox_Paint;
                }
                DrawPolesAndDisks(globPBoxes);
                gameData.Clear();
                foreach (PictureBox picBox in globPBoxes)
                {
                    gameData.Add(globPoleStack[picBox]);
                }


            }
            else
            {
                startStack = globPoleStack[pBox];
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
            int usedNum = globDisks - index;
            float factor = (float)usedNum / globDisks;
            int r = (int)(baseColor.R * factor);
            int g = (int)(baseColor.G * factor);
            int b = (int)(baseColor.B * factor);
            return Color.FromArgb(r, g, b);
        }

        public void DrawDisks(Graphics g, PictureBox pictureBox)
        {
            int count = 0;
            int? disk;

            for (int i = globPoleStack[pictureBox].Count() - 1; i >= 0; i--)
            {

                List<int> myList = new List<int>(globPoleStack[pictureBox]);
                disk = myList[i];
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

        private void updateBPDictionary(List<PictureBox> pictureBoxes, List<Button> buttons)
        {
            globButtonPictureBox.Clear();
            for (int i = 0; i < buttons.Count; i++)
            {
                globButtonPictureBox.Add(buttons[i], pictureBoxes[i]);
            }
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
            button.Click += DynamicButton_Click;

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
            var (locPictureBoxes, buttons) = gameObjects;
            gameData = InitializePoles(globPoles, globDisks);
            globPBoxes = locPictureBoxes;
            updateDictionary(locPictureBoxes);
            updateBPDictionary(locPictureBoxes, buttons);
            GamePanel.BackColor = backgroundOfGame;
            foreach (var pictureBox in locPictureBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(locPictureBoxes);
            buttonPresses = 0;
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

        private void AIMode_Click(object sender, EventArgs e)
        {
            GamePanel.Controls.Clear();
            PictureBox loadingBox = new PictureBox();
            loadingBox.Size = new Size(GamePanel.Width - 1, GamePanel.Height - 1);
            loadingBox.Location = new Point(0, 0);
            loadingBox.Image = Properties.Resources.HardGif;
            loadingBox.Visible = true;
            loadingBox.SizeMode = PictureBoxSizeMode.StretchImage;
            GamePanel.Controls.Add(loadingBox);
            for (long i = 0; i < 10_000_000_000; i++)
            {

            }
            GamePanel.Controls.Clear();

            var gameObjects = InitializePictureButtons(globPoles);
            var (locPictureBoxes, buttons) = gameObjects;
            gameData = AISolvePoles(globPoles, globDisks);
            globPBoxes = locPictureBoxes;
            updateDictionary(locPictureBoxes);
            updateBPDictionary(locPictureBoxes, buttons);
            GamePanel.BackColor = backgroundOfGame;
            foreach (var pictureBox in locPictureBoxes)
            {
                pictureBox.Paint += PictureBox_Paint;
            }
            DrawPolesAndDisks(locPictureBoxes);
            buttonPresses = 0;

        }

        private static List<Stack<int>> AISolvePoles(int poles, int disks)
        {
            List<Stack<int>> poleList = new List<Stack<int>>();

            for (int i = 0; i < poles; i++)
            {
                Stack<int> pole = new Stack<int>();

                // Only fill the first stack
                if (i == poles - 1)
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

        private void Check_Click(object sender, EventArgs e)
        {
            PictureBox lastBox = globPBoxes[globPBoxes.Count() - 1];
            Stack<int> lastStack = globPoleStack[lastBox];

            if (lastStack.Count() != 0 && lastStack.Peek() == 1)
            {
                Check.Text = "Game Complete!";
                Check.BackColor = Color.Green;
                Check.Font = new System.Drawing.Font("Stencil", 16, System.Drawing.FontStyle.Regular);

            }
            else
            {
                Check.Text = "Game Not Over!";
                Check.BackColor = Color.Red;
                Check.Font = new System.Drawing.Font("Stencil", 16, System.Drawing.FontStyle.Regular);
            }
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}