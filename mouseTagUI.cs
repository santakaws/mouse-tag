using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Media;

public class mouseTagUI: Form { 
    private Panel header = new Panel();
    private Graphicpanel display = new Graphicpanel();
    private Panel buttons = new Panel();
    private Label title = new Label();
    private Label author = new Label();
    private Label xLabel = new Label();
    private Label yLabel = new Label();
    private Label taggedLabel = new Label();
    private Label totalTriesLabel = new Label();
    private Label invalidInput = new Label();
    private static Button start = new Button();
    private Button initialize = new Button();
    private Button exit = new Button();
    private TextBox xCoordinate = new TextBox();
    private TextBox yCoordinate = new TextBox();
    private TextBox tagged = new TextBox();
    private TextBox totalTries = new TextBox();
    private Size minimumSize = new Size(1295,850);
    private Size maximumSize = new Size(1920,1080);
    private static double direction;
    private static double radians;
    private static double circSpeed;
    private const double animationClockSpeed = 144;
    private static double deltaX;
    private static double deltaY;
    private static double animationInterval = 1000/animationClockSpeed;
    private int convertedAnInterval = (int)System.Math.Round(animationInterval);
    private const double refreshClockSpeed = 144;
    private static double refreshInterval = 1000/refreshClockSpeed;
    private int convertedReInterval = (int)System.Math.Round(refreshInterval);
    private const double radius = 10;
    private const double initX = 640;
    private const double initY = 287.5;
    private static double x;
    private static double y;
    private static int hitMice = 0;
    private static int attempts = 0;
    private static bool initialized = false;
    private Random rnd = new Random();
    private static System.Timers.Timer refreshClock = new System.Timers.Timer();
    private static System.Timers.Timer ballClock = new System.Timers.Timer();

    public mouseTagUI() {
        MinimumSize = minimumSize;
        MaximumSize = maximumSize;

        xCoordinate.Multiline = true;
        yCoordinate.Multiline = true;

        this.Size = new Size(1280,850);
        header.Size = new Size(1280,60);
        display.Size = new Size(1280,575);
        buttons.Size = new Size(1280,236);
        title.Size = new Size(200,30);
        author.Size = new Size(200,20);
        start.Size = new Size(140,140);
        initialize.Size = new Size(140,140);
        exit.Size = new Size(70,40);
        xCoordinate.Size = new Size(70,40);
        yCoordinate.Size = new Size(70,40);
        tagged.Size = new Size(70,40);
        totalTries.Size = new Size(70,40);
        xLabel.Size = new Size(100,20);
        yLabel.Size = new Size(100,20);
        taggedLabel.Size = new Size(100,20);
        totalTriesLabel.Size = new Size(110,20);
        invalidInput.Size = new Size(200,85);

        header.BackColor = Color.Goldenrod;
        display.BackColor = Color.Thistle;
        buttons.BackColor = Color.RoyalBlue;
        start.BackColor = Color.Maroon;
        initialize.BackColor = Color.Maroon;
        exit.BackColor = Color.Maroon;
        xCoordinate.BackColor = Color.White;
        yCoordinate.BackColor = Color.White;
        tagged.BackColor = Color.White;
        totalTries.BackColor = Color.White;

        header.Location = new Point(0,0);
        display.Location = new Point(0,60);
        buttons.Location = new Point(0,635);
        title.Location = new Point(540,0);
        author.Location = new Point(540,35);
        start.Location = new Point(28,20);
        initialize.Location = new Point(239,20);
        exit.Location = new Point(1172,120);
        xCoordinate.Location = new Point(608,120);
        yCoordinate.Location = new Point(872,120);
        tagged.Location = new Point(608,30);
        totalTries.Location = new Point(872,30);
        xLabel.Location = new Point(608,100);
        yLabel.Location = new Point(872,100);
        taggedLabel.Location = new Point(608,10);
        totalTriesLabel.Location = new Point(872,10);
        invalidInput.Location = new Point(405,45);

        xCoordinate.ReadOnly = true;
        yCoordinate.ReadOnly = true;
        tagged.ReadOnly = true;
        totalTries.ReadOnly = true;

        Text = "Ricochet Ball";
        title.Text = "Ricochet Ball";
        author.Text = "By Brennon Hahs";
        start.Text = "Start";
        initialize.Text = "Initialize";
        exit.Text = "Exit";
        xCoordinate.Text = "N/A";
        yCoordinate.Text = "N/A";
        tagged.Text = "0";
        totalTries.Text = "0";
        xLabel.Text = "X Coord.";
        yLabel.Text = "Y Coord.";
        taggedLabel.Text = "Tagged";
        totalTriesLabel.Text = "Total";
        invalidInput.Text = "";

        title.Font = new Font("Times New Roman",20,FontStyle.Bold);
        author.Font = new Font("Times New Roman",10,FontStyle.Regular);
        start.Font = new Font("Arial",10,FontStyle.Bold);
        initialize.Font = new Font("Arial",10,FontStyle.Bold);
        exit.Font = new Font("Arial",10,FontStyle.Bold);
        xCoordinate.Font = new Font("Arial",10,FontStyle.Bold);
        yCoordinate.Font = new Font("Arial",10,FontStyle.Bold);
        tagged.Font = new Font("Arial",10,FontStyle.Bold);
        totalTries.Font = new Font("Arial",10,FontStyle.Bold);
        xLabel.Font = new Font("Arial",10,FontStyle.Bold);
        yLabel.Font = new Font("Arial",10,FontStyle.Bold);
        taggedLabel.Font = new Font("Arial",10,FontStyle.Bold);
        totalTriesLabel.Font = new Font("Arial",10,FontStyle.Bold);
        invalidInput.Font = new Font("Arial",14,FontStyle.Bold);

        title.TextAlign = ContentAlignment.MiddleCenter;
        author.TextAlign = ContentAlignment.MiddleCenter;
        start.TextAlign = ContentAlignment.MiddleCenter;
        initialize.TextAlign = ContentAlignment.MiddleCenter;
        exit.TextAlign = ContentAlignment.MiddleCenter;
        xCoordinate.TextAlign = HorizontalAlignment.Center;
        yCoordinate.TextAlign = HorizontalAlignment.Center;

        Controls.Add(header);
        header.Controls.Add(title);
        header.Controls.Add(author);
        Controls.Add(display);
        Controls.Add(buttons);
        buttons.Controls.Add(start);
        buttons.Controls.Add(initialize);
        buttons.Controls.Add(exit);
        buttons.Controls.Add(xCoordinate);
        buttons.Controls.Add(yCoordinate);
        buttons.Controls.Add(tagged);
        buttons.Controls.Add(totalTries);
        buttons.Controls.Add(xLabel);
        buttons.Controls.Add(yLabel);
        buttons.Controls.Add(taggedLabel);
        buttons.Controls.Add(totalTriesLabel);
        buttons.Controls.Add(invalidInput);

        start.Enabled = false;


        refreshClock.Enabled = false;
        refreshClock.Interval = convertedReInterval;
        refreshClock.Elapsed += new ElapsedEventHandler(refreshInterface);

        ballClock.Enabled = false;
        ballClock.Interval = convertedAnInterval;
        ballClock.Elapsed += new ElapsedEventHandler(updateCoordinates);
        
        start.Click += new EventHandler(startPause);
        exit.Click += new EventHandler(exitProgram);
        initialize.Click += new EventHandler(initializeFunct);

        display.MouseDown += new MouseEventHandler(didItHit);

        CenterToScreen();
    }

    void updateCoordinates(Object sender, EventArgs events) {
        x = x + deltaX;
        y = y + deltaY;
        if (y <= 0) {
            y = 0;
            deltaY = -deltaY;
        }
        if (x <= 0) {
            x = 0;
            deltaX = -deltaX;
        }
        if (y + (2*radius) >= 575) {
            y = 575 - (2*radius);
            deltaY = -deltaY;
        }
        if ((x + (2*radius)) >= 1280) {
            x = 1280 - (2*radius);
            deltaX = -deltaX;
        }
        xCoordinate.Text = (x + radius).ToString();
        yCoordinate.Text = (y + radius).ToString();
    }

    void refreshInterface(Object sender, EventArgs events) {
        display.Invalidate();
    }

    void initializeFunct(Object sender, EventArgs events) {
        bool isException = false;
        initialized = true;
        ballClock.Enabled = false;
        refreshClock.Enabled = false;
        direction = rnd.Next();
        circSpeed = rnd.Next(50, 100);
        start.Text = "Start";
        start.Enabled = true;
        invalidInput.Text = "";
        radians = direction * (Math.PI/180);
        deltaX = (circSpeed/animationClockSpeed) * Math.Cos(radians);
        deltaY = (circSpeed/animationClockSpeed) * Math.Sin(radians);
        x = 640 - radius;
        y = 287.5 - radius;
        display.Invalidate();
        xCoordinate.Text = (x + radius).ToString();
        yCoordinate.Text = (y + radius).ToString();
    }

    void startPause(Object sender, EventArgs events) {
        if (refreshClock.Enabled == false && ballClock.Enabled == false) {
            ballClock.Enabled = true;
            refreshClock.Enabled = true;
            start.Text = "Pause";
        }
        else {
            ballClock.Enabled = false;
            refreshClock.Enabled = false;
            start.Text = "Start";
        }
    }
    
    void didItHit(Object sender, System.Windows.Forms.MouseEventArgs e) {
        if (((e.X >= x) && (e.X <= (x + radius*2))) && ((e.Y >= y) && (e.Y <= (y + radius*2)))) {
            hitMice += 1;
        }
        attempts += 1;
        tagged.Text = hitMice.ToString();
        totalTries.Text = attempts.ToString();
    }

    void exitProgram(Object sender, EventArgs events) {
        Close();
    }


    public class Graphicpanel: Panel {
        private Pen bicPen = new Pen(Color.Black, 1);
        private Brush greenBrush = new SolidBrush(Color.Green);
        private Brush yellowBrush = new SolidBrush(Color.Yellow);
        private Brush redBrush = new SolidBrush(Color.Red);
        private Brush blueBrush = new SolidBrush(Color.Blue);

        public Graphicpanel() {
            Console.WriteLine("A graphic enabled panel was created");
        }
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            if (initialized == true) {
                g.FillEllipse(greenBrush, (float)x, (float)y, (float)(radius*2), (float)(radius*2));
            }
            base.OnPaint(e);
        }
    }
}