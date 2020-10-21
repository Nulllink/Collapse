using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collapse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool steps = false;
        int player=0;
        int maxplayer = 2;
        int cells1 = 8;
        int cells2 = 8;
        List<Color> col = new List<Color>() { Color.Green,Color.Blue,Color.Red,Color.Yellow,Color.Violet,Color.Black};

        private void Form1_Load(object sender, EventArgs e)
        {
            spacecreate();
        }
        private void pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (steps == false)
            {
                if (pic.Image == null)
                {
                    pic.Image = Properties.Resources.C1;
                    pic.Image.Tag = player + " " + 1;
                    pic.BackColor = col[player];
               
                    //steps[player] = steps[player] + 1;
                    if (player < maxplayer - 1)
                    {
                        player++;
                    }
                    else
                    {
                        steps = true;
                        player = 0;
                    }
                    toolStripStatusLabel1.Text = (player+1).ToString();
                }

            }
            else
            {
                if (pic.Image != null)
                {
                    bool pl = colap(pic, false);
                    if (pl == true)
                    {
                        if (player < maxplayer - 1)
                        {
                            player++;
                        }
                        else
                        {
                            player = 0;
                        }
                        toolStripStatusLabel1.Text = (player+1).ToString();
                    }
                }

            }
        }

        private bool colap(PictureBox pic,bool rec)
        {
            if (pic.Image != null)
            {
                string[] tags = pic.Image.Tag.ToString().Split(' ');
                if (tags[0] == player.ToString()||rec==true)
                {
                    if (tags[1] == "1")
                    {
                        pic.Image = Properties.Resources.C2;
                        pic.Image.Tag = player + " " + 2;
                    }
                    if (tags[1] == "2")
                    {
                        pic.Image = Properties.Resources.C3;
                        pic.Image.Tag = player + " " + 3;
                    }
                    if (tags[1] == "3")
                    {
                        pic.Image = Properties.Resources.C4;
                        pic.Image.Tag = player + " " + 4;
                        colap4();
                    }
                    return true;
                }
            }
            return false;
        }

        private void colap4()
        {
            int x, y;
            int i,for1,for2;
            int count4=1;
            while (count4 != 0)
            {
                count4 = 0;
                for (for1 = 1; for1 <= cells1; for1++)
                    for (for2 = 1; for2 <= cells2; for2++)
                    {
                        
                        PictureBox pic = (PictureBox)panel1.Controls[for1.ToString() + for2.ToString()];
                        if (pic.Image != null)
                        {
                            x = ((Point)pic.Tag).X;
                            y = ((Point)pic.Tag).Y;
                            string[] tags = pic.Image.Tag.ToString().Split(' ');
                            if (tags[1] == "4")
                            {
                                pic.Image = null;
                                pic.BackColor = Color.White;
                                for (i = 0; i < 4; i++)
                                {
                                    PictureBox pic1 = null;
                                    switch (i)
                                    {
                                        case 0:
                                            if (y - 1 > 0)
                                            {
                                                pic1 = (PictureBox)panel1.Controls[x.ToString() + (y - 1).ToString()];
                                            }
                                            break;
                                        case 1:
                                            if (x + 1 <= cells1)
                                            {
                                                pic1 = (PictureBox)panel1.Controls[(x + 1).ToString() + y.ToString()];
                                            }
                                            break;
                                        case 2:
                                            if (y + 1 <= cells2)
                                            {
                                                pic1 = (PictureBox)panel1.Controls[x.ToString() + (y + 1).ToString()];
                                            }
                                            break;
                                        case 3:
                                            if (x - 1 > 0)
                                            {
                                                pic1 = (PictureBox)panel1.Controls[(x - 1).ToString() + y.ToString()];
                                            }
                                            break;
                                    }
                                    if (pic1 != null)
                                    {
                                        if (pic1.Image != null)
                                        {
                                            string[] tags1 = pic1.Image.Tag.ToString().Split(' ');
                                            if (tags1[1] == "1")
                                            {
                                                pic1.Image = Properties.Resources.C2;
                                                pic1.Image.Tag = player + " " + 2;
                                                pic1.BackColor = col[player];
                                            }
                                            if (tags1[1] == "2")
                                            {
                                                pic1.Image = Properties.Resources.C3;
                                                pic1.Image.Tag = player + " " + 3;
                                                pic1.BackColor = col[player];
                                            }
                                            if (tags1[1] == "3")
                                            {
                                                pic1.Image = Properties.Resources.C4;
                                                pic1.Image.Tag = player + " " + 4;
                                                pic1.BackColor = col[player];
                                                count4++;
                                                //colap(pic1,true);
                                                //textBox1.Text = "5";
                                            }
                                        }
                                        else
                                        {
                                            pic1.Image = Properties.Resources.C1;
                                            pic1.Image.Tag = player + " " + 1;
                                            pic1.BackColor = col[player];
                                        }
                                    }
                                }
                            }
                        }
                    }
            }
        }

        private void spacecreate()
        {
            int for1, for2;
            panel1.Controls.Clear();
            Text = "Checkers";
            int w = panel1.Width / cells1;
            int h = panel1.Height / cells2;
            for (for1 = 1; for1 <= cells1; for1++)
                for (for2 = 1; for2 <= cells2; for2++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Height = h;
                    pic.Width = w;
                    pic.Location = new Point(w * (for1 - 1), h * (for2 - 1));
                    pic.BackColor = Color.White;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Tag = new Point(for1, for2);
                    pic.Name = for1.ToString() + for2.ToString();
                    pic.Click += pic_Click;
                    panel1.Controls.Add(pic);
                }
        }
        private void paramch()
        {
            cells1 = int.Parse(textBox2.Text);
            cells2 = int.Parse(textBox3.Text);
            player = 0;
            panel1.Width = Width - 60;
            panel1.Height = Height - 110;
            spacecreate();
            maxplayer = int.Parse(textBox1.Text);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter&&steps==false)
            {
                paramch();                
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && steps == false)
            {
                paramch();
            }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && steps == false)
            {
                paramch();
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (steps == false)
            {
                paramch();
            }
        }
    }
}
