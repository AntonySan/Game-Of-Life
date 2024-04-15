using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Game_Of_Life
{
    internal class UIElement
    {
        internal Label CreateLabel(string text, Point location)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = location;
            label.AutoSize = true;
            label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            return label;
        }

        internal Button CreateButton(string text, Point location, Size size, AnchorStyles anchorStyles, EventHandler eventHandler)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = location;
            button.Size = size;
            button.Anchor = anchorStyles;
            button.Click += eventHandler;
            return button;
        }

        internal PictureBox CreatePictureBox(Point point, Size size)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = point;
            pictureBox.Size = size;
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            return pictureBox;
        }

        internal NumericUpDown CreateNumericUpDown(Point point, Size size, short max, short min, short value, AnchorStyles anchorStyles)
        {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Location = point;
            numericUpDown.Size = size;
            numericUpDown.Maximum = max;
            numericUpDown.Minimum = min;
            numericUpDown.Value = value;
            numericUpDown.Anchor = anchorStyles;
            return numericUpDown;
        }

        internal ComboBox CreateComboBox(Point point, Size size)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = point;
            comboBox.Size = size;
            return comboBox;
        }


        internal static ToolStripMenuItem CreateToolStripMenuItem(string text, EventHandler onClick)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = text;
            menuItem.Click += onClick;
            return menuItem;
        }


         void Test()
        {
            Console.WriteLine("мяу");
            Console.WriteLine("поні-мяу");
        }

    }

    public partial class ConwayMain : Form
    {
        private PictureBox pbGrid;

        private NumericUpDown numSSize;

        private ComboBox cboCells;

        private Button[] Buttons = new Button[4];

        private bool InProgress;

        private Grid cellGrid;

        private string[] buttonName = new string[4] { "", "", "", "" };

        private UIElement uiElement;



        MenuStrip menuStrip1 = new MenuStrip();

        ToolStripMenuItem fileToolStripMenuItem = new ToolStripMenuItem("Game");
        ToolStripMenuItem toolStripMenuItem2;
        ToolStripMenuItem toolStripMenuItem3;
        ToolStripMenuItem toolStripMenuItem4;
        ToolStripMenuItem goToolStripMenuItem;
        ToolStripMenuItem exitToolStripMenuItem;

        public ConwayMain()
        {
            uiElement = new UIElement();


            InitializeComponent();
            InitializeUIElements();




            Load += ConwayMain_Load;

        }
        private void InitializeComponent()
        {
            AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(944, 581);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            MinimumSize = new System.Drawing.Size(960, 620);
            Name = "ConwayMain";
            Text = "Conway`s Game of Life";
        }

        private void InitializeUIElements()
        {
            AddLabel("Cell Size", new Point(19, 508));
            buttonName = new string[4] { "Reset", "Advance", "Go", "Clear" };
            Point[] location = new Point[4]
            {
                new Point(168, 506),
                new Point(752, 506),
                new Point(833, 506),
                new Point(249, 506)
            };
            AnchorStyles[] anchorStyles = new AnchorStyles[4]
            {
                AnchorStyles.Bottom | AnchorStyles.Left,
                AnchorStyles.Bottom | AnchorStyles.Right,
                AnchorStyles.Bottom | AnchorStyles.Right,
                AnchorStyles.Bottom | AnchorStyles.Left
            };
            EventHandler[] eventHandler = new EventHandler[4] { btnResset_click, btnAdvance_click, btnGo_click, btnClear_click };
            AddButton(buttonName, location, new Size(75, 23), anchorStyles, eventHandler);
            AddPictureBox(new Point(22, 56), new Size(910, 436));
            AddCreateNumericUpDown(new Point(88, 506), new Size(60, 20), 25, 5, 10, AnchorStyles.Bottom | AnchorStyles.Left);
            AddComboBox(new Point(380, 504), new Size(197, 23));
            AddMenuStrip();
        }

        private void AddLabel(string labelText, Point location)
        {
            if (uiElement != null)
            {
                Label value = uiElement.CreateLabel(labelText, location);
                Controls.Add(value);
            }
        }

        private void AddMenuStrip()
        {

            toolStripMenuItem2 = UIElement.CreateToolStripMenuItem("&Reset Grid", resetGridToolStripMenu_Click);
            toolStripMenuItem3 = UIElement.CreateToolStripMenuItem("&Clear Grid", clearGridToolStripMenu_Click);
            toolStripMenuItem4 = UIElement.CreateToolStripMenuItem("&Advance", advanceGridToolStripMenu_Click);
            goToolStripMenuItem = UIElement.CreateToolStripMenuItem("&Go", goGridToolStripMenu_Click);
            exitToolStripMenuItem = UIElement.CreateToolStripMenuItem("&Exit", exitGridToolStripMenu_Click);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
        toolStripMenuItem2,
        toolStripMenuItem3,
        new ToolStripSeparator(),
        toolStripMenuItem4,
        goToolStripMenuItem,
        new ToolStripSeparator(),
        exitToolStripMenuItem
    });

            menuStrip1.Items.Add(fileToolStripMenuItem);

            Controls.Add(menuStrip1);
        }
        private void AddButton(string[] text, Point[] location, Size size, AnchorStyles[] anchorStyles, EventHandler[] eventHandler)
        {
            if (uiElement != null)
            {
                for (int i = 0; i < buttonName.Length; i++)
                {
                    Buttons[i] = uiElement.CreateButton(text[i], location[i], size, anchorStyles[i], eventHandler[i]);
                    Control.ControlCollection controls = Controls;
                    Control[] buttons = Buttons;
                    controls.AddRange(buttons);
                }
            }
        }

        private void AddPictureBox(Point point, Size size)
        {
            if (uiElement != null)
            {
                pbGrid = uiElement.CreatePictureBox(point, size);
                pbGrid.MouseClick += pbGrid_MouseClick;
                Controls.Add(pbGrid);
            }
        }

        private void AddCreateNumericUpDown(Point point, Size size, short max, short min, short value, AnchorStyles anchorStyles)
        {
            if (uiElement != null)
            {
                numSSize = uiElement.CreateNumericUpDown(point, size, max, min, value, anchorStyles);
                Controls.Add(numSSize);
            }
        }

        private void AddComboBox(Point point, Size size)
        {
            cboCells = uiElement.CreateComboBox(point, size);
            Controls.Add(cboCells);
        }

        private void ConwayMain_Load(object sender, EventArgs e)
        {
            CreateGridSurface(RandomCells: true);
        }

        private void ConvayMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            InProgress = false;
            Application.Exit();
        }

        private void pbGrid_MouseClick(object sender, MouseEventArgs e)
        {
            Size cellSize = Grid.gridCells[0].CellSize;
            int num = e.X / cellSize.Width;
            int num2 = e.Y / cellSize.Height;
            int index = num2 * cellGrid.Columns + num;
            Grid.gridCells[index].IsAlive = !Grid.gridCells[index].IsAlive;
            UpdateGrid(cellGrid);
        }

        private void CreateGridSurface(bool RandomCells)
        {
            Random random = new Random();
            int num = (int)(pbGrid.Height / numSSize.Value);
            int num2 = (int)(pbGrid.Width / numSSize.Value);
            cellGrid = new Grid(num, num2);
            Grid.gridCells.Clear();
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    Cell cell = new Cell(j, i, (int)numSSize.Value);
                    if (RandomCells)
                    {
                        cell.IsAlive = ((random.Next(100) < 15) ? true : false);
                    }
                    else
                    {
                        cell.IsAlive = false;
                    }
                }
            }
            Grid.gridCells = Enumerable.ToList<Cell>(Grid.gridCells.OrderBy<Cell, int>((Cell c) => c.Xpos).OrderBy<Cell, int>((Cell c) => c.Ypos));
            UpdateGrid(cellGrid);
        }

        private void btnResset_click(object sender, EventArgs e)
        {
            CreateGridSurface(RandomCells: true);
        }

        private void btnAdvance_click(object sender, EventArgs e)
        {
            GetNextState();
        }

        private void btnGo_click(object sender, EventArgs e)
        {
            Go();
        }

        private void Go()
        {
            InProgress = !InProgress;
            Buttons[2].Text = (InProgress ? "Stop" : "GO");
            goToolStripMenuItem.Text = (InProgress ? "Stop" : "GO");
            while (InProgress)
            {
                GetNextState();
                Application.DoEvents();
            }
        }

        private void btnClear_click(object sender, EventArgs e)
        {
            CreateGridSurface(RandomCells: false);
        }

        private void resetGridToolStripMenu_Click(object sender, EventArgs e)
        {
            CreateGridSurface(true);
        }

        private void clearGridToolStripMenu_Click(object sender, EventArgs e)
        {
            CreateGridSurface(false);
        }

        private void advanceGridToolStripMenu_Click(object sender, EventArgs e)
        {
            GetNextState();
        }

        private void goGridToolStripMenu_Click(object sender, EventArgs e)
        {
            Go();
        }
        private void exitGridToolStripMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void GetActiveCounts()
        {
            cboCells.Items.Clear();
            foreach (Cell gridCell in Grid.gridCells)
            {
                cboCells.Items.Add($"X:{gridCell.Xpos}, Y:{gridCell.Ypos}, Count: {cellGrid.LiveAdjacent(gridCell)}");
            }
        }

        private void GetNextState()
        {
            foreach (Cell gridCell in Grid.gridCells)
            {
                int num = cellGrid.LiveAdjacent(gridCell);
                if (gridCell.IsAlive)
                {
                    if (num < 2 || num > 3)
                    {
                        gridCell.NextStatus = false;
                    }
                    else
                    {
                        gridCell.NextStatus = true;
                    }
                }
                else if (num == 3)
                {
                    gridCell.NextStatus = true;
                }
            }
            foreach (Cell gridCell2 in Grid.gridCells)
            {
                gridCell2.IsAlive = gridCell2.NextStatus;
            }
            UpdateGrid(cellGrid);
        }

        private void UpdateGrid(Grid GridDisplay)
        {
            Random random = new Random();
            Bitmap bitmap = new Bitmap(pbGrid.Width, pbGrid.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new SolidBrush(Color.DarkOrange);
            graphics.Clear(Color.Black);
            foreach (Cell gridCell in Grid.gridCells)
            {
                if (gridCell.IsAlive)
                {
                    graphics.FillRectangle(brush, gridCell.CellDisplay);
                }
            }
            if (pbGrid.Image != null)
            {
                pbGrid.Image.Dispose();
            }
            pbGrid.Image = (Bitmap)bitmap.Clone();
        }

    }

    public class Cell
    {
        private Point cLocation;

        private Rectangle cCellDisplay;

        private Size cCellSize;

        private int cXpos;

        private int cYpos;

        private bool cIsAlive;

        private bool cNext;

        public Rectangle CellDisplay
        {
            get
            {
                return cCellDisplay;
            }
            set
            {
                cCellDisplay = value;
            }
        }

        public Size CellSize
        {
            get
            {
                return cCellSize;
            }
            set
            {
                cCellSize = value;
            }
        }

        public Point Location
        {
            get
            {
                return cLocation;
            }
            set
            {
                cLocation = value;
            }
        }

        public int Xpos
        {
            get
            {
                return cXpos;
            }
            set
            {
                cXpos = value;
            }
        }

        public int Ypos
        {
            get
            {
                return cYpos;
            }
            set
            {
                cYpos = value;
            }
        }

        public bool IsAlive
        {
            get
            {
                return cIsAlive;
            }
            set
            {
                cIsAlive = value;
            }
        }

        public bool NextStatus
        {
            get
            {
                return cNext;
            }
            set
            {
                cNext = value;
            }
        }

        public Cell(Point location, int X, int Y)
        {
            Location = location;
            Xpos = X;
            Ypos = Y;
            Grid.gridCells.Add(this);
            int num = ((X != 0) ? (location.X / X) : 0);
            CellDisplay = new Rectangle(Location, new Size(num, num));
        }

        public Cell(int X, int Y, int CellSize)
        {
            Location = new Point(X * CellSize, Y * CellSize);
            Xpos = X;
            Ypos = Y;
            this.CellSize = new Size(CellSize, CellSize);
            Grid.gridCells.Add(this);
            CellDisplay = new Rectangle(Location, new Size(CellSize - 1, CellSize - 1));
        }

        public override string ToString()
        {
            return $"GridX: {Xpos}  DridY: {Ypos}  LocX: {Location.X}  LocY: {Location.Y}";
        }
    }

    public class Grid
    {
        public static List<Cell> gridCells = new List<Cell>();

        private int cRows;

        private int cCols;

        public int Rows
        {
            get
            {
                return cRows;
            }
            set
            {
                cRows = value;
            }
        }

        public int Columns
        {
            get
            {
                return cCols;
            }
            set
            {
                cCols = value;
            }
        }

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public int LiveAdjacent(Cell cell)
        {
            int num = 0;
            int num2 = cell.Ypos * Columns + cell.Xpos;
            int num3 = num2 - Columns - 2;
            int num4 = num2 + Columns + 2;
            num3 = ((num3 >= 0) ? num3 : 0);
            num4 = ((num4 > gridCells.Count - 1) ? (gridCells.Count - 1) : num4);
            for (int i = num3; i < num4; i++)
            {
                if (Math.Abs(cell.Xpos - gridCells[i].Xpos) < 2 && Math.Abs(cell.Ypos - gridCells[i].Ypos) < 2 && gridCells[i].Location != cell.Location && gridCells[i].IsAlive)
                {
                    num++;
                }
            }
            return num;
        }
    }

}



